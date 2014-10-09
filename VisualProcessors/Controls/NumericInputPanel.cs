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
	public partial class NumericInputPanel : UserControl
	{
		#region Properties

		private bool m_Changing = false;

		public decimal InputIncrement
		{
			get
			{
				return InputNumeric.Increment;
			}
			set
			{
				InputNumeric.Increment = value;
			}
		}

		public decimal InputMaximum
		{
			get
			{
				return InputNumeric.Maximum;
			}
			set
			{
				InputNumeric.Maximum = value;
			}
		}

		public decimal InputMinimum
		{
			get
			{
				return InputNumeric.Minimum;
			}
			set
			{
				InputNumeric.Minimum = value;
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
				int oldright = InputNumeric.Right;
				InputLabel.Width = InputLabel.PreferredWidth;
				InputNumeric.Left = InputLabel.Right;
				InputNumeric.Width = oldright - InputNumeric.Left;
			}
		}

		public decimal InputValue
		{
			get
			{
				return InputNumeric.Value;
			}
			set
			{
				m_Changing = true;
				InputNumeric.Value = value;
				m_Changing = false;
			}
		}

		public bool IsInputValid { get; private set; }

		#endregion Properties

		#region Contructor

		public NumericInputPanel()
		{
			m_Changing = true;
			InitializeComponent();
			IsInputValid = false;
			m_Changing = false;
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

		private void InputNumeric_ValueChanged(object sender, EventArgs e)
		{
			if (!m_Changing)
			{
				IsInputValid = OnRequestValidation();
				InputNumeric.BackColor = IsInputValid ? Color.White : Color.Red;
				if (IsInputValid)
				{
					InputNumeric.BackColor = Color.White;
					OnInputCompleted();
				}
				else
				{
					InputNumeric.BackColor = Color.Red;
				}
			}
		}

		#endregion Event Handlers
	}
}