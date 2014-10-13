using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualProcessors.Processing;

namespace VisualProcessors.Forms
{
	public partial class SerialPortForm : Form
	{
		#region Properties

		private bool m_DataBitsTextBoxChanging = false;
		private bool m_ReadTimeoutTextBoxChanging = false;
		private bool m_ReceivedBytesThresholdTextBoxChanging = false;
		private bool m_WriteTimeoutTextBoxChanging = false;
		private bool m_BaudRateComboBoxChanging = false;

		public int BaudRate
		{
			get
			{
				int i;
				int.TryParse(BaudRateComboBox.Text, out i);
				return i;
			}
		}

		public int DataBits
		{
			get
			{
				int i;
				int.TryParse(DataBitsTextBox.Text, out i);
				return i;
			}
		}

		public bool DtrEnable
		{
			get
			{
				return DtrEnableCheckBox.Checked;
			}
		}

		public Handshake Handshake
		{
			get
			{
				return (Handshake)Enum.Parse(typeof(Handshake), HandshakeComboBox.Text);
			}
		}

		public Parity Parity
		{
			get
			{
				return (Parity)Enum.Parse(typeof(Parity), ParityComboBox.Text);
			}
		}

		public string PortName
		{
			get
			{
				return PortNameComboBox.Text;
			}
		}

		public int ReadTimeout
		{
			get
			{
				int i;
				if (int.TryParse(ReadTimeoutTextBox.Text, out i))
				{
					return i;
				}
				return SerialPort.InfiniteTimeout;
			}
		}

		public int ReceivedBytesThreshold
		{
			get
			{
				int i = 1;
				if (int.TryParse(ReceivedBytesThresholdTextBox.Text, out i))
				{
					return i;
				}
				return 1;
			}
		}

		public bool RtsEnable
		{
			get
			{
				return RtsEnableCheckBox.Checked;
			}
		}
		
		public StopBits StopBits
		{
			get
			{
				return (StopBits)Enum.Parse(typeof(StopBits), StopBitsComboBox.Text);
			}
		}

		public int WriteTimeout
		{
			get
			{
				int i;
				if (int.TryParse(WriteTimeoutTextBox.Text, out i))
				{
					return i;
				}
				return SerialPort.InfiniteTimeout;
			}
		}

		#endregion Properties

		#region Constructor

		public SerialPortForm()
		{
			InitializeComponent();
			ParityComboBox.DataSource = Enum.GetValues(typeof(Parity));
			HandshakeComboBox.DataSource = Enum.GetValues(typeof(Handshake));
			StopBitsComboBox.DataSource = Enum.GetValues(typeof(StopBits));
			ReadTimeoutTextBox.Text = SerialPort.InfiniteTimeout.ToString();
			WriteTimeoutTextBox.Text = SerialPort.InfiniteTimeout.ToString();
			RefreshPortList();
		}

		public SerialPortForm(string portname, int baudrate, int databits, Parity parity, Handshake handshake, StopBits stopbits, int readtimeout, int writetimeout, int receivedbytesthreshold, bool dtr, bool rts)
			: this()
		{
			PortNameComboBox.Text = portname;
			BaudRateComboBox.Text = baudrate.ToString();
			DataBitsTextBox.Text = databits.ToString();
			ParityComboBox.SelectedItem = parity;
			HandshakeComboBox.SelectedItem = handshake;
			StopBitsComboBox.SelectedItem = stopbits;
			ReadTimeoutTextBox.Text = readtimeout.ToString();
			WriteTimeoutTextBox.Text = writetimeout.ToString();
			ReceivedBytesThresholdTextBox.Text = receivedbytesthreshold.ToString();
			DtrEnableCheckBox.Checked = dtr;
			RtsEnableCheckBox.Checked = rts;
		}


		#endregion Constructor

		#region Methods

		protected void RefreshPortList()
		{
			string current = PortNameComboBox.Text;
			PortNameComboBox.Items.Clear();
			int selected = -1;
			foreach (string port in SerialPort.GetPortNames())
			{
				int i = PortNameComboBox.Items.Add(port);
				if (port == current)
				{
					selected = i;
				}
			}
			PortNameComboBox.SelectedIndex = selected;
		}

		protected void TextboxValidateInput(Control box)
		{
			if (!TextboxValidateInteger(box))
			{
				box.BackColor = Color.Red;
			}
			else
			{
				box.BackColor = Color.White;
			}
		}

		protected bool TextboxValidateInteger(Control box)
		{
			int i = 0;
			return int.TryParse(box.Text, out i);
		}

		#endregion Methods

		#region Event Handlers

		private void DataBitsTextBox_TextChanged(object sender, EventArgs e)
		{
			if (m_DataBitsTextBoxChanging)
			{
				return;
			}
			m_DataBitsTextBoxChanging = true;
			TextboxValidateInput(DataBitsTextBox);
			m_DataBitsTextBoxChanging = false;
		}

		private void ReadTimeoutTextBox_TextChanged(object sender, EventArgs e)
		{
			if (m_ReadTimeoutTextBoxChanging)
			{
				return;
			}
			m_ReadTimeoutTextBoxChanging = true;
			TextboxValidateInput(ReadTimeoutTextBox);
			m_ReadTimeoutTextBoxChanging = false;
		}

		private void ReceivedBytesThresholdTextBox_TextChanged(object sender, EventArgs e)
		{
			if (m_ReceivedBytesThresholdTextBoxChanging)
			{
				return;
			}
			m_ReceivedBytesThresholdTextBoxChanging = true;
			TextboxValidateInput(WriteTimeoutTextBox);
			m_ReceivedBytesThresholdTextBoxChanging = false;
		}

		private void RefreshButton_Click(object sender, EventArgs e)
		{
			RefreshPortList();
		}

		private void WriteTimeoutTextBox_TextChanged(object sender, EventArgs e)
		{
			if (m_WriteTimeoutTextBoxChanging)
			{
				return;
			}
			m_WriteTimeoutTextBoxChanging = true;
			TextboxValidateInput(WriteTimeoutTextBox);
			m_WriteTimeoutTextBoxChanging = false;
		}
		private void BaudRateComboBox_TextChanged(object sender, EventArgs e)
		{

			if (m_BaudRateComboBoxChanging)
			{
				return;
			}
			m_BaudRateComboBoxChanging = true;
			TextboxValidateInput(BaudRateComboBox);
			m_BaudRateComboBoxChanging = false;
		}

		#endregion Event Handlers
	}
}