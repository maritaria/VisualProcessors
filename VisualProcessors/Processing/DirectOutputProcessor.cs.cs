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
	[ProcessorMeta("Bram Kamies", "Displays a single number on a label", "Input", "",
		OutputTabMode = ProcessorTabMode.Hidden,
		CustomTabTitle = "Data")]
	public class DirectOutputProcessor : Processor
	{
		#region Properties

		private List<Label> guis = new List<Label>();

		#endregion Properties

		#region Constructor

		public DirectOutputProcessor()
		{
		}

		public DirectOutputProcessor(Pipeline pipeline, string name)
			: base(pipeline, name)
		{
			AddInputChannel("Input", false);
		}

		#endregion Constructor

		#region Methods

		public override void GetUserInterface(Panel panel)
		{
			Label l = new Label();
			l.Text = "No input available yet";
			l.Dock = DockStyle.Top;
			guis.Add(l);
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

		#endregion Methods
	}
}