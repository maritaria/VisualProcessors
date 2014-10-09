using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualProcessors.Processing;

namespace VisualProcessors.Controls
{
	public partial class CodePanel : UserControl
	{
		#region Properties

		private bool m_Applied = false;
		private bool m_CodeChanged = false;
		private UserCodeContext m_UserCodeContext;
		private CodeProcessor m_CodeProcessor;
		public CodeProcessor CodeProcessor
		{
			get
			{
				return m_CodeProcessor;
			}
			set
			{
				m_CodeProcessor = value;
				CodeBox.Text = m_CodeProcessor.Code;
			}
		}

		#endregion Properties

		#region Constructor

		public CodePanel()
		{
			InitializeComponent();
			SaveCodeDialog.InitialDirectory = Application.StartupPath;
			LoadCodeDialog.InitialDirectory = Application.StartupPath;
			m_UserCodeContext = new UserCodeContext();
		}

		public CodePanel(CodeProcessor cp)
			: this()
		{
			CodeProcessor = cp;
		}

		#endregion Constructor

		#region Methods

		#endregion Methods

		#region Event Handlers

		private void ApplyButton_Click(object sender, EventArgs e)
		{
			if (!m_Applied)
			{
				CodeProcessor.ProcessFunction = m_UserCodeContext.ProcessFunction;
				CodeProcessor.PrepareFunction = m_UserCodeContext.PrepareFunction;
				CodeProcessor.Code = CodeBox.Text;
				ErrorList.Items.Add("[Processor Updated]");
				ApplyButton.Enabled = false;
				m_Applied = true;
			}
		}

		private void CodeBox_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				string path = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
				FileInfo fi = new FileInfo(path);
				if (fi.Length > 30000)
				{
					CodeBox.Text = "Error: file is larger then 30kb";
				}
				CodeBox.Text = File.ReadAllText(path);
			}
			else
			{
				CodeBox.Text = (string)e.Data.GetData(DataFormats.Text);
			}
		}

		private void CodeBox_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop) || e.Data.GetDataPresent(DataFormats.Text))
			{
				e.Effect = DragDropEffects.Copy;
			}
		}

		private void CompileButton_Click(object sender, EventArgs e)
		{
			m_UserCodeContext.UserCode = CodeBox.Text;
			bool success = m_UserCodeContext.Compile();
			ErrorList.Items.Clear();
			CompileButton.Enabled = false;
			ApplyButton.Enabled = success;
			m_CodeChanged = false;
			m_Applied = false;
			if (success)
			{
				ErrorList.Items.Add("[Compiled Succesfully]");
				CompileButton.Enabled = false;
			}
			foreach (CompilerError error in m_UserCodeContext.CompileErrors)
			{
				string msg = "Error:" + error.Line + " " + error.ErrorText;
				ErrorList.Items.Add(msg);
			}
			foreach (CompilerError warning in m_UserCodeContext.CompileWarnings)
			{
				string msg = "Warning:" + warning.Line + " " + warning.ErrorText;
				ErrorList.Items.Add(msg);
			}
		}

		private void LoadButton_Click(object sender, EventArgs e)
		{
			LoadCodeDialog.ShowDialog();
		}

		private void LoadCodeDialog_FileOk(object sender, CancelEventArgs e)
		{
			if (!e.Cancel)
			{
				Stream s = LoadCodeDialog.OpenFile();
				StreamReader reader = new StreamReader(s);
				CodeBox.Text = reader.ReadToEnd();
				reader.Close();
				reader.Dispose();
			}
		}

		private void SaveButton_Click(object sender, EventArgs e)
		{
			if (CodeBox.Text != "")
			{
				SaveCodeDialog.ShowDialog();
			}
		}

		private void SaveCodeDialog_FileOk(object sender, CancelEventArgs e)
		{
			if (!e.Cancel)
			{
				Stream s = SaveCodeDialog.OpenFile();
				StreamWriter writer = new StreamWriter(s);
				writer.Write(CodeBox.Text);
				writer.Close();
				writer.Dispose();
			}
		}

		#endregion Event Handlers

		private void CodeBox_TextChanged(object sender, EventArgs e)
		{
			if (!m_CodeChanged)
			{
				CompileButton.Enabled = true;
				ApplyButton.Enabled = false;
				m_CodeChanged = true;
				m_Applied = false;
				ErrorList.Items.Add("[Code modified]");
			}
		}

		private void DefaultCodeButton_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to replace your code with the default code?", "Restore default code", MessageBoxButtons.OKCancel) == DialogResult.OK)
			{
				CodeBox.Text = CodeProcessor.DefaultCode;
			}
		}
	}
}