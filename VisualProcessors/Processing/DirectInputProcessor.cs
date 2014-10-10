using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using VisualProcessors.Controls;
using System.Globalization;

namespace VisualProcessors.Processing
{
	[ProcessorAttribute("Bram Kamies", "Writes a 0 to its OutputChannel, triggered via a clickable button", "", "Output",
		HideInputTab = true,
		SettingsTabLabel = "Input")]
	public class DirectInputProcessor : Processor
	{
		#region Properties

		#endregion Properties
		#region Options

		public double Value
		{
			get
			{
				return double.Parse(Options.GetOption("Value", "1"));
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", "Cannot be lower or equal to zero");
				}
				Options.SetOption("Value", value.ToString());
			}
		}

		#endregion Options


		#region Constructor

		public DirectInputProcessor()
		{
		}

		public DirectInputProcessor(Pipeline pipeline, string name)
			: base(pipeline, name)
		{
			AddInputChannel("DNC", false);
			AddOutputChannel("Output");
		}

		#endregion Constructor

		#region Methods

		public override void GetUserInterface(Panel panel)
		{
			Button button = new Button();
			button.Text = "Click me";
			button.Click += button_Click;
			button.Location = new Point((panel.Width - button.Width) / 2, (panel.Height - button.Height) / 2);
			button.Anchor = AnchorStyles.None;

			var valueInput = new DoubleInputPanel("Output value:");
			valueInput.Dock = DockStyle.Top;
			valueInput.InputText = Value.ToString();
			valueInput.InputCompleted+=valueInput_InputCompleted;
			valueInput.ToolTipText = "Use '" + CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator + "' as decimal seperator";


			panel.Controls.Add(valueInput);
			panel.Controls.Add(button);
			base.GetUserInterface(panel);
		}

		private void button_Click(object sender, EventArgs e)
		{
			GetOutputChannel("Output").WriteValue(Value);
		}

		private void valueInput_InputCompleted(object sender, EventArgs e)
		{
			Value = (sender as DoubleInputPanel).InputValue;
		}

		#endregion Methods
	}
}