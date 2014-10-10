using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualProcessors.Controls
{
	public partial class IntegerInputPanel : StringInputPanel
	{
		#region Properties

		public int InputValue
		{
			get
			{
				int i = 0;
				if (IsInputValid && int.TryParse(InputText, out i))
				{
					return i;
				}
				return int.MinValue;
			}
		}

		#endregion Properties

		#region Constructor

		public IntegerInputPanel()
			: base()
		{
			InitializeComponent();
		}

		public IntegerInputPanel(string name)
			: base(name)
		{
			RequestValidation += NumberInputPanel_RequestValidation;
		}

		#endregion Constructor

		#region Event Handlers

		private void NumberInputPanel_RequestValidation(object sender, CancelEventArgs e)
		{
			int i = 0;
			e.Cancel = !int.TryParse(InputText, out i);
		}

		#endregion Event Handlers
	}
}