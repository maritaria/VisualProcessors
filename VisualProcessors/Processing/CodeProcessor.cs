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

		public string Code { get; set; }

		public override Control GetUserInterface()
		{
			CodePanel cpanel = new CodePanel(this);
			return cpanel;
		}

		protected override void Process()
		{
			double a = GetInputChannel("A").GetValue();
			double b = GetInputChannel("B").GetValue();
			GetOutputChannel("Output").WriteValue(a + b);
		}
	}
}