using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualProcessors.Controls;

namespace VisualProcessors.Processing
{
	[ProcessorAttribute("Bram Kamies", "Writes the input to a .csv file", "Value1", "WritePulse",
		HideOutputTab = true, DefaultInput = "1")]
	public class CSVProcessor : Processor
	{
		#region Properties

		private StreamWriter m_FileWriter;
		private bool m_Loaded = false;
		private bool m_OutputSet = false;
		private bool m_Overwrite = true;

		public string FilePath
		{
			get
			{
				return Options.GetOption("FilePath");
			}
			set
			{
				Options.SetOption("FilePath", value);
				OnModified(true);
			}
		}

		public string Seperator
		{
			get
			{
				return Options.GetOption("Seperator");
			}
			set
			{
				Options.SetOption("Seperator", value);
				OnModified(false);
			}
		}

		#endregion Properties

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
			FilePath = "";
			Seperator = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator;
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
			NumericInputPanel channelInput = new NumericInputPanel();
			channelInput.Dock = DockStyle.Top;
			channelInput.InputTitle = "Columns:";
			channelInput.InputMinimum = 1;
			channelInput.InputMaximum = 100;
			channelInput.InputIncrement = 1;
			channelInput.InputCompleted += channelCountInput_InputCompleted;

			SaveFileDialog fileDialog = new SaveFileDialog();
			fileDialog.Filter = "CSV-file|*.csv";
			fileDialog.FileName = Application.StartupPath;
			FilepathInputPanel fileInput = new FilepathInputPanel("Output file:", fileDialog);
			fileInput.Dock = DockStyle.Top;
			fileInput.BrowseComplete += fileInput_BrowseComplete;
			fileInput.CanBrowse += fileInput_CanBrowse;

			StringInputPanel seperatorInput = new StringInputPanel("Seperator:");
			seperatorInput.Dock = DockStyle.Top;
			seperatorInput.InputText = Seperator;
			seperatorInput.InputCompleted += seperatorInput_InputCompleted;

			panel.Controls.Add(seperatorInput);
			panel.Controls.Add(channelInput);
			panel.Controls.Add(fileInput);
			base.GetUserInterface(panel);
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

		private void ThrowExceptionOnInvalid(string path)
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
				OnWarning("The selected file is a temporary file, it may be removed when the application closes");
			}
			if (info.Attributes.HasFlag(FileAttributes.Compressed))
			{
				OnWarning("The selected file is compressed, unknown behaviour on read/write-operations");
			}

			//This will try to open the file, and immidiatly close it again.
			//This will throw an exception if the file is in use by another program.
			info.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None).Close();
		}

		#endregion Methods

		#region Event Handlers

		private void channelCountInput_InputCompleted(object sender, EventArgs e)
		{
			int wanted = (int)(sender as NumericInputPanel).InputValue;
			int count = InputChannelCount;
			while (count > wanted)
			{
				RemoveInputChannel(GetInputChannel(count.ToString()));
				count = InputChannelCount;
			}
			while (count < wanted)
			{
				AddInputChannel((count + 1).ToString(), false);
				count = InputChannelCount;
			}
		}

		private void fileInput_BrowseComplete(object sender, FilepathEventArgs e)
		{
			e.AcceptPath = true;
			try
			{
				ThrowExceptionOnInvalid(e.FilePath);
				FilePath = e.FilePath;
				e.AcceptPath = true;
				m_OutputSet = true;
			}
			catch (Exception ex)
			{
				e.AcceptPath = false;
				m_OutputSet = false;
				OnError(ex.Message);
			}
		}

		private void fileInput_CanBrowse(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (IsRunning)
			{
				DialogResult result = MessageBox.Show("If you want to change the output file, the simulation has to pause, are you sure you want to pause the simulation?", "Pause required", MessageBoxButtons.YesNo);
				e.Cancel = (result == DialogResult.Yes);//OnModified() will be called when the target file changes
			}
		}

		private void seperatorInput_InputCompleted(object sender, EventArgs e)
		{
			Seperator = (sender as StringInputPanel).InputText;
		}

		#endregion Event Handlers
	}
}