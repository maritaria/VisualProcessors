using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VisualProcessors.Design;
using VisualProcessors.Processing;

namespace VisualProcessors.Processors
{
	[ProcessorMeta("Bram Kamies", "Reads data from the eCompass sensor", "", "AcceleroX",
		InputTabMode = ProcessorVisibility.Hide,
		CustomTabMode = ProcessorVisibility.Hide,
		ThreadMode = ProcessorThreadMode.ForceMultiThreading)]
	public class CompassSensorReader : SerialPortProcessor
	{
		#region Properties

		private static byte[] m_SyncBlock = new byte[] { 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF };

		private InternalState m_State = InternalState.Configuring;
		private bool m_StateConfirmed = false;
		private PacketReadState m_PacketState = PacketReadState.Syncblock;
		private DateTime m_StateConfirmedTimestamp = DateTime.Now;
		private List<byte> m_Buffer = new List<byte>();

		public InternalState State
		{
			get
			{
				return m_State;
			}
			private set
			{
				if (m_State != value)
				{
					m_State = value;
					StateConfirmed = false;
				}
			}
		}

		public bool StateConfirmed
		{
			get
			{
				return m_StateConfirmed;
			}
			private set
			{
				m_StateConfirmed = value;
				if (value)
				{
					m_StateConfirmedTimestamp = DateTime.Now;
				}
			}
		}

		public PacketReadState PacketState
		{
			get
			{
				return m_PacketState;
			}
			private set
			{
				m_PacketState = value;
			}
		}

		public List<byte> Buffer
		{
			get
			{
				return m_Buffer;
			}
			private set
			{
				m_Buffer = value;
			}
		}

		public DateTime StateConfirmedTimestamp
		{
			get
			{
				return m_StateConfirmedTimestamp;
			}
			private set
			{
				m_StateConfirmedTimestamp = value;
			}
		}

		#endregion Properties

		#region Options

		[Browsable(true)]
		[ReadOnly(false)]
		[DisplayName("Sensor selection")]
		[Category("Settings")]
		[Description("Selects the axis to receive data for, allows for higher sample rates when selecting less axis")]
		[DefaultValue(AxisSelection.None)]
		public AxisSelection Axis
		{
			get
			{
				return (AxisSelection)Enum.Parse(typeof(AxisSelection), Options.GetOption("AxisSelection", AxisSelection.None.ToString()));
			}
			set
			{
				Options.SetOption("AxisSelection", value.ToString());
				int val = 0;
				int axi = (int)Axis;
				for (int i = 0; i < 8; i++)
				{
					val += (axi >> i) & 1;
				}
				if (SampleRate * val > 2400)
				{
					OnError(new ProcessorErrorEventArgs(this, "Datarate too high", "the given samplerate and sensor axis selection creates a data rate higher then 2400, the device may block up!", true, HaltTypes.Ask));
				}
				OnModified(HaltTypes.Ask);
			}
		}

		[Browsable(true)]
		[ReadOnly(false)]
		[DisplayName("Confirmation timeout")]
		[Category("Settings")]
		[Description("Sets the maximum time in seconds to wait for a response from the RF device")]
		[DefaultValue(1)]
		public double ConfirmationTimeout
		{
			get
			{
				return double.Parse(Options.GetOption("ConfirmationTimeout", "1"));
			}
			set
			{
				if (double.IsNaN(value) || double.IsInfinity(value) || value <= 0)
				{
					throw new Exception("ConfirmationTimeout cannot be NaN, infinity, negative or zero");
				}
				Options.SetOption("ConfirmationTimeout", value.ToString());
			}
		}

		[Browsable(true)]
		[ReadOnly(false)]
		[DisplayName("SampleRate")]
		[Category("Settings")]
		[Description("Sets the samplerate to read samples in from, the actual binary data rate is the samplerate multiplied by the number of selected sensor axis.")]
		[DefaultValue(60)]
		public int SampleRate
		{
			get
			{
				return int.Parse(Options.GetOption("SampleRate", "100"));
			}
			set
			{
				Options.SetOption("SampleRate", value.ToString());
				int val = 0;
				int axi = (int)Axis;
				for (int i = 0; i < 8; i++)
				{
					val += (axi >> i) & 1;
				}
				if (SampleRate * val > 2400)
				{
					OnError(new ProcessorErrorEventArgs(this, "Datarate too high", "the given samplerate and sensor axis selection creates a data rate higher then 2400, the device may block up!", true, HaltTypes.Ask));
				}
				OnModified(HaltTypes.Ask);
			}
		}

		#endregion Options

		#region Constructor

		public CompassSensorReader()
		{
		}

