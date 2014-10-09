using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualProcessors.Processing
{
	[AttributeUsage(AttributeTargets.Class)]
	public class ProcessorAttribute : System.Attribute
	{
		public static ProcessorAttribute Default = new ProcessorAttribute("Unknown", "Unknown", "Unknown", "Unknown");

		/// <summary>
		///  Determines whether the input channels are allowed to be optional
		/// </summary>
		public bool AllowOptionalInputs = false;

		/// <summary>
		///  Determines whether the user can spawn this processor on demand
		/// </summary>
		public bool AllowUserSpawn = true;

		/// <summary>
		///  Determines the author of the processor
		/// </summary>
		public string Author;

		/// <summary>
		///  Determines the name of the default/primary InputChannel
		/// </summary>
		public string DefaultInput;

		/// <summary>
		///  Determines the name of the default/primary OutputChannel
		/// </summary>
		public string DefaultOutput;

		/// <summary>
		///  A short description of the processor
		/// </summary>
		public string Description;

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

		/// <summary>
		///  Determines the title of the 'Input' tab
		/// </summary>
		public string InputTabLabel = "Input";

		/// <summary>
		///  Determines the title of the 'Output' tab
		/// </summary>
		public string OutputTabLabel = "Output";

		/// <summary>
		///  Determines the title of the 'Settings' tab
		/// </summary>
		public string SettingsTabLabel = "Settings";

		public ProcessorAttribute(string author, string description, string defaultinput, string defaultoutput)
		{
			Author = author;
			Description = description;
			DefaultInput = defaultinput;
			DefaultOutput = defaultoutput;
		}
	}
}