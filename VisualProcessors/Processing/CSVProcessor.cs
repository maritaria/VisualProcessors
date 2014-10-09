using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualProcessors.Controls;
using System.Drawing;
using System.IO;

namespace VisualProcessors.Processing
{
	[ProcessorAttribute("Bram Kamies","Writes the input to a .csv file","Value1","WritePulse",
		HideOutputTab=true,DefaultInput="1")]
	public class CSVProcessor : Processor
	{
		#region Properties
		private bool m_OutputSet = false;
		private int m_ChannelCount = 1;
		private StreamWriter m_FileWriter;
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
			}
		}
		#endregion
		#region Constructor
		public CSVProcessor() : base() { }

		public CSVProcessor(Pipeline pipeline, string name)
			: base(pipeline, name)
		{
			AddInputChannel("1", false);
		}
		#endregion
		#region Methods

		public override void GetUserInterface(Panel panel)
		{
			Panel filePanel = new Panel();
			TextBox fileTextBox = new TextBox();
			Button fileBrowseButton = new Button();
			Button fileUnsetButton = new Button();

			Panel inputPanel = new Panel();
			Label inputTitle = new Label();
			NumericUpDown inputNumeric = new NumericUpDown();

			panel.Controls.Add(inputPanel);
			inputPanel.Controls.Add(inputNumeric);
			inputPanel.Controls.Add(inputTitle);

			panel.Controls.Add(filePanel);
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

			inputPanel.Dock = DockStyle.Top;
			inputPanel.Height = 26;
			inputTitle.Dock = DockStyle.Left;
			inputTitle.TextAlign = ContentAlignment.MiddleLeft;
			inputTitle.Text = "Number of channels:";
			inputTitle.Width = inputTitle.PreferredWidth;

			int center = inputPanel.Height / 2;
			inputNumeric.Location = new Point(inputTitle.Right, (inputPanel.Height - inputNumeric.Height) / 2);
			inputNumeric.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;
			inputNumeric.Width = 70;
			inputNumeric.ReadOnly = true;
			inputNumeric.Increment = 1;
			inputNumeric.Value = 1;
			inputNumeric.Minimum = 1;
			inputNumeric.ValueChanged += delegate(object sender, EventArgs e)
			{
				string[] names = GetInputChannelNames();
				while (names.Length > inputNumeric.Value)
				{
					RemoveInputChannel(GetInputChannel(GetInputChannelNames().Last()));
					names = GetInputChannelNames();
				}
				while(names.Length < inputNumeric.Value)
				{
					AddInputChannel((names.Length + 1).ToString(), false);
					names = GetInputChannelNames();
				}
				m_ChannelCount = names.Length;
			};

			base.GetUserInterface(panel);
		}
		public override void Start()
		{
			base.Start();
			FileMode mode = FileMode.Append;
			if (m_Overwrite)
			{
				mode = FileMode.Create;
			}
			m_FileWriter = new StreamWriter(new FileStream(FilePath,mode));
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
			for(int i = 0;i<m_ChannelCount;i++)
			{
				m_FileWriter.Write(GetInputChannel((i + 1).ToString()).GetValue());
				if (i < m_ChannelCount - 1)
				{
					m_FileWriter.Write(",");
				}
			}
			m_FileWriter.WriteLine();
		}

		public override void Stop()
		{
			base.Stop();
			DisposeFileWriter();
			m_Overwrite = false;
		}

		private void DisposeFileWriter()
		{
			if (m_FileWriter != null)
			{
				m_FileWriter.Dispose();
				m_FileWriter = null;
			}
		}

		public override void Dispose()
		{
			base.Dispose();
			if (m_FileWriter!=null)
			{
				m_FileWriter.Dispose();
				m_FileWriter = null;
			}
		}

		#endregion
	}
}
