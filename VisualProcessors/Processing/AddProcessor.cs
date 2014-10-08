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
	[ProcessorAttribute("Bram Kamies", "Adds two values together", "A", "Output")]
	public class AddProcessor : Processor
	{
		public AddProcessor()
		{
		}

		public AddProcessor(string name)
			: base(name)
		{
			AddInputChannel("A", false);
			AddInputChannel("B", false);
			AddOutputChannel("Output");
		}

		protected override void Process()
		{
			double a = GetInputChannel("A").GetValue();
			double b = GetInputChannel("B").GetValue();
			GetOutputChannel("Output").WriteValue(a + b);
		}
	}
}