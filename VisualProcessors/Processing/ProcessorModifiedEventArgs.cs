using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualProcessors.Processing
{
	public class ProcessorModifiedEventArgs : EventArgs
	{
		#region Properties

		public bool ShouldHalt { get; private set; }

		public Processor Source { get; private set; }

		#endregion Properties

		#region Constructor

		public ProcessorModifiedEventArgs(Processor source, bool shouldHalt)
		{
			Source = source;
			ShouldHalt = shouldHalt;
		}

		#endregion Constructor
	}
}