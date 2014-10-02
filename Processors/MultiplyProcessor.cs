﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace VisualProcessors.Processing
{
	public class MultiplyProcessor : Processor
	{
		public MultiplyProcessor()
		{
			AddInputChannel("A");
			AddInputChannel("B");
		}

		public MultiplyProcessor(string name)
			: base(name)
		{
			AddInputChannel("A");
			AddInputChannel("B");
		}

		protected override void Process()
		{
			double a = GetInputChannel("A").GetValue();
			double b = GetInputChannel("B").GetValue();
			GetOutputChannel("Output").WriteValue(a * b);
		}
	}
}