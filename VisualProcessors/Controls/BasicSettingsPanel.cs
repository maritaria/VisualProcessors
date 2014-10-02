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
	public partial class BasicSettingsPanel : UserControl
	{
		#region Properties

		private bool m_Changing = false;
		private Processor m_Processor;

		public Processor Processor
		{
			get
			{
				return m_Processor;
			}
			set
			{
				SetProcessor(value);
			}
		}

		#endregion Properties

		#region Contructor

		public BasicSettingsPanel()
		{
			InitializeComponent();
		}

		#endregion Contructor

		#region Methods

		public void SetProcessor(Processor value)
		{
			m_Changing = true;
			m_Processor = value;
			if (m_Processor != null)
			{
				NameTextBox.Text = m_Processor.Name;
				NameTextBox.Enabled = true;
			}
			else
			{
				NameTextBox.Text = "Processor is null";
				NameTextBox.Enabled = false;
			}
			m_Changing = false;
		}

		#endregion Methods

		#region Event Handlers

		private void NameTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (!m_Changing)
			{
				if (e.KeyCode == Keys.Enter)
				{
					e.SuppressKeyPress = true;
					Processor.Name = NameTextBox.Text;
				}
			}
		}

		#endregion Event Handlers
	}
}