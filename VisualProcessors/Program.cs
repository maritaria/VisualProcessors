using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using VisualProcessors;
using VisualProcessors.Controls;
using VisualProcessors.Forms;
using VisualProcessors.Processing;

namespace VisualProcessors
{
	internal static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			Pipeline p = new Pipeline();

			DirectInputProcessor in1 = new DirectInputProcessor("DirectInputProcessor1");
			AddProcessor add1 = new AddProcessor("Add1");
			add1.GetInputChannel("B").IsConstant = true;
			add1.GetInputChannel("B").ConstantValue = 20;
			AddProcessor add2 = new AddProcessor("Add2");
			add2.GetInputChannel("B").IsConstant = true;
			add2.GetInputChannel("B").ConstantValue = 40;
			DirectOutputProcessor out1 = new DirectOutputProcessor("DirectOutputProcessor1");

			in1.GetOutputChannel("Output").Link(add1.GetInputChannel("A"));
			add1.GetOutputChannel("Output").Link(add2.GetInputChannel("A"));
			add2.GetOutputChannel("Output").Link(out1.GetInputChannel("Input"));

			p.AddProcessor(new CodeProcessor("Test"));
			p.AddProcessor(in1);
			p.AddProcessor(add1);
			p.AddProcessor(add2);
			p.AddProcessor(out1);

			string path = Application.StartupPath + "/output.xml";
			FileStream file = new FileStream(path, FileMode.Create);

			p.SaveToFile(file);
			file.Position = 0;

			var dp = Pipeline.LoadFromFile(file);

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new PipelineForm(p));
		}
	}
}