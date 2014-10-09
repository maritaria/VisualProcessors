using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualProcessors;
using VisualProcessors.Controls;
using VisualProcessors.Forms;
using VisualProcessors.Processing;

namespace VisualProcessors.Controls
{
	public partial class StringInputPanel : UserControl
	{
		#region Properties

		private bool m_Changing = false;

		public string InputText
		{
			get
			{
				return InputTextBox.Text;
			}
			set
			{
				m_Changing = true;
				InputTextBox.Text = value;
				m_Changing = false;
			}
		}

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

		public bool IsInputValid { get; private set; }

		#endregion Properties

		#region Contructor

		public StringInputPanel()
		{
			m_Changing = true;
			InitializeComponent();
			IsInputValid = false;
			m_Changing = false;
		}
		public StringInputPanel(string title) : this()
		{
			InputTitle = title;
		}

		#endregion Contructor

		#region Event

		/// <summary>
		///  Invoked when the user has given valid input, and has pressed enter to confirm it.
		/// </summary>
		public event EventHandler InputCompleted;

		/// <summary>
		///  Invoked after the user presses enter in the textbox to confirm their input. Use the
		///  CancelEventArgs to cancel validation.
		/// </summary>
		public event CancelEventHandler RequestValidation;

		private void OnInputCompleted()
		{
			if (InputCompleted != null)
			{
				InputCompleted(this, EventArgs.Empty);
			}
		}

		private bool OnRequestValidation()
		{
			if (RequestValidation != null)
			{
				CancelEventArgs carg = new CancelEventArgs(false);
				RequestValidation(this, carg);
				return !carg.Cancel;
			}
			return true;
		}

		#endregion Event

		#region Event Handlers

		private void InputTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				e.SuppressKeyPress = true;
				if (!m_Changing && IsInputValid)
				{
					OnInputCompleted();
				}
			}
		}

		private void InputTextBox_TextChanged(object sender, EventArgs e)
		{
			if (!m_Changing)
			{
				IsInputValid = OnRequestValidation();
				InputTextBox.BackColor = IsInputValid ? Color.White : Color.Red;
			}
		}

		#endregion Event Handlers
	}
}