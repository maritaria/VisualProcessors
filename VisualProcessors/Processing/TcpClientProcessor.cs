using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace VisualProcessors.Processing
{
	public class TcpClientProcessor : Processor
	{
		#region Properties

		public static string HostnameRegex = @"([a-zA-Z0-9]([a-zA-Z0-9\-]*\.?[a-zA-Z0-9\-])*\.[a-zA-Z0-9\-]*[a-zA-Z0-9])";
		public static string IpAddressRegex = @"([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3})";
		public static string PortTailRegex = @"(\:[0-9]{1-5})";

		private TcpClient m_TcpClient;
		private NetworkStream m_TcpStream;

		#endregion Properties

		#region Options


		public string Hostname
		{
			get
			{
				return Options.GetOption("Hostname", "localhost");
			}
			set
			{
				if (value.Contains(":"))
				{
					throw new ArgumentException("Hostname cannot contain a port devider (:) ");
				}
				if (!(Regex.IsMatch(value,HostnameRegex)||Regex.IsMatch(value,IpAddressRegex)))
				{
					throw new Exception("Hostname does conform to a valid IP-Address or DNS hostname");
				}
				
				Options.SetOption("Hostname", value);
				OnModified(HaltTypes.Ask);
			}
		}

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

		public bool ConnectInWorkerThread
		{
			get
			{
				return bool.Parse(Options.GetOption("ConnectInWorkerThread", "False"));
			}
			set
			{
				Options.GetOption("ConnectInWorkerThread", value.ToString());
				OnModified(HaltTypes.Continue);
			}
		}

		#endregion Options

		#region Constructor
		public TcpClientProcessor() : base()
		{
		}
		public TcpClientProcessor(Pipeline p, string name) :base(p,name)
		{
		}

		#endregion Constructor

		#region Methods

		public override void Start()
		{
			if (m_TcpClient!=null)
			{
				DisposeTcpClient();
			}
			if (!ConnectInWorkerThread)
			{
				Connect();
			}
			base.Start();
		}
		protected override void WorkerMethod()
		{
			if (ConnectInWorkerThread)
			{
				Connect();
			}
			byte[] buffer;
			int read = 0;
			OutputChannel channel = GetOutputChannel("Data");
			while(true)
			{
				buffer = new byte[m_TcpClient.ReceiveBufferSize];
				try
				{
					read = m_TcpStream.Read(buffer, 0, m_TcpClient.ReceiveBufferSize);
				}
				catch(Exception e)
				{
					OnWarning("Connection lost: " + e.Message);
					return;
				}
				if (read==0)
				{
					OnWarning("Connection lost: socket closed");
					return;
				}
				if (HandlePacket(buffer,read))
				{
					DisposeTcpClient();
					return;
				}
			}
		}

		/// <summary>
		/// Overridable function for packet handling implementation.
		/// </summary>
		/// <param name="packet">The packet that has been received, may be incomplete</param>
		/// <param name="length">The number of bytes read from the network stream, any bytes in the packet array beyond length should be ignored</param>
		/// <returns>True if the WorkerThread should disconnect</returns>
		protected virtual bool HandlePacket(byte[] packet, int length)
		{
			Console.WriteLine("TcpClientProcessor.HandlePacket():");
			Console.WriteLine(BitConverter.ToString(packet, 0, length));
			return false;
		}

		public override void Stop()
		{
			base.Stop();
			DisposeTcpClient();
		}

		protected void Connect()
		{
			if (m_TcpClient!=null)
			{
				DisposeTcpClient();
			}
			m_TcpClient = new TcpClient(Hostname, Port);
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

		#endregion Methods
	}
}