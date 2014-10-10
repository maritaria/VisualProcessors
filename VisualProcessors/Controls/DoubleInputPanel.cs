using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace VisualProcessors.Controls
{
	public partial class DoubleInputPanel : StringInputPanel
	{
		#region Properties

		public double InputValue
		{
			get
			{
				double i = 0;
				if (IsInputValid && double.TryParse(InputText, out i))
				{
					return i;
				}
				return double.MaxValue;
			}
		}

		#endregion Properties

		#region Constructor

		public DoubleInputPanel()
			: base()
		{
			InitializeComponent();
		}

		public DoubleInputPanel(string name)
			: base(name)
		{
			InitializeComponent();
			RequestValidation += DoubleInputPanel_RequestValidation;
			ToolTipText = "Use '" + CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator + "' as decimal seperator";
		}

		#endregion Constructor

		#region EventHandlers

		private void DoubleInputPanel_RequestValidation(object sender, CancelEventArgs e)
		{
			double i = 0;
			e.Cancel = !double.TryParse(InputText, out i);
		}

		#endregion EventHandlers
	}
}