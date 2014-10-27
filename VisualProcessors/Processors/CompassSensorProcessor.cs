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
		InputTabMode = ProcessorTabMode.Hide,
		CustomTabMode = ProcessorTabMode.Hide,
		ThreadMode=ProcessorThreadMode.ForceMultiThreading)]
	public class CompassSensorProcessor : SerialPortProcessor
	{
		#region Properties

		private static byte[] SyncBlock = new byte[] { 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF };

		private bool m_DeviceStopped = false;
		private bool m_Stopping = false;

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
				Options.GetOption("AxisSelection", value.ToString());
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
				int val = 0;
				int axi = (int)Axis;
				for (int i = 0; i < 8; i++)
				{
					val += (axi >> i) & 1;
				}
				if (value * val > 2400)
				{
					OnWarning("Warning: the given samplerate and sensor axis selection creates a data rate higher then 2400, the device may block up!");
				}
				Options.SetOption("SampleRate", value.ToString());
			}
		}

		#endregion Options

		#region Constructor

		public CompassSensorProcessor()
		{
		}

		public CompassSensorProcessor(Pipeline p, string name)
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

		public override void Stop()
		{
			if (SerialPort != null)
			{
				m_DeviceStopped = false;
				m_Stopping = true;
				SerialPort.ReadTimeout = 0;
				DateTime start = DateTime.Now;
				while (!m_DeviceStopped && DateTime.Now.Subtract(start).TotalSeconds < 5) ;
				SerialPort.Dispose();
				SerialPort = null;
				m_DeviceStopped = false;
				m_Stopping = false;
			}
			base.Stop();
		}

		protected override void WorkerMethod()
		{
			if (Axis == AxisSelection.None)
			{
				OnWarning("No axis have been selected");
				return;
			}
			DateTime start = DateTime.Now;
			while(true)
			{
				if (SerialPort!=null && SerialPort.IsOpen)
				{
					break;
				}
				if (DateTime.Now.Subtract(start).TotalSeconds > 5)
				{
					OnError("SerialPort was not initialized");
					return;
				}
				Thread.Sleep(1);
			}

			try
			{
				SendConfigCommand();
				SendStartCommand();
			}
			catch (Exception e)
			{
				if (!m_Stopping)
				{
					OnError("Could not configure device:" + Environment.NewLine + e.Message);
				}
				m_DeviceStopped = true;
				return;
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
			int count = 0;
			for (int i = 0; i < 8; i++)
			{
				count += ((int)axis >> i) & 1;
			}

			while (true)
			{
				try
				{
					List<short> packet = ReadPacket().ToList();
					if (m_Stopping)
					{
						break;
					}
					while (packet.Count >= count)
					{
						if (sax)
						{
							ax.WriteValue(PopFromBottom(packet));
						}
						if (say)
						{
							ay.WriteValue(PopFromBottom(packet));
						}
						if (saz)
						{
							az.WriteValue(PopFromBottom(packet));
						}
						if (smx)
						{
							mx.WriteValue(PopFromBottom(packet));
						}
						if (smy)
						{
							my.WriteValue(PopFromBottom(packet));
						}
						if (smz)
						{
							mz.WriteValue(PopFromBottom(packet));
						}
					}
				}
				catch (Exception e)
				{
					if (m_Stopping)
					{
						break;
					}
					if (SerialPort != null)
					{
						OnWarning("Lost connection to device:" + Environment.NewLine + e.Message);
					}
					return;
				}
			}
			SerialPort.ReadTimeout = 3000;
			try
			{
				SendStopCommand();
			}
			catch (Exception e2)
			{
				OnWarning("Failed to stop device:" + Environment.NewLine + e2.Message);
			}
			finally
			{
				m_DeviceStopped = true;
			}
		}

		private bool ArrayEquals(byte[] b1, byte[] b2)
		{
			if (b1.Length != 6 || b2.Length != 6)
			{
				return false;
			}
			for (int i = 0; i < 6; i++)
			{
				if (b1[i] != b2[i])
				{
					return false;
				}
			}
			return true;
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

		private double PopFromBottom(List<short> l)
		{
			short result = l[0];
			l.RemoveAt(0);
			return (double)result;
		}

		private short[] ReadPacket()
		{
			byte[] header = new byte[6] { 0, 0, 0, 0, 0, 0 };
			while (true)
			{
				SerialPort.Read(header, 5, 1);
				if (ArrayEquals(header, SyncBlock))
				{
					break;
				}
				Array.Copy(header, 1, header, 0, 5);
			}

			//Reading body
			byte[] packet = new byte[60];
			int count = 0;
			while (count < 60)
			{
				Thread.Sleep(1);
				count += SerialPort.Read(packet, count, 60 - count);
			}

			//Convert to data
			short[] converted = new short[30];
			for (int i = 0; i < 30; i++)
			{
				converted[i] = BitConverter.ToInt16(packet, i * 2);
				if (converted[i] < -25000)
				{
				}
			}
			return converted;
		}

		private void SendConfigCommand()
		{
			if (SerialPort != null && SerialPort.IsOpen)
			{
				int srate = SampleRate;
				byte[] packet = GeneratePacket(new byte[] { 0x03, (byte)(srate >> 8), (byte)(srate & 0xFF), (byte)Axis });
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
				WaitForConfirmation();
			}
		}

		private void SendStartCommand()
		{
			if (SerialPort != null && SerialPort.IsOpen)
			{
				byte[] packet = GeneratePacket(new byte[] { 0x01 });
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

		private void WaitForConfirmation()
		{
			byte[] buffer = new byte[3] { 0, 0, 0 };
			DateTime start = DateTime.Now;
			while (DateTime.Now.Subtract(start).TotalSeconds < ConfirmationTimeout)
			{
				if (SerialPort.BytesToRead > 0)
				{
					int i = SerialPort.ReadByte();
					if (i == -1)
					{
						throw new Exception("SerialPort has closed");
					}
					Array.Copy(buffer, 0, buffer, 1, buffer.Length - 1);
					buffer[0] = (byte)i;
					if (buffer[0] == 0xC4 && buffer[1] == 0xD8 && buffer[2] == 0x61)
					{
						return;
					}
				}
				else
				{
					Thread.Sleep(1);
				}
			}
			throw new Exception("Device did not respond with confirmation");
		}

		#endregion Methods

		#region AxisSelection Enum

		[Flags]
		[Editor(typeof(EnumFlagsTypeEditor), typeof(UITypeEditor))]
		public enum AxisSelection : int
		{
			None = 0x00,
			AcceleroX = 0x01,
			AcceleroY = 0x02,
			AcceleroZ = 0x04,
			//Accelero = AcceleroX | AcceleroY | AcceleroZ,
			MagnetoX = 0x10,
			MagnetoY = 0x20,
			MagnetoZ = 0x40,
			//Magneto = MagnetoX | MagnetoY | MagnetoZ,
			//All = Accelero | Magneto,
		}

		#endregion AxisSelection Enum
	}
}