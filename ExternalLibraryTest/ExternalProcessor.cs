using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualProcessors.Processing;

namespace ExternalLibraryTest
{
	[ProcessorAttribute("Bram Kamies", "This processor is loaded from an external assembly", "Input", "Output")]
	public class ExternalProcessor : Processor
	{
		public ExternalProcessor()
		{
		}

		public ExternalProcessor(Pipeline p, string name)
			: base(p,name)
		{
			AddInputChannel("Input", false);
			AddOutputChannel("Output");
		}

		protected override void Process()
		{
			GetOutputChannel("Output").WriteValue(GetInputChannel("Input").GetValue());
		}
	}
}