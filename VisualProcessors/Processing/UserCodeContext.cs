using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VisualProcessors.Controls;
using VisualProcessors.Processing;

namespace VisualProcessors.Processing
{
	public class UserCodeContext
	{
		public List<Assembly> Assemblies = new List<Assembly>();
		public string Classname = "UserCode";
		public List<CompilerError> CompileErrors = new List<CompilerError>();
		public List<CompilerError> CompileWarnings = new List<CompilerError>();
		public string Namespace = "VisualProcessors.UserCode";
		public Action<CodeProcessor> PrepareFunction;
		public Action<CodeProcessor> ProcessFunction;
		public string UserCode = "";
		public List<string> Usings = new List<string>();

		public UserCodeContext()
		{
			Usings.Add("System");
			Usings.Add("System.Collections.Generic");
			Usings.Add("System.Xml.Serialization");
			Usings.Add("VisualProcessors.Processing");
			Assemblies.Add(System.Reflection.Assembly.GetAssembly(typeof(Console)));
			Assemblies.Add(System.Reflection.Assembly.GetAssembly(typeof(Processor)));
			Assemblies.Add(System.Reflection.Assembly.GetAssembly(typeof(System.Xml.Serialization.IXmlSerializable)));
		}

		public bool Compile()
		{
			//Initialize compiler and clear error/warning lists
			CompileErrors.Clear();
			CompileWarnings.Clear();
			CSharpCodeProvider provider = new CSharpCodeProvider();
			CompilerParameters parameters = new CompilerParameters();
			foreach (Assembly asm in Assemblies)
			{
				parameters.ReferencedAssemblies.Add(asm.Location);
			}
			parameters.GenerateInMemory = true;
			parameters.GenerateExecutable = false;

			//Compile
			CompilerResults results = provider.CompileAssemblyFromSource(parameters, GenerateCode(UserCode));

			//Report errors and warnings
			if (results.Errors.HasErrors)
			{
				foreach (CompilerError error in results.Errors)
				{
					if (error.IsWarning)
					{
						CompileWarnings.Add(error);
					}
					else
					{
						CompileErrors.Add(error);
					}
				}
				return false;
			}
			foreach (CompilerError error in results.Errors)
			{
				if (error.IsWarning)
				{
					CompileErrors.Add(error);
				}
			}

			//Bind function
			Assembly assembly = results.CompiledAssembly;
			Type program = assembly.GetType(Namespace + "." + Classname);
			MethodInfo process = program.GetMethod("Process");
			if (process == null)
			{
				CompileErrors.Add(new CompilerError("userinput", 1, 1, "0", "No function 'Process' is defined"));
				return false;
			}
			else
			{
				ProcessFunction = (Action<CodeProcessor>)Delegate.CreateDelegate(typeof(Action<CodeProcessor>), process);
			}
			MethodInfo prepare = program.GetMethod("Prepare");
			if (prepare == null)
			{
				CompileWarnings.Add(new CompilerError("userinput", 1, 1, "0", "No function 'Prepare' is defined"));
			}
			else
			{
				PrepareFunction = (Action<CodeProcessor>)Delegate.CreateDelegate(typeof(Action<CodeProcessor>), prepare);
			}
			return true;
		}

		public string GenerateCode(string usercode)
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
	}
}