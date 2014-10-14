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

		private List<List<short>> m_Bursts = new List<List<short>>();
		private Thread m_SpacerThread;

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
				OnModified(HaltTypes.AskHalt);
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

		public override void Start()
		{
			if (m_SpacerThread != null)
			{
				Stop();
			}
			m_SpacerThread = new Thread(new ThreadStart(SpacerWorkerMethod));
			m_SpacerThread.IsBackground = true;
			m_SpacerThread.Start();
			base.Start();
		}

		public override void Stop()
		{
			if (m_SpacerThread != null)
			{
				if (m_SpacerThread.IsAlive)
				{
					m_SpacerThread.Abort();
					while (m_SpacerThread.IsAlive) ;
				}
				m_SpacerThread = null;
			}
			base.Stop();
		}

		protected override void Prepare()
		{
			base.Prepare();
			m_Bursts.Clear();
		}

		protected override void WorkerMethod()
		{
			if (Axis == AxisSelection.None)
			{
				OnWarning("No axis have been selected");
				return;
			}
			while (true)
			{
				try
				{
					List<short> packet = ReadPacket().ToList();
					lock (m_Bursts)
					{
						m_Bursts.Add(packet);
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

		private void SpacerWorkerMethod()
		{
			TimeSpan averageSpan = new TimeSpan(0);
			DateTime lastread = DateTime.Now;
			OutputChannel ax = GetOutputChannel("AcceleroX");
			OutputChannel ay = GetOutputChannel("AcceleroY");
			OutputChannel az = GetOutputChannel("AcceleroZ");
			OutputChannel mx = GetOutputChannel("MagnetoX");
			OutputChannel my = GetOutputChannel("MagnetoY");
			OutputChannel mz = GetOutputChannel("MagnetoZ");
			while (true)
			{
				List<short> packet;
				while (m_Bursts.Count == 0) ;
				lock (m_Bursts)
				{
					packet = m_Bursts.First();
					m_Bursts.RemoveAt(0);
				}
				TimeSpan span = DateTime.Now.Subtract(lastread);
				averageSpan = new TimeSpan((averageSpan + span).Ticks / (2 + m_Bursts.Count));
				TimeSpan delay = new TimeSpan(averageSpan.Ticks / packet.Count);
				Console.WriteLine("{3} LastRead: {0}, average: {1}, span: {2}", span, averageSpan, delay, m_Bursts.Count);
				lastread = DateTime.Now;
				while (packet.Count > 0)
				{
					if (Axis.HasFlag(AxisSelection.AcceleroX))
					{
						ax.WriteValue(PopFromBottom(packet));
						Thread.Sleep(delay);
					}
					if (Axis.HasFlag(AxisSelection.AcceleroY))
					{
						ay.WriteValue(PopFromBottom(packet));
						Thread.Sleep(delay);
					}
					if (Axis.HasFlag(AxisSelection.AcceleroZ))
					{
						az.WriteValue(PopFromBottom(packet));
						Thread.Sleep(delay);
					}
					if (Axis.HasFlag(AxisSelection.MagnetoX))
					{
						mx.WriteValue(PopFromBottom(packet));
						Thread.Sleep(delay);
					}
					if (Axis.HasFlag(AxisSelection.MagnetoY))
					{
						my.WriteValue(PopFromBottom(packet));
						Thread.Sleep(delay);
					}
					if (Axis.HasFlag(AxisSelection.MagnetoZ))
					{
						mz.WriteValue(PopFromBottom(packet));
						Thread.Sleep(delay);
					}
				}
			}
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
			MagnetoX = 0x08,
			MagnetoY = 0x10,
			MagnetoZ = 0x20,
		}

		#endregion AxisSelection Enum
	}
}