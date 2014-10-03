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
using VisualProcessors.Forms;
using VisualProcessors.Processing;

namespace VisualProcessors.Controls
{
	public partial class ToolboxPanel : UserControl
	{
		#region Properties

		private PipelineForm m_Pipeline;

		public PipelineForm Pipeline
		{
			get
			{
				return m_Pipeline;
			}
			set
			{
				m_Pipeline = value;
				UpdateTypeList();
			}
		}

		#endregion Properties

		#region Constructor

		public ToolboxPanel()
		{
			InitializeComponent();
		}

		#endregion Constructor

		#region Methods

		public void AddAssembly(Assembly assembly)
		{
			Type[] processorTypes = assembly.GetTypes();
			foreach (Type processorType in processorTypes.Reverse())
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
					ButtonPanel.Controls.Add(b);
				}
			}
		}

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

		private void UpdateTypeList()
		{
			ButtonPanel.Controls.Clear();
			if (m_Pipeline == null)
			{
				return;
			}
			Assembly processorAssembly = Assembly.GetAssembly(typeof(Processor));
			AddAssembly(processorAssembly);
		}

		#endregion Methods
	}
}