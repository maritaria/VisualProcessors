using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using VisualProcessors.Controls;

namespace VisualProcessors.Processing
{
	[ProcessorMeta("Bram Kamies", "Allows user C# code to be executed during simulations", "Input1", "Output1",
		AllowOptionalInputs = true,
		CustomTabTitle = "Code")]
	public class CodeProcessor : Processor
	{
		#region Properties

		[Browsable(false)]
		public static string DefaultCode =
			"public static void Process(CodeProcessor processor)" + Environment.NewLine +
			"{" + Environment.NewLine + "\t" + Environment.NewLine + "}" + Environment.NewLine + Environment.NewLine +
			"public static void Prepare(CodeProcessor processor)" + Environment.NewLine +
			"{" + Environment.NewLine + "\t" + Environment.NewLine + "}" + Environment.NewLine;

		private List<Assembly> m_Assemblies = new List<Assembly>();
		private string m_ChachedCode = "";
		private List<CompilerError> m_Errors = new List<CompilerError>();
		private bool m_IsCompiled;
		private List<string> m_Usings = new List<string>();
		private List<CompilerError> m_Warnings = new List<CompilerError>();

		[Browsable(false)]
		public List<Assembly> Assemblies
		{
			get
			{
				return m_Assemblies;
			}
		}

		[Browsable(false)]
		public string Classname
		{
			get
			{
				return "UserCode";
			}
		}

		[Browsable(false)]
		public List<CompilerError> Errors
		{
			get
			{
				return m_Errors;
			}
		}

		[Browsable(false)]
		public string Namespace
		{
			get
			{
				return "VisualProcessors.UserCode";
			}
		}

		/// <summary>
		///  Gets or sets the function executed when the CodeProcessor needs to prepare for a new
		///  simulation
		/// </summary>
		[Browsable(false)]
		public Action<CodeProcessor> PrepareFunction { get; set; }

		/// <summary>
		///  Gets or sets the function executed when the CodeProcessor needs to process data.
		/// </summary>
		[Browsable(false)]
		public Action<CodeProcessor> ProcessFunction { get; set; }

		[Browsable(false)]
		public List<CompilerError> Warnings
		{
			get
			{
				return m_Warnings;
			}
		}

		#endregion Properties

		#region Options

		/// <summary>
		///  Gets or sets the code stored in the processor. To use the code, compile it into a
		///  function and store these in PrepareFunction and ProcessFunction.
		/// </summary>
		[Browsable(false)]
		public string Code
		{
			get
			{
				return Options.GetOption("Code", DefaultCode);
			}
			set
			{
				Options.SetOption("Code", value);
				OnModified(HaltTypes.AskHalt);
			}
		}

		[Browsable(true)]
		[ReadOnly(true)]
		[DisplayName("Using statements")]
		[Category("Settings")]
		[Description("Sets the using statements for the code")]
		[Editor(@"System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
			typeof(System.Drawing.Design.UITypeEditor))]
		public List<string> Usings
		{
			get
			{
				return m_Usings;
			}
		}

		#endregion Options

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

			Usings.Add("System");
			Usings.Add("System.Threading");
			Usings.Add("VisualProcessors.Processing");
			Assemblies.Add(Assembly.GetAssembly(typeof(int)));//mscorlib
			Assemblies.Add(Assembly.GetAssembly(typeof(System.Xml.XmlAttribute)));//System.Xml namespace
			Assemblies.AddRange(Program.ProcessorAssemblies);
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
			if (!m_IsCompiled)
			{
				throw new Exception("Uncompiled code");
			}
			if (m_ChachedCode != Code)
			{
				OnWarning("Code changed");
			}
			base.Prepare();
			if (PrepareFunction != null)
			{
				try
				{
					PrepareFunction(this);
				}
				catch (Exception e)
				{
					throw new Exception("The Prepare() function threw an exception!" + Environment.NewLine + e.Message);
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

		#region Compiling

		public bool Compile()
		{
			m_IsCompiled = false;
			m_ChachedCode = Code;
			m_Errors.Clear();
			PrepareFunction = null;
			ProcessFunction = null;
			CSharpCodeProvider provider = new CSharpCodeProvider();
			CompilerParameters parameters = new CompilerParameters();
			foreach (Assembly asm in Assemblies)
			{
				parameters.ReferencedAssemblies.Add(asm.Location);
			}
			parameters.GenerateInMemory = true;
			parameters.GenerateExecutable = false;

			//Compile
			CompilerResults results = provider.CompileAssemblyFromSource(parameters, GenerateCode(Code));

			//Report errors and warnings
			if (results.Errors.HasErrors)
			{
				foreach (CompilerError error in results.Errors)
				{
					if (error.IsWarning)
					{
						Warnings.Add(error);
					}
					else
					{
						Errors.Add(error);
					}
				}
				return false;
			}
			foreach (CompilerError error in results.Errors)
			{
				if (error.IsWarning)
				{
					Errors.Add(error);
				}
			}

			//Bind function
			Assembly assembly = results.CompiledAssembly;
			Type program = assembly.GetType(Namespace + "." + Classname);
			MethodInfo process = program.GetMethod("Process");
			if (process == null)
			{
				Errors.Add(new CompilerError("userinput", 1, 1, "0", "No function 'Process' is defined"));
				return false;
			}
			else
			{
				ProcessFunction = (Action<CodeProcessor>)Delegate.CreateDelegate(typeof(Action<CodeProcessor>), process);
			}
			MethodInfo prepare = program.GetMethod("Prepare");
			if (prepare == null)
			{
				Warnings.Add(new CompilerError("userinput", 1, 1, "0", "No function 'Prepare' is defined"));
			}
			else
			{
				PrepareFunction = (Action<CodeProcessor>)Delegate.CreateDelegate(typeof(Action<CodeProcessor>), prepare);
			}
			m_IsCompiled = true;
			return true;
		}

		private string GenerateCode(string usercode)
		{
			string result = "";
			foreach (string use in Usings)
			{
				result += "using " + use + ";";
			}
			string classcode = "class " + Classname + "{" + usercode + "}";
			result += "namespace " + Namespace + "{" + classcode + "}";
			return result;
		}

		#endregion Compiling

		#region Serialization

		public override void ReadXml(XmlReader reader)
		{
			//After the options have been read, get all the assemblies and usings and put them in our lists, then remove the options from the Options object
			base.ReadXml(reader);
			IEnumerable<string> keys = Options.GetKeys();
			List<string> removeQueue = new List<string>();
			foreach (string key in keys)
			{
				if (key.StartsWith("Assembly_"))
				{
					Assemblies.Add(Assembly.Load(Options.GetOption(key, null)));
					removeQueue.Add(key);
					continue;
				}
				if (key.StartsWith("Using_"))
				{
					Usings.Add(Options.GetOption(key, null));
					removeQueue.Add(key);
					continue;
				}
			}
			foreach (string key in removeQueue)
			{
				Options.ClearOption(key);
			}
		}

		public override void WriteXml(XmlWriter writer)
		{
			//Add the lists of assemblies and usings to the options, for ease
			for (int i = 0; i < Assemblies.Count; i++)
			{
				Options.SetOption("Assembly_" + i.ToString(), Assemblies[i].FullName);
			}
			for (int i = 0; i < Usings.Count; i++)
			{
				Options.SetOption("Using_" + i.ToString(), Usings[i]);
			}
			base.WriteXml(writer);
		}

		#endregion Serialization
	}
}