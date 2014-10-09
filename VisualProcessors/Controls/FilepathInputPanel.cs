using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualProcessors.Controls
{
	public partial class FilepathInputPanel : UserControl
	{
		#region Properties

		public FileDialog BrowseDialog { get; set; }

		public string InputTitle
		{
			get
			{
				return InputLabel.Text;
			}
			set
			{
				InputLabel.Text = value;
				int oldright = InputTextBox.Right;
				InputLabel.Width = InputLabel.PreferredWidth;
				InputTextBox.Left = InputLabel.Right;
				InputTextBox.Width = oldright - InputTextBox.Left;
			}
		}

		#endregion Properties

		#region Constructor

		public FilepathInputPanel()
		{
			InitializeComponent();
		}

		public FilepathInputPanel(string title, FileDialog browseDialog)
			: this()
		{
			InputTitle = title;
			BrowseDialog = browseDialog;
		}

		#endregion Constructor

		private void BrowseButton_Click(object sender, EventArgs e)
		{
			if (OnCanBrowse())
			{
				if (BrowseDialog.ShowDialog() == DialogResult.OK)
				{
					OnBrowseComplete(new FilepathEventArgs(BrowseDialog.FileName));
				}
			}
		}

		#region Methods

		#endregion Methods

		#region Events

		/// <summary>
		///  Invoked when the player has finished with the FileDialog brought up by the 'Browse'
		///  button.
		/// </summary>
		public event EventHandler<FilepathEventArgs> BrowseComplete;

		public event CancelEventHandler CanBrowse;

		private void OnBrowseComplete(FilepathEventArgs e)
		{
			if (BrowseComplete != null)
			{
				BrowseComplete(this, e);
				if (e.AcceptPath)
				{
					InputTextBox.BackColor = Color.White;
				}
				else
				{
					InputTextBox.BackColor = Color.Red;
				}
			}
			else
			{
				InputTextBox.BackColor = Color.Orange;
			}
			InputTextBox.Text = e.FilePath;
		}

		private bool OnCanBrowse()
		{
			if (CanBrowse != null)
			{
				CancelEventArgs e = new CancelEventArgs(false);
				CanBrowse(this, e);
				return !e.Cancel;
			}
			return true;
		}

		#endregion Events

		private void InputTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				e.SuppressKeyPress = true;
				OnBrowseComplete(new FilepathEventArgs(InputTextBox.Text));
			}
		}

		#region Event Handlers

		#endregion Event Handlers
	}

	#region FilepathEventArgs class

	public class FilepathEventArgs : EventArgs
	{
		public FilepathEventArgs(string path)
		{
			FilePath = path;
			AcceptPath = true;
		}

		public bool AcceptPath { get; set; }

		public string FilePath { get; private set; }
	}

	#endregion FilepathEventArgs class
}