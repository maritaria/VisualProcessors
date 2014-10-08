using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualProcessors.Controls;
using System.Drawing;

namespace VisualProcessors.Processing
{
	[ProcessorAttribute("Bram Kamies","Writes the input to a .csv file","Value1","WritePulse")]
	public class CSVProcessor : Processor
	{
		public CSVProcessor() :base() { }

		public CSVProcessor(string name) : base(name) { }

		public override void GetUserInterface(Panel panel)
		{
			base.GetUserInterface(panel);
			Panel p = new Panel();
			p.Dock = DockStyle.Top;
			Button b = new Button();
			b.Text = "Browse...";
			b.Dock = DockStyle.Right;
			Label l = new Label();
			l.Text = "Select output file";
			l.Dock = DockStyle.Fill;
			l.TextAlign = ContentAlignment.MiddleLeft;

			p.Controls.Add(b);

		}
	}
}
