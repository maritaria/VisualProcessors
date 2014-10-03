using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualProcessors.Processing;

namespace VisualProcessors.Forms
{
	public partial class ToolboxForm : Form
	{
		#region Properties

		public PipelineForm Pipeline { get; private set; }

		#endregion Properties

		#region Constructor

		public ToolboxForm(PipelineForm pipeline)
		{
			Pipeline = pipeline;
			InitializeComponent();
		}

		#endregion Constructor

		#region Methods

		#endregion Methods

		#region Event Handlers

		private void SpawnProcessor(Type t)
		{
			int counter = 1;
			string basename = t.Name;
			while (true)
			{
				if (Pipeline.GetProcessorForm(basename + counter) == null)
					break;
				counter++;
			}
			Processor p = (Processor)Activator.CreateInstance(t, t.Name + counter);
			Pipeline.AddProcessor(p);
		}

		private void ToolboxForm_Load(object sender, EventArgs e)
		{
			Assembly processorAssembly = Assembly.GetAssembly(typeof(Processor));
			Type[] processorTypes = processorAssembly.GetTypes();
			foreach (Type processorType in processorTypes)
			{
				if (processorType.IsSubclassOf(typeof(Processor)))
				{
					Button b = new Button();
					b.Dock = DockStyle.Top;
					b.Text = processorType.Name;
					b.TextAlign = ContentAlignment.MiddleLeft;
					b.Click += delegate(object _sender, EventArgs _e)
					{
						SpawnProcessor(processorType);
					};
					Controls.Add(b);
				}
			}
		}

		#endregion Event Handlers
	}
}