		public CompassSensorReader(Pipeline p, string name)
			: base(p, name)
		{
			AddOutputChannel("AcceleroX");
			AddOutputChannel("AcceleroY");
			AddOutputChannel("AcceleroZ");
			AddOutputChannel("MagnetoX");
			AddOutputChannel("MagnetoY");
			AddOutputChannel("MagnetoZ");
		}

		#endregion Constructor

		#region Methods

		public override void Start()
		{
			State = InternalState.Starting;
			base.Start();
		}

		public override void Stop()
		{
			if (SerialPort != null)
			{
				State = InternalState.Stopping;
				DateTime start = DateTime.Now;
				while (State != InternalState.Stopped && DateTime.Now.Subtract(start).TotalSeconds < 1) ;
				SerialPort.Close();
			}
			base.Stop();
		}

		protected override void WorkerMethod()
		{
			if (Axis == AxisSelection.None)
			{
				OnError(new ProcessorErrorEventArgs(this, "AxisSelection", "No axis have been selected", true, HaltTypes.Ask, delegate()
				{
					return Axis != AxisSelection.None;
				}));
				return;
			}
			while (!SerialPort.IsOpen)
			{
				if (State==InternalState.Stopping)
				{
					State = InternalState.Stopped;
					return;
				}
			}

			OutputChannel ax = GetOutputChannel("AcceleroX");
			OutputChannel ay = GetOutputChannel("AcceleroY");
			OutputChannel az = GetOutputChannel("AcceleroZ");
			OutputChannel mx = GetOutputChannel("MagnetoX");
			OutputChannel my = GetOutputChannel("MagnetoY");
			OutputChannel mz = GetOutputChannel("MagnetoZ");
			AxisSelection axis = Axis;
			bool sax = axis.HasFlag(AxisSelection.AcceleroX);
			bool say = axis.HasFlag(AxisSelection.AcceleroY);
			bool saz = axis.HasFlag(AxisSelection.AcceleroZ);
			bool smx = axis.HasFlag(AxisSelection.MagnetoX);
			bool smy = axis.HasFlag(AxisSelection.MagnetoY);
			bool smz = axis.HasFlag(AxisSelection.MagnetoZ);
			int flagCount = 0;
			for (int i = 0; i < 32; i++)
			{
				flagCount += ((int)axis >> i) & 1;
			}

			while (SerialPort.IsOpen)
			{
				try
				{
					int i = -1;
					switch (State)
					{
						case InternalState.Starting:
						case InternalState.Configuring:
							if (StateConfirmed)
							{
								//Check response
								i = SerialPort.ReadByte();
								Buffer.Add((byte)i);
								if (Buffer.Count == 4 && Buffer[0] == 0x24 && Buffer[1] == 0x61 && Buffer[2] == 0xD8 && Buffer[3] == 0xC4)
								{
									State = InternalState.Activating;
									Buffer.Clear();
								}
								while (Buffer.Count >= 4)
								{
									Buffer.RemoveAt(0);
								}
								if (StateConfirmedTimestamp.Subtract(DateTime.Now).TotalSeconds > 0.5)
								{
									StateConfirmed = false;
								}
							}
							else
							{
								//Send packet
								int srate = SampleRate;
								byte[] packet = GeneratePacket(new byte[] { 0x03, (byte)(srate >> 8), (byte)(srate & 0xFF), (byte)Axis });
								SerialPort.DiscardInBuffer();
								SerialPort.Write(packet, 0, packet.Length);
								StateConfirmed = true;
							}
							break;
						case InternalState.Activating:
							if (StateConfirmed)
							{
								//Check response
								i = SerialPort.ReadByte();
								if (i == 0x24)
								{
									State = InternalState.Reading;
								}
								if (StateConfirmedTimestamp.Subtract(DateTime.Now).TotalSeconds > 0.5)
								{
									StateConfirmed = false;
								}
							}
							else
							{
								//Send packet
								int srate = SampleRate;
								byte[] packet = GeneratePacket(new byte[] { 0x01 });
								SerialPort.DiscardInBuffer();
								SerialPort.Write(packet, 0, packet.Length);
								StateConfirmed = true;
							}
							break;
						case InternalState.Reading:
							if (!StateConfirmed)
							{
								StateConfirmed = true;
								Buffer.Clear();
							}
							switch (PacketState)
							{
								case PacketReadState.Syncblock:
									i = SerialPort.ReadByte();
									Buffer.Add((byte)i);
									if (Buffer.Count == 6 && Buffer[0] == 0xAA && Buffer[1] == 0xBB && Buffer[2] == 0xCC && Buffer[3] == 0xDD && Buffer[4] == 0xEE && Buffer[5] == 0xFF)
									{
										PacketState = PacketReadState.Data;
										Buffer.Clear();
										break;
									}
									while (Buffer.Count >= 6)
									{
										Buffer.RemoveAt(0);
									}
									break;
								case PacketReadState.Data:
									i = SerialPort.ReadByte();
									Buffer.Add((byte)i);
									if (Buffer.Count >= 60)
									{
										PacketState = PacketReadState.Convert;
										break;
									}
									break;
								case PacketReadState.Convert:
									byte[] packet = Buffer.ToArray();
									Buffer.Clear();
									short converted;
									for (i = 0; i + flagCount < 30; i += flagCount)
									{
										converted = BitConverter.ToInt16(packet, i * 2);
										if (sax)
										{
											ax.WriteValue((double)converted);
										}
										if (say)
										{
											ay.WriteValue((double)converted);
										}
										if (saz)
										{
											az.WriteValue((double)converted);
										}
										if (smx)
										{
											mx.WriteValue((double)converted);
										}
										if (smy)
										{
											my.WriteValue((double)converted);
										}
										if (smz)
										{
											mz.WriteValue((double)converted);
										}
									}
									PacketState = PacketReadState.Syncblock;
									break;
							}
							break;
						case InternalState.Stopping:
							if (StateConfirmed)
							{
								//Check response
								i = SerialPort.ReadByte();
								if (i == 0x24)
								{
									State = InternalState.Stopped;
								}
								if (StateConfirmedTimestamp.Subtract(DateTime.Now).TotalSeconds > 0.5)
								{
									StateConfirmed = false;
								}
							}
							else
							{
								//Send packet
								int srate = SampleRate;
								byte[] packet = GeneratePacket(new byte[] { 0x02 });
								SerialPort.DiscardInBuffer();
								SerialPort.Write(packet, 0, packet.Length);
								StateConfirmed = true;
							}
							break;
						case InternalState.Stopped:
							return;
					}
				}
				catch
				{

				}
			}
			OnError(new ProcessorErrorEventArgs(this, "SerialPort", "The serialport has closed during simulation", false, HaltTypes.Ask));
		}

