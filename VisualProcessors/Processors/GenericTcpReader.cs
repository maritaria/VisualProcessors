using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualProcessors.Processing;

namespace VisualProcessors.Processors
{
	[ProcessorMeta("Bram Kamies", "Processor that implements the TcpPlot Generic profile protocol", "", "1",
		CustomTabMode = ProcessorTabMode.Hide,
		InputTabMode = ProcessorTabMode.Hide)]
	public class GenericTcpReader : TcpClientBasedProcessor
	{
		#region Properties

		private string m_Storage = "";

		#endregion Properties

		#region Options

		[Browsable(true)]
		[ReadOnly(false)]
		[DisplayName("Columns")]
		[Category("Settings")]
		[Description("The number of values to expect per data row")]
		[DefaultValue(1)]
		public int ColumnCount
		{
			get
			{
				return OutputChannelCount;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("ColumnCount cannot be zero or negative");
				}
				int count = OutputChannelCount;
				while (count > value)
				{
					RemoveOutputChannel(GetOutputChannel(count.ToString()));
					count = OutputChannelCount;
				}
				while (count < value)
				{
					AddOutputChannel((count + 1).ToString());
					count = OutputChannelCount;
				}
			}
		}

		#endregion Options

		#region Constructor

		public GenericTcpReader()
			: base()
		{
		}

		public GenericTcpReader(Pipeline p, string name)
			: base(p, name)
		{
			AddOutputChannel("1");
		}

		#endregion Constructor

		#region Methods

		protected override bool HandlePacket(byte[] packet, int length)
		{
			string source = m_Storage + Encoding.UTF8.GetString(packet, 0, length);
			Console.WriteLine("Source: '" + source + "'");
			int i = 0;
			while (true)
			{
				if (source[i] == '$')
				{
					string sub = "";
					int j = i;
					bool ended = false;
					while (j < source.Length)
					{
						if (source[j] == '\n')
						{
							ended = true;
							break;
						}
						j++;
					}
					if (ended)
					{
						sub = source.Substring(i + 1, j - i - 1);
						string[] numbers = sub.Split(',');
						for (int cnum = 0; cnum < numbers.Length && cnum < ColumnCount; cnum++)
						{
							double value = 0;
							if (double.TryParse(numbers[cnum], out value))
							{
								GetOutputChannel(cnum.ToString()).WriteValue(value);
							}
						}
						i = j + 1;
					}
					else
					{
						break;
					}
				}
				else
				{
					i++;
				}
				if (i >= source.Length)
				{
					break;
				}
			}
			if (i < source.Length)
			{
				m_Storage = source.Substring(i);
			}
			else
			{
				m_Storage = "";
			}
			return false;
		}

		#endregion Methods
	}
}