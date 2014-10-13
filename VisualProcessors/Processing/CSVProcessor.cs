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

namespace VisualProcessors.Processing
{
	[ProcessorMeta("Bram Kamies", "Writes the input to a .csv file", "1", "",
		CustomTabMode = ProcessorTabMode.Hidden,
		OutputTabMode = ProcessorTabMode.Hidden)]
	public class CSVProcessor : Processor
	{
		#region Properties

		private StreamWriter m_FileWriter;
		private bool m_Loaded = false;
		private bool m_OutputSet = false;
		private bool m_Overwrite = true;

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
		[DisplayName("Output file")]
		[Category("Settings")]
		[Description("The file to write the input data to")]
		[DefaultValue("")]
		[Editor(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string FilePath
		{
			get
			{
				return Options.GetOption("FilePath", "");
			}
			set
			{
				m_OutputSet = false;
				ThrowExceptionOnInvalid(value);
				m_OutputSet = true;
				Options.SetOption("FilePath", value);
				OnModified(HaltTypes.ShouldHalt);
			}
		}

		[Browsable(true)]
		[ReadOnly(false)]
		[DisplayName("Column seperator")]
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
				OnModified(HaltTypes.AskHalt);
			}
		}

		#endregion Options

		#region Constructor

		public CSVProcessor()
			: base()
		{
			m_Loaded = true;
		}

		public CSVProcessor(Pipeline pipeline, string name)
			: base(pipeline, name)
		{
			AddInputChannel("1", false);
		}

		#endregion Constructor

		#region Methods

		public override void Dispose()
		{
			base.Dispose();
			if (m_FileWriter != null)
			{
				m_FileWriter.Dispose();
				m_FileWriter = null;
			}
		}

		public override void GetUserInterface(Panel panel)
		{
		}

		public override void Start()
		{
			base.Start();
			ThrowExceptionOnInvalid(FilePath);
			FileMode mode = FileMode.Append;
			if (m_Overwrite)
			{
				mode = FileMode.Create;
			}
			m_FileWriter = new StreamWriter(new FileStream(FilePath, mode));
		}

		public override void Stop()
		{
			base.Stop();
			DisposeFileWriter();
			m_Overwrite = false;
		}

		public void ThrowExceptionOnInvalid(string path)
		{
			if (path == "")
			{
				throw new Exception("No output file selected");
			}
			FileInfo info = new FileInfo(path);
			if (!info.Exists)
			{
				File.Create(path).Close();
				info = new FileInfo(path);
			}
			if (info.Extension != ".csv")
			{
				throw new Exception("File extension is invalid: must be *.csv");
			}
			if (info.Attributes.HasFlag(FileAttributes.ReadOnly) || info.IsReadOnly)
			{
				throw new Exception("The selected file is a read-only file");
			}
			if (info.Attributes.HasFlag(FileAttributes.Directory))
			{
				throw new Exception("The selected file is a directory");
			}
			if (info.Attributes.HasFlag(FileAttributes.System))
			{
				throw new Exception("The selected file is a system-only file");
			}
			if (info.Attributes.HasFlag(FileAttributes.Offline))
			{
				throw new Exception("The selected file is offline or unavailable");
			}
			if (info.Attributes.HasFlag(FileAttributes.Temporary))
			{
				//OnWarning("The selected file is a temporary file, it may be removed when the application closes");
			}
			if (info.Attributes.HasFlag(FileAttributes.Compressed))
			{
				//OnWarning("The selected file is compressed, unknown behaviour on read/write-operations");
			}

			//This will try to open the file, and immidiatly close it again.
			//This will throw an exception if the file is in use by another program.
			info.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None).Close();
		}

		protected override void Prepare()
		{
			if (m_Loaded)
			{
				if (FilePath != "")
				{
					m_OutputSet = true;
				}
				m_Loaded = false;
			}
			if (m_OutputSet)
			{
				m_Overwrite = true;
			}
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
					//System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator
					m_FileWriter.Write(Seperator);
				}
			}
			m_FileWriter.WriteLine();
			m_FileWriter.Flush();
		}

		protected override void WorkerMethod()
		{
			if (!m_OutputSet)
			{
				OnWarning("No output set");
			}
			else
			{
				base.WorkerMethod();
			}
		}

		private void DisposeFileWriter()
		{
			if (m_FileWriter != null)
			{
				m_FileWriter.Dispose();
				m_FileWriter = null;
			}
		}

		#endregion Methods
	}
}