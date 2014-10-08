using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using VisualProcessors;
using VisualProcessors.Controls;
using VisualProcessors.Forms;
using VisualProcessors.Processing;
using ZedGraph;

namespace VisualProcessors
{
	internal static class Program
	{
		public static List<Assembly> LoadedAssemblies = new List<Assembly>();
		public static List<Assembly> ProcessorAssemblies = new List<Assembly>();

		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		public static void Main()
		{
			string assemblyDirectory = "/Assemblies";
			string dir = Application.StartupPath + assemblyDirectory;
			if (!Directory.Exists(dir))
			{
				Console.WriteLine("Creating " + assemblyDirectory + " directory");
				Directory.CreateDirectory(dir);
			}

			Console.WriteLine("Loading assemblies from " + assemblyDirectory);
			foreach (string f in Directory.EnumerateFiles(dir))
			{
				try
				{
					Console.Write("\t" + Path.GetFileName(f) + ": ");
					Assembly asm = Assembly.LoadFrom(f);
					LoadedAssemblies.Add(asm);
					Console.WriteLine("Success");
				}
				catch
				{
					Console.WriteLine("Failed");
				}
			}

			Assembly processorAssembly = typeof(Processor).Assembly;
			AssemblyName processorName = processorAssembly.GetName();
			string name = processorName.Name;
			Console.WriteLine("Listing assemblies that reference " + name);
			foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
			{
				foreach (AssemblyName reference in asm.GetReferencedAssemblies())
				{
					if (reference.Name == name)
					{
						if (reference.Version != processorName.Version)
						{
							Console.WriteLine("\t" + asm.GetName().Name + " VERSION MISMATCH!!!");
						}
						else
						{
							Console.WriteLine("\t" + asm.GetName().Name);
						}
						ProcessorAssemblies.Add(asm);
						break;
					}
				}
			}
			if (!ProcessorAssemblies.Contains(processorAssembly))
			{
				Console.WriteLine("\t" + processorAssembly.GetName().Name);
				ProcessorAssemblies.Add(processorAssembly);
			}
			
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new PipelineForm());
		}
	}
}