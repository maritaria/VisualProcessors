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

namespace VisualProcessors.Processing
{
	[ProcessorMeta("Bram Kamies", "Reads data from the eCompass sensor", "", "AcceleroX",
		InputTabMode = ProcessorTabMode.Hidden,
		CustomTabMode = ProcessorTabMode.Hidden)]
	public class CompassSensorProcessor : SerialPortProcessor
	{
		#region Properties

		private static byte[] SyncBlock = new byte[] { 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF };

		#endregion Properties

		#region Options

		[Browsable(true)]
		[ReadOnly(false)]
		[DisplayName("Sensor selection")]
		[Category("Settings")]
		[Description("Selects the axis to receive data for, allows for higher sample rates when selecting less axis")]
		[DefaultValue(0x00)]
		public AxisSelection Axis
		{
			get
			{
				return (AxisSelection)Enum.Parse(typeof(AxisSelection), Options.GetOption("AxisSelection", AxisSelection.None.ToString()));
			}
			set
			{
				Options.SetOption("AxisSelection", value.ToString());
				OnModified(HaltTypes.Ask);
			}
		}

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
				try
				{
					KillWorkerThread();
					SendStopCommand();
				}
				catch (Exception e)
				{
					OnError("Failed to stop device:" + Environment.NewLine + e.Message);
					return;
				}
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
			try
			{
				SendConfigCommand();
				SendStartCommand();
			}
			catch (Exception e)
			{
				OnError("Could not configure device:" + Environment.NewLine + e.Message);
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
				catch
				{
					return;
				}
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
			packet.AddRange(userdata);
			packet.Add(0xBB);
			packet.Add(0xCC);

			Console.WriteLine("Generated: " + BitConverter.ToString(packet.ToArray()));

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
			}
			return converted;
		}

		private void SendConfigCommand()
		{
			if (SerialPort != null && SerialPort.IsOpen)
			{
				int srate = SampleRate;
				byte[] packet = GeneratePacket(new byte[] { 0x03, (byte)(srate >> 8), (byte)(srate & 0xFF), (byte)Axis });
				Console.WriteLine("Sending config command");
				SerialPort.Write(packet, 0, packet.Length);
				Console.WriteLine("Waiting for confirmation");
				WaitForConfirmation();
			}
		}

		private void SendStartCommand()
		{
			if (SerialPort != null && SerialPort.IsOpen)
			{
				byte[] packet = GeneratePacket(new byte[] { 0x01 });
				Console.WriteLine("Sending start command");
				SerialPort.Write(packet, 0, packet.Length);
			}
		}

		private void SendStopCommand()
		{
			if (SerialPort != null && SerialPort.IsOpen)
			{
				byte[] packet = GeneratePacket(new byte[] { 0x02 });
				Console.WriteLine("Sending stop command");
				SerialPort.Write(packet, 0, packet.Length);
				Console.WriteLine("Waiting for confirmation");
				WaitForConfirmation();
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
					Console.Write(i.ToString() + " ");
					if (buffer[0] == 0xC4 && buffer[1] == 0xD8 && buffer[2] == 0x61)
					{
						Console.WriteLine("Confirmed");
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
			MagnetoX = 0x10,
			MagnetoY = 0x20,
			MagnetoZ = 0x40,
		}

		#endregion AxisSelection Enum
	}
}