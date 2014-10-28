using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using VisualProcessors.Forms;
using VisualProcessors.Processing;

namespace VisualProcessors
{
	internal static class Program
	{
		public static ApplicationSettings Config = new ApplicationSettings();
		public static List<Assembly> LoadedAssemblies = new List<Assembly>();
		public static List<Assembly> ProcessorAssemblies = new List<Assembly>();

		public static void LoadConfig(string path)
		{
			Console.WriteLine("Loading config from: " + path);
			FileStream fs = null;
			try
			{
				fs = new FileStream(path, FileMode.Open);
				XmlSerializer s1 = new XmlSerializer(typeof(ApplicationSettings));
				Config = (ApplicationSettings)s1.Deserialize(fs);
			}
			catch (Exception e)
			{
				while (e != null)
				{
					Console.WriteLine("Failed to load config: " + e.Message);
					e = e.InnerException;
				}
				return;
			}
			finally
			{
				if (fs != null)
				{
					fs.Dispose();
				}
			}
		}

		public static void SaveConfig(string path)
		{
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.IndentChars = "\t";
			settings.Indent = true;

			FileStream fs = null;
			try
			{
				fs = new FileStream(Application.StartupPath + "/application.config.xml", FileMode.Create);
				XmlWriter writer = XmlWriter.Create(fs, settings);
				XmlSerializer s1 = new XmlSerializer(typeof(ApplicationSettings));
				s1.Serialize(writer, Config);
			}
			catch (Exception e)
			{
				while (e != null)
				{
					Console.WriteLine("Failed to safe config: " + e.Message);
					e = e.InnerException;
				}
				return;
			}
			finally
			{
				fs.Dispose();
			}
		}

		private static void LoadAssemblies(string path)
		{
			foreach (string f in Directory.EnumerateFiles(path))
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
				if (!asm.IsDynamic)
				{
					foreach (AssemblyName reference in asm.GetReferencedAssemblies())
					{
						if (reference.Name == name)
						{
							if (reference.Version != processorName.Version)
							{
								Console.WriteLine("\t" + asm.GetName().Name + " Version mismatch!");
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
			}
			if (!ProcessorAssemblies.Contains(processorAssembly))
			{
				Console.WriteLine("\t" + processorAssembly.GetName().Name);
				ProcessorAssemblies.Add(processorAssembly);
			}
		}

		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			LoadConfig(Application.StartupPath + "/application.config.xml");

			string assemblyDirectory = Config.AssemblySubDirectory;
			string dir = Application.StartupPath + "/" + assemblyDirectory;
			if (!Directory.Exists(dir))
			{
				Console.WriteLine("Creating " + assemblyDirectory + " directory");
				Directory.CreateDirectory(dir);
			}
			Console.WriteLine("Loading assemblies from " + assemblyDirectory);
			LoadAssemblies(dir);

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			PipelineForm mainform = new PipelineForm();
			Application.Run(mainform);
			SaveConfig(Application.StartupPath + "/application.config.xml");
		}
	}
}