		private void CheckResponse(InternalState nextState)
		{
			int i = SerialPort.ReadByte();
			Buffer.Add((byte)i);
			if (Buffer.Count == 4 && Buffer[0] == 0x24 && Buffer[1] == 0x61 && Buffer[2] == 0xD8 && Buffer[3] == 0xC4)
			{
				State = nextState;
				Buffer.Clear();
			}
			while (Buffer.Count >= 4)
			{
				Buffer.RemoveAt(0);
			}
			if (StateConfirmedTimestamp.Subtract(DateTime.Now).TotalSeconds > 0.5)
			{
				StateConfirmed = false;
			}
		}

		private byte[] GeneratePacket(byte[] userdata)
		{
			List<byte> packet = new List<byte>();
			packet.Add(0xAA);
			if (userdata.Length > 255)
			{
				throw new ArgumentException("userdata is too long");
			}
			packet.Add((byte)userdata.Length);
			packet.Add((byte)userdata.Length);
			packet.AddRange(userdata);
			packet.Add(0xBB);
			packet.Add(0xCC);

			return packet.ToArray();
		}

		private void SendStopCommand()
		{
			if (SerialPort != null && SerialPort.IsOpen)
			{
				byte[] packet = GeneratePacket(new byte[] { 0x02 });
				DateTime start = DateTime.Now;
				bool success = false;
				while (DateTime.Now.Subtract(start).TotalSeconds < 1)
				{
					SerialPort.DiscardInBuffer();
					SerialPort.Write(packet, 0, packet.Length);
					int i = SerialPort.ReadByte();
					if (i == 0x24)
					{
						success = true;
						break;
					}
				}
				if (!success)
				{
					throw new TimeoutException("Device accept any packet within timeout");
				}
			}
		}

		#endregion Methods

		#region Enums

		[Flags]
		[Editor(typeof(EnumFlagsTypeEditor), typeof(UITypeEditor))]
		public enum AxisSelection : int
		{
			None = 0x00,
			AcceleroX = 0x01,
			AcceleroY = 0x02,
			AcceleroZ = 0x04,
			MagnetoX = 0x10,
			MagnetoY = 0x20,
			MagnetoZ = 0x40,
		}

		public enum InternalState
		{
			Starting,
			Configuring,
			Activating,
			Reading,
			Stopping,
			Stopped,
		}

		public enum PacketReadState
		{
			Syncblock,
			Data,
			Convert,
		}

		#endregion Enums
	}
}