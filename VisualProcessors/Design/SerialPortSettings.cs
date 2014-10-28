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
using VisualProcessors.Processing;
using VisualProcessors.Processors;

namespace VisualProcessors.Design
{
	[TypeConverter(typeof(ExpandableObjectConverter))]
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
			string result = "Portname: " + PortName + " Baudrate: " + BaudRate + " Databits: " + DataBits;
			if (Parity != Parity.None)
			{
				result += " Parity: " + Parity;
			}
			if (Handshake != Handshake.None)
			{
				result += " Handshake: " + Handshake;
			}
			if (StopBits != StopBits.None)
			{
				result += " Stopbits: " + StopBits;
			}
			if (ReadTimeout != SerialPort.InfiniteTimeout)
			{
				result += " ReadTimeout: " + ReadTimeout;
			}
			if (WriteTimeout != SerialPort.InfiniteTimeout)
			{
				result += " WriteTimeout: " + WriteTimeout;
			}
			if (ReceivedBytesThreshold != 1)
			{
				result += "ReceivedBytesThreshold: " + ReceivedBytesThreshold;
			}
			if (DtrEnable)
			{
				result += " DTR";
			}
			if (RtsEnable)
			{
				result += " RTS";
			}

			return result;
		}
	}
}