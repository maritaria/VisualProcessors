using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using VisualProcessors.Controls;
using VisualProcessors.Processing;

namespace VisualProcessors.Processors
{
	[ProcessorMeta("Bram Kamies", "Writes a 0 to its OutputChannel, triggered via a clickable button", "", "Output",
		InputTabMode = ProcessorTabMode.Hidden,
		CustomTabTitle = "Input")]
	public class DirectInputProcessor : Processor
	{
		#region Properties

		#endregion Properties

		#region Options

		[Browsable(true)]
		[ReadOnly(false)]
		[DisplayName("Output value")]
		[Category("Settings")]
		[Description("The value to write to the OutputChannel")]
		[DefaultValue(0)]
		public double Value
		{
			get
			{
				return double.Parse(Options.GetOption("Value", "0"));
			}
			set
			{
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
			button.Text = "Write to output";
			button.Click += button_Click;
			button.Location = new Point((panel.Width - button.Width) / 2, (panel.Height - button.Height) / 2);
			button.Anchor = AnchorStyles.None;
			panel.Controls.Add(button);
		}

		private void button_Click(object sender, EventArgs e)
		{
			GetOutputChannel("Output").WriteValue(Value);
		}

		#endregion Methods
	}
}