using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace VisualProcessors.Processing
{
	public class DirectInputProcessor : Processor
	{
		public DirectInputProcessor()
		{
		}

		public DirectInputProcessor(string name)
			: base(name)
		{
			AddInputChannel("DNC");
			AddOutputChannel("Output");
		}

		public override Control GetUserInterface()
		{
			Button button = new Button();
			button.Text = "Click me";
			button.Click += button_Click;
			return button;
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