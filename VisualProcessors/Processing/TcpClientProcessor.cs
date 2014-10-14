using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace VisualProcessors.Processing
{
	[ProcessorMeta("Bram Kamies", "A base processor for processors which use a TcpClient", "", "",
		AllowUserSpawn = false)]
	public class TcpClientProcessor : Processor
	{
		#region Properties

		public static string HostnameRegex = @"^([a-zA-Z0-9]([a-zA-Z0-9\-]*\.?[a-zA-Z0-9\-])*\.[a-zA-Z0-9\-]*[a-zA-Z0-9])|(localhost)$";
		public static string IpAddressRegex = @"^[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}$";

		private TcpClient m_TcpClient;
		private NetworkStream m_TcpStream;
		private Thread m_TcpThread;

		protected TcpClient TcpClient
		{
			get
			{
				return m_TcpClient;
			}
		}

		protected NetworkStream TcpStream
		{
			get
			{
				return m_TcpStream;
			}
		}

		#endregion Properties

		#region Options

		[Browsable(true)]
		[ReadOnly(false)]
		[DisplayName("Hostname")]
		[Category("Settings")]
		[Description("The IP-address or DNS-hostname to connect to")]
		[DefaultValue("localhost")]
		public string Hostname
		{
			get
			{
				return Options.GetOption("Hostname", "localhost");
			}
			set
			{
				if (!(Regex.IsMatch(value, HostnameRegex) || Regex.IsMatch(value, IpAddressRegex)))
				{
					throw new Exception("Hostname does conform to a valid IP-Address or DNS hostname");
				}

				Options.SetOption("Hostname", value);
				OnModified(HaltTypes.Ask);
			}
		}

		[Browsable(true)]
		[ReadOnly(false)]
		[DisplayName("Port")]
		[Category("Settings")]
		[Description("The port to connect to, must be in the range of 0-65535")]
		[DefaultValue(1000)]
		public int Port
		{
			get
			{
				return int.Parse(Options.GetOption("Port", "1000"));
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("Port cannot be lower or equal to zero");
				}
				if (value > UInt16.MaxValue)
				{
					throw new ArgumentOutOfRangeException("Port cannot be higher then " + UInt16.MaxValue);
				}
				Options.SetOption("Port", value.ToString());
				OnModified(HaltTypes.Ask);
			}
		}

		#endregion Options

		#region Constructor

		public TcpClientProcessor()
			: base()
		{
		}

		public TcpClientProcessor(Pipeline p, string name)
			: base(p, name)
		{
		}

		#endregion Constructor

		#region Methods

		public override void Start()
		{
			if (m_TcpThread != null)
			{
				m_TcpThread.Abort();
				while (m_TcpThread.IsAlive) ;
			}
			m_TcpThread = new Thread(new ThreadStart(TcpWorkerThread));
			m_TcpThread.IsBackground = true;
			m_TcpThread.Start();
			base.Start();
		}

		public override void Stop()
		{
			base.Stop();
			DisposeTcpClient();
		}

		protected void Connect(string hostname, int port)
		{
			if (m_TcpClient != null)
			{
				DisposeTcpClient();
			}
			m_TcpClient = new TcpClient(hostname, port);
			m_TcpStream = m_TcpClient.GetStream();
		}

		protected void DisposeTcpClient()
		{
			if (m_TcpClient != null)
			{
				m_TcpClient.Close();
				m_TcpClient = null;
				m_TcpStream.Dispose();
				m_TcpStream = null;
			}
		}

		/// <summary>
		///  Overridable function for packet handling implementation.
		/// </summary>
		/// <param name="packet">The packet that has been received, may be incomplete</param>
		/// <param name="length">
		///  The number of bytes read from the network stream, any bytes in the packet array beyond
		///  length should be ignored
		/// </param>
		/// <returns>True if the WorkerThread should disconnect</returns>
		protected virtual bool HandlePacket(byte[] packet, int length)
		{
			return false;
		}

		protected void TcpWorkerThread()
		{
			if (m_TcpClient != null)
			{
				DisposeTcpClient();
			}
			Connect(Hostname, Port);
			byte[] buffer;
			int read = 0;
			OutputChannel channel = GetOutputChannel("Data");
			while (true)
			{
				buffer = new byte[m_TcpClient.ReceiveBufferSize];
				try
				{
					read = m_TcpStream.Read(buffer, 0, m_TcpClient.ReceiveBufferSize);
				}
				catch (Exception e)
				{
					OnWarning("Connection lost: " + e.Message);
					return;
				}
				if (read == 0)
				{
					OnWarning("Connection lost: socket closed");
					return;
				}
				if (HandlePacket(buffer, read))
				{
					DisposeTcpClient();
					return;
				}
			}
		}

		#endregion Methods
	}
}