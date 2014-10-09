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

		private int m_ChannelCount = 1;
		private StreamWriter m_FileWriter;
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
				OnModified(false);
			}
		}

		#endregion Properties

		#region Constructor

		public CSVProcessor()
			: base()
		{
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

			NumericInputPanel channelInput = new NumericInputPanel();
			channelInput.Dock = DockStyle.Top;
			channelInput.InputTitle = "Columns:";
			channelInput.InputMinimum = 1;
			channelInput.InputMaximum = 100;
			channelInput.InputIncrement = 1;
			channelInput.InputCompleted += channelCountInput_InputCompleted;

			panel.Controls.Add(channelInput);

#warning Make this a new InputPanel
			Panel filePanel = new Panel();
			TextBox fileTextBox = new TextBox();
			Button fileBrowseButton = new Button();
			Button fileUnsetButton = new Button();
			filePanel.Controls.Add(fileBrowseButton);
			filePanel.Controls.Add(fileUnsetButton);
			filePanel.Controls.Add(fileTextBox);

			filePanel.Dock = DockStyle.Top;
			filePanel.Height = 23;
			fileTextBox.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
			fileTextBox.Text = "Select output file";
			fileTextBox.ReadOnly = true;
			fileBrowseButton.Text = "Browse...";
			fileBrowseButton.Dock = DockStyle.Right;
			fileUnsetButton.Text = "Unset";
			fileUnsetButton.Dock = DockStyle.Right;
			fileUnsetButton.Enabled = false;
			fileTextBox.Width = fileBrowseButton.Left - fileTextBox.Left - 2;
			fileTextBox.Location = new Point(fileTextBox.Left, 2);
			fileBrowseButton.Click += delegate(object sender, EventArgs e)
			{
				SaveFileDialog sfd = new SaveFileDialog();
				sfd.Filter = "CSV-file|*.csv";
				sfd.InitialDirectory = Application.StartupPath;
				sfd.CheckPathExists = true;
				sfd.CheckFileExists = false;
				DialogResult result = sfd.ShowDialog();
				if (result == DialogResult.OK)
				{
					fileTextBox.Text = sfd.FileName;
					FilePath = sfd.FileName;
					m_OutputSet = true;
					m_Overwrite = true;
					fileUnsetButton.Enabled = true;
				}
			};
			fileUnsetButton.Click += delegate(object sender, EventArgs e)
			{
				fileTextBox.Text = "Select output file";
				m_OutputSet = false;
				fileUnsetButton.Enabled = false;
			};
			panel.Controls.Add(filePanel);
		
			base.GetUserInterface(panel);
		}

		void channelCountInput_InputCompleted(object sender, EventArgs e)
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
			m_ChannelCount = InputChannelCount;
		}

		public override void Start()
		{
			base.Start();
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
			if (m_OutputSet)
			{
				m_Overwrite = true;
			}
		}

		protected override void Process()
		{
			double[] vals = new double[m_ChannelCount];
			for (int i = 0; i < m_ChannelCount; i++)
			{
				m_FileWriter.Write(GetInputChannel((i + 1).ToString()).GetValue());
				if (i < m_ChannelCount - 1)
				{
					m_FileWriter.Write(",");
				}
			}
			m_FileWriter.WriteLine();
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