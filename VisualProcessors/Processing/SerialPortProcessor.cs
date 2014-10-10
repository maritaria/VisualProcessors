#region Using statements

using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion Using statements

namespace VisualProcessors.Processing
{
	[ProcessorAttribute("Bram Kamies", "Reads data from a SerialPort and writes it to its 'Output' channel", "", "Output",
		HideInputTab = true,
		SettingsTabLabel = "SerialPort"
		)]
	public class SerialPortProcessor : Processor
	{
		#region Properties

		private SerialPort m_SerialPort;

		protected SerialPort SerialPort
		{
			get
			{
				return m_SerialPort;
			}
			set
			{
				m_SerialPort = value;
			}
		}

		#endregion Properties

		#region Options

		public int BaudRate
		{
			get
			{
				return int.Parse(Options.GetOption("Serial.BaudRate", "9600"));
			}
			set
			{
				Options.SetOption("Serial.BaudRate", value.ToString());
				OnModified(HaltTypes.ShouldHalt);
			}
		}

		public int DataBits
		{
			get
			{
				return int.Parse(Options.GetOption("Serial.DataBits", "8"));
			}
			set
			{
				Options.SetOption("Serial.DataBits", value.ToString());
				OnModified(HaltTypes.ShouldHalt);
			}
		}

		public bool DtrEnable
		{
			get
			{
				return bool.Parse(Options.GetOption("Serial.DtrEnable", "False"));
			}
			set
			{
				Options.SetOption("Serial.DataBits", value.ToString());
				OnModified(HaltTypes.ShouldHalt);
			}
		}

		public Handshake Handshake
		{
			get
			{
				return (Handshake)Enum.Parse(typeof(Handshake), Options.GetOption("Serial.Handshake", "None"));
			}
			set
			{
				Options.SetOption("Serial.Handshake", value.ToString());
				OnModified(HaltTypes.ShouldHalt);
			}
		}

		public Parity Parity
		{
			get
			{
				return (Parity)Enum.Parse(typeof(Parity), Options.GetOption("Serial.Parity", "None"));
			}
			set
			{
				Options.SetOption("Serial.Parity", value.ToString());
				OnModified(HaltTypes.ShouldHalt);
			}
		}

		public string PortName
		{
			get
			{
				return Options.GetOption("Serial.PortName", "Unknown");
			}
			set
			{
				Options.SetOption("Serial.PortName", value);
				OnModified(HaltTypes.ShouldHalt);
			}
		}

		public int ReadTimeout
		{
			get
			{
				return int.Parse(Options.GetOption("Serial.ReadTimeout", "-1"));
			}
			set
			{
				Options.SetOption("Serial.ReadTimeout", SerialPort.InfiniteTimeout.ToString());
				OnModified(HaltTypes.ShouldHalt);
			}
		}

		public int ReceivedBytesThreshold
		{
			get
			{
				return int.Parse(Options.GetOption("Serial.ReceivedBytesThreshold", "1"));
			}
			set
			{
				Options.SetOption("Serial.ReceivedBytesThreshold", value.ToString());
				OnModified(HaltTypes.ShouldHalt);
			}
		}

		public bool RtsEnable
		{
			get
			{
				return bool.Parse(Options.GetOption("Serial.DtrEnable", "False"));
			}
			set
			{
				Options.SetOption("Serial.DataBits", value.ToString());
				OnModified(HaltTypes.ShouldHalt);
			}
		}

		public StopBits StopBits
		{
			get
			{
				return (StopBits)Enum.Parse(typeof(StopBits), Options.GetOption("Serial.StopBits", StopBits.One.ToString()));
			}
			set
			{
				Options.SetOption("Serial.StopBits", value.ToString());
				OnModified(HaltTypes.ShouldHalt);
			}
		}

		public int WriteTimeout
		{
			get
			{
				return int.Parse(Options.GetOption("Serial.WriteTimeout", SerialPort.InfiniteTimeout.ToString()));
			}
			set
			{
				Options.SetOption("Serial.WriteTimeout", value.ToString());
				OnModified(HaltTypes.ShouldHalt);
			}
		}

		#endregion Options

		#region Constructor

		public SerialPortProcessor()
			: base()
		{
		}

		public SerialPortProcessor(Pipeline pipeline, string name)
			: base(pipeline, name)
		{
			AddOutputChannel("Output");
		}

		#endregion Constructor

		#region Methods

		public override void Start()
		{
			if (SerialPort != null)
			{
				//This should never happen
				OnWarning("SerialPort was not disposed previously");
				if (SerialPort.IsOpen)
				{
					SerialPort.Close();
				}
				SerialPort.Dispose();
			}
			SerialPort = new SerialPort(PortName, BaudRate, Parity, DataBits, StopBits);
			SerialPort.DtrEnable = DtrEnable;
			SerialPort.Handshake = Handshake;
			SerialPort.ReadTimeout = ReadTimeout;
			SerialPort.ReceivedBytesThreshold = ReceivedBytesThreshold;
			SerialPort.RtsEnable = RtsEnable;
			SerialPort.WriteTimeout = WriteTimeout;
			base.Start();
		}

		public override void Stop()
		{
			SerialPort.Dispose();
			SerialPort = null;
			base.Stop();
		}

		protected override void WorkerMethod()
		{
			while (SerialPort.IsOpen)
			{
				byte[] buffer = new byte[4096];
				int read = SerialPort.Read(buffer, 0, 4096);
				for (int i = 0; i < read; i++)
				{
					GetOutputChannel("Output").WriteValue((double)buffer[i]);
				}
			}
			OnWarning("SerialPort has closed");
		}

		#endregion Methods
	}
}