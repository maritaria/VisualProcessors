using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualProcessors.Processing
{
	public enum HaltTypes
	{
		Continue,
		ShouldHalt,
		Ask,
	}

	public class ProcessorModifiedEventArgs : EventArgs
	{
		#region Properties

		public HaltTypes HaltType { get; private set; }

		public Processor Source { get; private set; }

		#endregion Properties

		#region Constructor

		public ProcessorModifiedEventArgs(Processor source, HaltTypes haltType)
		{
			Source = source;
			HaltType = haltType;
		}

		#endregion Constructor
	}
}