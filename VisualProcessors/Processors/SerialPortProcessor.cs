using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Design;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using VisualProcessors.Design;
using VisualProcessors.Forms;
using VisualProcessors.Processing;

namespace VisualProcessors.Processors
{
	[ProcessorMeta("Bram Kamies", "Reads data from a SerialPort and writes it to its 'Output' channel", "", "Output",
		AllowUserSpawn = false)]
	public class SerialPortProcessor : Processor
	{
		#region Properties

		private SerialPort m_SerialPort;
		private SerialPortSettings m_SerialPortSettings;

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

		[Browsable(false)]
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

		[Browsable(false)]
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

		[Browsable(false)]
		public bool DtrEnable
		{
			get
			{
				return bool.Parse(Options.GetOption("Serial.DtrEnable", "False"));
			}
			set
			{
				Options.SetOption("Serial.DtrEnable", value.ToString());
				OnModified(HaltTypes.ShouldHalt);
			}
		}

		[Browsable(false)]
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

		[Browsable(false)]
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

		[Browsable(false)]
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

		[Browsable(false)]
		public int ReadTimeout
		{
			get
			{
				return int.Parse(Options.GetOption("Serial.ReadTimeout", SerialPort.InfiniteTimeout.ToString()));
			}
			set
			{
				Options.SetOption("Serial.ReadTimeout", SerialPort.InfiniteTimeout.ToString());
				OnModified(HaltTypes.ShouldHalt);
			}
		}

		[Browsable(false)]
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

		[Browsable(false)]
		public bool RtsEnable
		{
			get
			{
				return bool.Parse(Options.GetOption("Serial.RtsEnable", "False"));
			}
			set
			{
				Options.SetOption("Serial.RtsEnable", value.ToString());
				OnModified(HaltTypes.ShouldHalt);
			}
		}

		/// <summary>
		///  Used for displaying the SerialPort configuration in a PropertyGrid
		/// </summary>
		[Browsable(true)]
		[ReadOnly(false)]
		[DisplayName("Serial settings")]
		[Category("Settings")]
		[Description("Configures the SerialPort used to retrieve data")]
		public SerialPortSettings SerialSettings
		{
			get
			{
				return m_SerialPortSettings;
			}
		}

		[Browsable(false)]
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

		[Browsable(false)]
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
			m_SerialPortSettings = new SerialPortSettings(this);
		}

		public SerialPortProcessor(Pipeline pipeline, string name)
			: base(pipeline, name)
		{
			m_SerialPortSettings = new SerialPortSettings(this);
		}

		~SerialPortProcessor()
		{
			Stop();
		}

		#endregion Constructor

		#region Methods

		public override void Start()
		{
			base.Start();
			SerialPort = new SerialPort(PortName, BaudRate, Parity, DataBits, StopBits);
			SerialPort.DtrEnable = DtrEnable;
			SerialPort.Handshake = Handshake;
			SerialPort.ReadTimeout = ReadTimeout;
			SerialPort.ReceivedBytesThreshold = ReceivedBytesThreshold;
			SerialPort.RtsEnable = RtsEnable;
			SerialPort.WriteTimeout = WriteTimeout;
			SerialPort.Open();
		}

		public override void Stop()
		{
			if (SerialPort != null)
			{
				SerialPort.Dispose();
				SerialPort = null;
			}
			base.Stop();
		}

		#endregion Methods
	}

	#region PropertyGrid Helper Classes

	#endregion PropertyGrid Helper Classes
}