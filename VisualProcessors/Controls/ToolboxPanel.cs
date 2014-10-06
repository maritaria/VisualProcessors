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

		private List<Type> m_Types = new List<Type>();

		public PipelineForm Pipeline
		{
			get
			{
				return m_Pipeline;
			}
			set
			{
				m_Pipeline = value;
			}
		}

		public Type[] Types
		{
			get
			{
				return m_Types.ToArray();
			}
		}

		#endregion Properties

		#region Constructor

		public ToolboxPanel()
		{
			InitializeComponent();
			ButtonPanel.Controls.Clear();
			AddAssembly(Assembly.GetAssembly(typeof(Processor)));
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
					m_Types.Add(processorType);
					Button b = new Button();
					b.Dock = DockStyle.Top;
					b.Text = processorType.Name;
					b.TextAlign = ContentAlignment.MiddleLeft;
					foreach (Attribute attr in Attribute.GetCustomAttributes(processorType))
					{
						if (attr is ProcessorMeta)
						{
							ProcessorMeta pm = attr as ProcessorMeta;
							DescriptionTooltip.SetToolTip(b, pm.Description);
							break;
						}
					}
					b.Click += delegate(object _sender, EventArgs _e)
					{
						SpawnProcessor(processorType);
					};
					ButtonPanel.Controls.Add(b);
				}
			}
		}

		public void SpawnProcessor(Type t)
		{
			if (Pipeline == null)
			{
				return;
			}
			Point center = Pipeline.MdiClient.Bounds.GetCenter();
			SpawnProcessor(t, center);
		}

		public void SpawnProcessor(Type t, Point pos)
		{
			if (Pipeline == null)
			{
				return;
			}
			int counter = 1;
			string basename = t.Name;
			while (true)
			{
				if (Pipeline.GetProcessorForm(basename + counter) == null)
					break;
				counter++;
			}
			Processor p = (Processor)Activator.CreateInstance(t, t.Name + counter);

			ProcessorForm pf = Pipeline.AddProcessor(p);
			Point pfcenter = pf.GetCenter();
			Point offset = new Point(pfcenter.X - pf.Location.X, pfcenter.Y - pf.Location.Y);
			pf.Location = new Point(pos.X - offset.X, pos.Y - offset.Y);
		}

		#endregion Methods
	}
}