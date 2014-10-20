using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualProcessors.Processing;

namespace VisualProcessors.Processors
{
	public class EmptyProcessor : Processor
	{
		public EmptyProcessor()
		{
		}

		public EmptyProcessor(Pipeline pipeline, string name)
			: base(pipeline, name)
		{
		}

		protected override void Prepare()
		{
			Console.WriteLine("EmptyProcessor.Prepare() " + Name);
		}

		protected override void Process()
		{
			Console.WriteLine("EmptyProcessor.Process() " + Name);
		}
	}
}