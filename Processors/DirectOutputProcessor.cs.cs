using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace VisualProcessors.Processing
{
	public class DirectOutputProcessor : Processor
	{
		private List<Label> guis = new List<Label>();

		public DirectOutputProcessor()
		{
		}

		public DirectOutputProcessor(string name)
			: base(name)
		{
			AddInputChannel("Input");
		}

		public override Control GetUserInterface()
		{
			Label l = new Label();
			guis.Add(l);
			l.Text = "No input available yet";
			return l;
		}

		protected override void Process()
		{
			double value = GetInputChannel("Input").GetValue();
			foreach (Label l in guis)
			{
				MethodInvoker action = delegate { l.Text = value.ToString(); };
				l.BeginInvoke(action);
			}
			Console.WriteLine(Name + " Received: " + value);
		}
	}
}