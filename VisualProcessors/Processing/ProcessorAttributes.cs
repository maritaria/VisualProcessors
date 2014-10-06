using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualProcessors.Processing
{
	[AttributeUsage(AttributeTargets.Class)]
	public class ProcessorMeta : System.Attribute
	{
		/// <summary>
		///  Determines whether the input channels are allowed to be optional
		/// </summary>
		public bool AllowOptionalInputs = true;

		/// <summary>
		///  Determines whether the user can spawn this processor on demand
		/// </summary>
		public bool AllowUserSpawn = true;

		/// <summary>
		///  Determines the author of the processor
		/// </summary>
		public string Author = "This class probably has no ProcessorMeta Attribute";

		/// <summary>
		///  Determines the name of the default/primary InputChannel
		/// </summary>
		public string DefaultInput = "";

		/// <summary>
		///  Determines the name of the default/primary OutputChannel
		/// </summary>
		public string DefaultOutput = "";

		/// <summary>
		///  A short description of the processor
		/// </summary>
		public string Description = "This class probably has no ProcessorMeta Attribute";

		/// <summary>
		///  Determines whether to hide the 'Input' TabPage on the ProcessorForm for this type of
		///  Processor
		/// </summary>
		public bool HideInputTab = false;

		/// <summary>
		///  Determines whether to hide the 'Output' TabPage on the ProcessorForm for this type of
		///  Processor
		/// </summary>
		public bool HideOutputTab = false;

		/// <summary>
		///  Determines whether to hide the 'Settings' TabPage on the ProcessorForm for this type of
		///  Processor
		/// </summary>
		public bool HideSettingsTab = false;

		public ProcessorMeta()
		{
		}
	}
}