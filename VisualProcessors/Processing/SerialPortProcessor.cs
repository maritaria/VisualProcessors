using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using VisualProcessors.Forms;
using System.Diagnostics;

namespace VisualProcessors.Processing
{
	[ProcessorMeta("Bram Kamies", "Reads data from a SerialPort and writes it to its 'Output' channel", "", "Output",
		InputTabMode = ProcessorTabMode.Hidden,
		CustomTabMode = ProcessorTabMode.Hidden
		)]
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
				return int.Parse(Options.GetOption("Serial.ReadTimeout", "-1"));
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
			AddOutputChannel("Output");
		}

		~SerialPortProcessor()
		{
			Stop();
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
			SerialPort.Open();
			base.Start();
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

		protected override void WorkerMethod()
		{
			while (SerialPort.IsOpen)
			{
				int i = SerialPort.ReadByte();
				if (i==-1)
				{
					break;
				}
				GetOutputChannel("Output").WriteValue((double)i);
			}
			OnWarning("SerialPort has closed");
		}

		#endregion Methods
	}

	#region PropertyGrid Helper Classes

	/// <summary>
	///
	/// </summary>
	/// <seealso cref="http://stackoverflow.com/questions/1016239/how-to-create-custom-propertygrid-editor-item-which-opens-a-form"/>
	public class SerialPortEditor : UITypeEditor
	{
		public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
		{
			IWindowsFormsEditorService svc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
			SerialPortSettings settings = value as SerialPortSettings;
			if (svc != null && settings != null)
			{
				using (SerialPortForm form = new SerialPortForm(settings.PortName, settings.BaudRate, settings.DataBits, settings.Parity, settings.Handshake, settings.StopBits, settings.ReadTimeout, settings.WriteTimeout, settings.ReceivedBytesThreshold, settings.DtrEnable, settings.RtsEnable))
				{
					if (svc.ShowDialog(form) == DialogResult.OK)
					{
						//Update the settings object, value object is also updated because settings is a casted reference
						settings.BaudRate = form.BaudRate;
						settings.DataBits = form.DataBits;
						settings.DtrEnable = form.DtrEnable;
						settings.Handshake = form.Handshake;
						settings.Parity = form.Parity;
						settings.PortName = form.PortName;
						settings.ReadTimeout = form.ReadTimeout;
						settings.ReceivedBytesThreshold = form.ReceivedBytesThreshold;
						settings.RtsEnable = form.RtsEnable;
						settings.StopBits = form.StopBits;
						settings.WriteTimeout = form.WriteTimeout;
					}
				}
			}
			return value;
		}

		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}
	}

	[Editor(typeof(SerialPortEditor), typeof(UITypeEditor))]
	public class SerialPortSettings
	{
		private SerialPortProcessor m_Owner;

		public SerialPortSettings(SerialPortProcessor owner)
		{
			m_Owner = owner;
		}

		public int BaudRate
		{
			get
			{
				return m_Owner.BaudRate;
			}
			set
			{
				m_Owner.BaudRate = value;
			}
		}

		public int DataBits
		{
			get
			{
				return m_Owner.DataBits;
			}
			set
			{
				m_Owner.DataBits = value;
			}
		}

		public bool DtrEnable
		{
			get
			{
				return m_Owner.DtrEnable;
			}
			set
			{
				m_Owner.DtrEnable = value;
			}
		}

		public Handshake Handshake
		{
			get
			{
				return m_Owner.Handshake;
			}
			set
			{
				m_Owner.Handshake = value;
			}
		}

		public Parity Parity
		{
			get
			{
				return m_Owner.Parity;
			}
			set
			{
				m_Owner.Parity = value;
			}
		}

		public string PortName
		{
			get
			{
				return m_Owner.PortName;
			}
			set
			{
				m_Owner.PortName = value;
			}
		}

		public int ReadTimeout
		{
			get
			{
				return m_Owner.ReadTimeout;
			}
			set
			{
				m_Owner.ReadTimeout = value;
			}
		}

		public int ReceivedBytesThreshold
		{
			get
			{
				return m_Owner.ReceivedBytesThreshold;
			}
			set
			{
				m_Owner.ReceivedBytesThreshold = value;
			}
		}

		public bool RtsEnable
		{
			get
			{
				return m_Owner.RtsEnable;
			}
			set
			{
				m_Owner.RtsEnable = value;
			}
		}

		public StopBits StopBits
		{
			get
			{
				return m_Owner.StopBits;
			}
			set
			{
				m_Owner.StopBits = value;
			}
		}

		public int WriteTimeout
		{
			get
			{
				return m_Owner.WriteTimeout;
			}
			set
			{
				m_Owner.WriteTimeout = value;
			}
		}

		public override string ToString()
		{
			string result = "Portname: " + PortName + " Baudrate: " + BaudRate;
			if (Parity!=Parity.None)
			{
				result += " Parity: " + Parity;
			}
			if (Handshake!=Handshake.None)
			{
				result += " Handshake: " + Handshake;
			}
			if (StopBits!= StopBits.One)
			{
				result += " Stopbits: " + StopBits;
			}

			return result;
		}
	}

	#endregion PropertyGrid Helper Classes
}