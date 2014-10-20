using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualProcessors.Controls;
using VisualProcessors.Processing;

namespace VisualProcessors.Processors
{
	[ProcessorMeta("Bram Kamies", "Writes the input to a .csv file", "1", "",
		CustomTabMode = ProcessorTabMode.Hidden,
		OutputTabMode = ProcessorTabMode.Hidden)]
	public class CSVProcessor : FileBasedProcessor
	{
		#region Properties

		private StreamWriter m_FileWriter;
		private bool m_Truncate = false;

		#endregion Properties

		#region Options

		[Browsable(true)]
		[ReadOnly(false)]
		[DisplayName("Columns")]
		[Category("Settings")]
		[Description("The number of values to write per row")]
		[DefaultValue(1)]
		public int ColumnCount
		{
			get
			{
				return InputChannelCount;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("ColumnCount cannot be zero or negative");
				}
				int count = InputChannelCount;
				while (count > value)
				{
					RemoveInputChannel(GetInputChannel(count.ToString()));
					count = InputChannelCount;
				}
				while (count < value)
				{
					AddInputChannel((count + 1).ToString(), false);
					count = InputChannelCount;
				}
			}
		}

		[Browsable(true)]
		[ReadOnly(false)]
		[DisplayName("Seperator")]
		[Category("Settings")]
		[Description("The string to seperate columns by, is not added to the last column")]
		[DefaultValue("")]
		public string Seperator
		{
			get
			{
				return Options.GetOption("Seperator", System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator);
			}
			set
			{
				Options.SetOption("Seperator", value);
				OnModified(HaltTypes.Ask);
			}
		}

		[Browsable(true)]
		[ReadOnly(false)]
		[DisplayName("Truncate")]
		[Category("Settings")]
		[Description("When true, the CSVProcessor will truncate(empty) the target file whenever it runs Prepare(). When false, it will always append to the existing contents of the file")]
		[DefaultValue(true)]
		public bool TruncateOnPrepare
		{
			get
			{
				return bool.Parse(Options.GetOption("TruncateOnReset", "True"));
			}
			set
			{
				Options.SetOption("TruncateOnReset", value.ToString());
			}
		}

		#endregion Options

		#region Constructor

		public CSVProcessor()
			: base()
		{
			ExtensionRestriction.Add(".csv");
		}

		public CSVProcessor(Pipeline pipeline, string name)
			: base(pipeline, name)
		{
			AddInputChannel("1", false);
			ExtensionRestriction.Add(".csv");
		}

		#endregion Constructor

		#region Methods

		public override void Start()
		{
			base.Start();
			if (m_FileWriter != null)
			{
				m_FileWriter.Dispose();
			}
			if (m_Truncate)
			{
				FileStream.SetLength(0);
			}
			FileStream.Position = FileStream.Length;
			m_FileWriter = new StreamWriter(FileStream);
		}

		public override void Stop()
		{
			if (m_FileWriter != null)
			{
				try
				{
					m_FileWriter.Dispose();
				}
				catch
				{
				}
				m_FileWriter = null;
			}
			base.Stop();
		}

		protected override void Prepare()
		{
			base.Prepare();
			m_Truncate = TruncateOnPrepare;
		}

		protected override void Process()
		{
			int c = InputChannelCount;
			double[] vals = new double[c];
			for (int i = 0; i < c; i++)
			{
				m_FileWriter.Write(GetInputChannel((i + 1).ToString()).GetValue());
				if (i < c - 1)
				{
					m_FileWriter.Write(Seperator);
				}
			}
			m_FileWriter.WriteLine();
			m_FileWriter.Flush();
		}

		#endregion Methods
	}
}