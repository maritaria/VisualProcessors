using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualProcessors.Processing
{
	public enum ProcessorTabMode
	{
		Normal,
		Hidden,
		Custom,
	}

	public enum ProcessorTabs
	{
		Input,
		Properties,
		Output,
		Custom,
	}

	[AttributeUsage(AttributeTargets.Class)]
	public class ProcessorMeta : System.Attribute
	{
		public static ProcessorMeta Default = new ProcessorMeta("Unknown", "Unknown", "Unknown", "Unknown");

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
		///  Determines the display of the 'Custom' TabPage on the ProcessorForm of this type of
		///  Processor
		/// </summary>
		public ProcessorTabMode CustomTabMode = ProcessorTabMode.Normal;

		/// <summary>
		///  Determines the title of the 'Custom' tab
		/// </summary>
		public string CustomTabTitle = "Custom";

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
		///  Determines the display of the 'Input' TabPage on the ProcessorForm of this type of
		///  Processor
		/// </summary>
		public ProcessorTabMode InputTabMode = ProcessorTabMode.Normal;

		/// <summary>
		///  Determines the title of the 'Input' tab
		/// </summary>
		public string InputTabTitle;

		/// <summary>
		///  Determines the display of the 'Output' TabPage on the ProcessorForm of this type of
		///  Processor
		/// </summary>
		public ProcessorTabMode OutputTabMode = ProcessorTabMode.Normal;

		/// <summary>
		///  Determines the title of the 'Output' tab
		/// </summary>
		public string OutputTabTitle;

		/// <summary>
		///  Determines the display of the 'Properties' TabPage on the ProcessorForm of this type of
		///  Processor
		/// </summary>
		public ProcessorTabMode PropertiesTabMode = ProcessorTabMode.Normal;

		/// <summary>
		///  Determines the title of the 'Properties' tab
		/// </summary>
		public string PropertiesTabTitle = "Properties";

		public ProcessorMeta(string author, string description, string defaultinput, string defaultoutput)
		{
			Author = author;
			Description = description;
			DefaultInput = defaultinput;
			DefaultOutput = defaultoutput;
		}
	}
}