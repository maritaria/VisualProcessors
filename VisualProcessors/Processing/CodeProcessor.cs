using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using VisualProcessors.Controls;

namespace VisualProcessors.Processing
{
	public class CodeProcessor : Processor
	{
		public CodeProcessor()
		{
		}

		public CodeProcessor(string name)
			: base(name)
		{
			AddInputChannel("A", false);
			AddInputChannel("B", true);
			AddInputChannel("C", true);
			AddInputChannel("D", true);
			AddInputChannel("E", true);
			AddOutputChannel("Output1");
			AddOutputChannel("Output2");
			AddOutputChannel("Output3");

			Code = "//Your code here";
		}

		public override bool AllowOptionalChannels
		{
			get
			{
				return true;
			}
		}

		public string Code { get; set; }

		public Action<CodeProcessor> ProcessFunction { get; set; }

		public override void GetUserInterface(Panel panel)
		{
			CodePanel cpanel = new CodePanel(this);
			cpanel.Dock = DockStyle.Fill;
			panel.Controls.Add(cpanel);
			base.GetUserInterface(panel);
		}

		protected override void Process()
		{
			if (ProcessFunction != null)
			{
				ProcessFunction(this);
			}
		}
	}
}