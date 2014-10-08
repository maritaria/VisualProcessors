using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualProcessors.Processing
{
	public class EmptyProcessor : Processor
	{
		public EmptyProcessor()
		{
		}
		public EmptyProcessor(string name) : base(name) 
		{
		}
		protected override bool Prepare()
		{
			Console.WriteLine("EmptyProcessor.Prepare() " + Name);
			return true;
		}
		protected override void Process()
		{
			Console.WriteLine("EmptyProcessor.Process() " + Name);
		}
	}
}
