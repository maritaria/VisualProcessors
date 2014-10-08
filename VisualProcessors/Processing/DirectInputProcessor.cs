using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace VisualProcessors.Processing
{
	[ProcessorAttribute("Bram Kamies", "Writes a 0 to its OutputChannel, triggered via a clickable button", "", "Output",
		HideInputTab = true)]
	public class DirectInputProcessor : Processor
	{
		public DirectInputProcessor()
		{
		}

		public DirectInputProcessor(string name)
			: base(name)
		{
			AddInputChannel("DNC", false);
			AddOutputChannel("Output");
		}

		public override void GetUserInterface(Panel panel)
		{
			Button button = new Button();
			button.Text = "Click me";
			button.Click += button_Click;
			button.Location = new Point((panel.Width - button.Width) / 2, (panel.Height - button.Height) / 2);
			button.Anchor = AnchorStyles.None;
			panel.Controls.Add(button);
			base.GetUserInterface(panel);
		}

		protected override void Process()
		{
			GetOutputChannel("Output").WriteValue(0);
		}

		private void button_Click(object sender, EventArgs e)
		{
			Process();
		}
	}
}