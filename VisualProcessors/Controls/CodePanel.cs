using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualProcessors.Processing;
using System.IO;
using System.CodeDom.Compiler;

namespace VisualProcessors.Controls
{
	public partial class CodePanel : UserControl
	{
		#region Properties

		private UserCodeContext m_UserCodeContext;

		public CodeProcessor CodeProcessor { get; set; }
		

		#endregion Properties

		#region Constructor

		public CodePanel()
		{
			InitializeComponent();
		}

		public CodePanel(CodeProcessor cp,string usercode="")
			: this()
		{
			CodeProcessor = cp;
			CodeBox.Text = cp.Code;
			m_UserCodeContext = new UserCodeContext();
			if (usercode == "")
			{
				CodeBox.Text = m_UserCodeContext.MethodDeclaration + Environment.NewLine +
					"{" + Environment.NewLine +
					"\tvar a=processor.GetInputChannel(\"A\").GetValue();" + Environment.NewLine +
					"\tprocessor.GetOutputChannel(\"Output1\").WriteValue(a+1);" + Environment.NewLine +
					"}";
			}
			else
			{
				CodeBox.Text = usercode;
			}
		}

		#endregion Constructor

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
			ErrorList.Items.Clear();
			bool success = m_UserCodeContext.Compile();
			ApplyButton.Enabled = success;
			if (success)
			{
				ErrorList.Items.Add("No errors");
			}
			foreach (CompilerError error in m_UserCodeContext.CompileErrors)
			{
				string msg = "[Error:" + error.Line + "] " + error.ErrorText;
				ErrorList.Items.Add(msg);
			}
			foreach (CompilerError warning in m_UserCodeContext.CompileWarnings)
			{
				string msg = "[Warning:" + warning.Line + "] " + warning.ErrorText;
				ErrorList.Items.Add(msg);
			}
		}

		private void ApplyButton_Click(object sender, EventArgs e)
		{
			CodeProcessor.ProcessFunction = m_UserCodeContext.CompiledFunction;
			CodeProcessor.Code = CodeBox.Text;
		}

		#region Methods

		#endregion Methods
	}
}