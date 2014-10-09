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
	[ProcessorAttribute("Bram Kamies", "Allows user C# code to be executed during simulations", "Input1", "Output1",
		AllowOptionalInputs = true)]
	public class CodeProcessor : Processor
	{
		#region Properties

		public static string DefaultCode =
			"public static void Process(CodeProcessor processor)" + Environment.NewLine +
			"{" + Environment.NewLine + "\t" + Environment.NewLine + "}" + Environment.NewLine + Environment.NewLine +
			"public static void Prepare(CodeProcessor processor)" + Environment.NewLine +
			"{" + Environment.NewLine + "\t" + Environment.NewLine + "}" + Environment.NewLine;

		/// <summary>
		///  Gets or sets the code stored in the processor. To use the code, compile it into a
		///  function and store these in PrepareFunction and ProcessFunction.
		/// </summary>
		public string Code
		{
			get
			{
				return Options.GetOption("Code");
			}
			set
			{
				Options.SetOption("Code", value);
				OnModified(true);
			}
		}

		/// <summary>
		///  Gets or sets the function executed when the CodeProcessor needs to prepare for a new
		///  simulation
		/// </summary>
		public Action<CodeProcessor> PrepareFunction { get; set; }

		/// <summary>
		///  Gets or sets the function executed when the CodeProcessor needs to process data.
		/// </summary>
		public Action<CodeProcessor> ProcessFunction { get; set; }

		#endregion Properties

		#region Constructor

		public CodeProcessor()
		{
		}

		public CodeProcessor(Pipeline pipeline, string name)
			: base(pipeline, name)
		{
			AddInputChannel("Input1", false);
			AddInputChannel("Input2", true);
			AddInputChannel("Input3", true);
			AddInputChannel("Input4", true);
			AddInputChannel("Input5", true);
			AddOutputChannel("Output1");
			AddOutputChannel("Output2");
			AddOutputChannel("Output3");
			AddOutputChannel("Output4");
			AddOutputChannel("Output5");

			Code = DefaultCode;
		}

		#endregion Constructor

		#region Methods

		public override void GetUserInterface(Panel panel)
		{
			CodePanel cpanel = new CodePanel(this);
			cpanel.Dock = DockStyle.Fill;
			panel.Controls.Add(cpanel);
			base.GetUserInterface(panel);
		}

		protected override void Prepare()
		{
			base.Prepare();
			if (PrepareFunction != null)
			{
				try
				{
					PrepareFunction(this);
				}
				catch (Exception e)
				{
					OnError("The Prepare() function threw an exception!" + Environment.NewLine + e.Message);
				}
			}
		}

		protected override void Process()
		{
			if (ProcessFunction != null)
			{
				try
				{
					ProcessFunction(this);
				}
				catch (Exception e)
				{
					OnError("The Process() function threw an exception!" + Environment.NewLine + e.Message);
				}
			}
		}

		#endregion Methods
	}
}