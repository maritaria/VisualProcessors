using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VisualProcessors.Processing
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Pipeline p = new Pipeline();

			//*
			AddProcessor add1 = new AddProcessor("Add1");
			add1.GetInputChannel("B").IsConstant = true;
			add1.GetInputChannel("B").ConstantValue = 20;
			AddProcessor add2 = new AddProcessor("Add2");
			add2.GetInputChannel("B").IsConstant = true;
			add2.GetInputChannel("B").ConstantValue = 40;
			add1.GetOutputChannel("Output").Link(add2.GetInputChannel("A"));
			p.AddProcessor(add1);
			p.AddProcessor(add2);

			string path = Directory.GetCurrentDirectory() + "/output.xml";
			FileStream file = new FileStream(path, FileMode.Create);

			p.SaveToFile(file);
			file.Position = 0;

			var dp = Pipeline.LoadFromFile(file);
			Console.WriteLine("\nBuild\n");
			dp.Start();
			Thread.Sleep(100);

			for (int i = 0; i < 5; i++)
			{
				Console.WriteLine("Writing i=" + i);
				dp.GetByName("Add1").GetInputChannel("A").WriteValue(i);
				Console.WriteLine("Done writing i=" + i);
			}

			Console.ReadKey(true);
			dp.Stop();
		}
	}
}