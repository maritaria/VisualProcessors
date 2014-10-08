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
	[ProcessorAttribute("Bram Kamies", "Displays a single number on a label", "Input", "",
		HideOutputTab = true)]
	public class DirectOutputProcessor : Processor
	{
		private List<Label> guis = new List<Label>();

		public DirectOutputProcessor()
		{
		}

		public DirectOutputProcessor(Pipeline pipeline, string name)
			: base(pipeline, name)
		{
			AddInputChannel("Input", false);
		}

		public override void GetUserInterface(Panel panel)
		{
			Label l = new Label();
			guis.Add(l);
			l.Text = "No input available yet";
			l.Dock = DockStyle.Top;
			panel.Controls.Add(l);
			base.GetUserInterface(panel);
		}

		protected override void Process()
		{
			double value = GetInputChannel("Input").GetValue();
			foreach (Label l in guis)
			{
				MethodInvoker action = delegate { l.Text = value.ToString(); };
				l.BeginInvoke(action);
			}
		}
	}
}