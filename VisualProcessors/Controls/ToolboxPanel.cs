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

		private PipelineForm m_PipelineForm;
		private List<Type> m_Types = new List<Type>();

		public PipelineForm PipelineForm
		{
			get
			{
				return m_PipelineForm;
			}
			set
			{
				SetPipelineForm(value);
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
			ClearButtonList();
		}

		#endregion Constructor

		#region Methods

		public void ClearButtonList()
		{
			foreach (Button b in ButtonPanel.Controls)
			{
				b.Dispose();
			}
			ButtonPanel.Controls.Clear();
		}

		public void PopulateButtonList()
		{
			ClearButtonList();
			if (PipelineForm == null || PipelineForm.CurrentPipeline == null)
			{
				return;
			}
			foreach (Assembly asm in Program.ProcessorAssemblies)
			{
				AddAssembly(asm);
			}
		}

		public void SetPipelineForm(PipelineForm pipeline)
		{
			ClearButtonList();
			if (PipelineForm != null)
			{
				PipelineForm.PipelineChanged -= PipelineForm_PipelineChanged;
			}
			m_PipelineForm = pipeline;
			if (PipelineForm != null)
			{
				PipelineForm.PipelineChanged += PipelineForm_PipelineChanged;
				PipelineForm_PipelineChanged(null, PipelineForm.CurrentPipeline);
			}
			PopulateButtonList();
		}

		public void SpawnProcessor(Type t)
		{
			if (PipelineForm == null)
			{
				return;
			}
			Point center = PipelineForm.MdiClient.Bounds.GetCenter();
			SpawnProcessor(t, center);
		}

		public void SpawnProcessor(Type t, Point pos)
		{
			if (PipelineForm == null)
			{
				return;
			}
			int counter = 1;
			string basename = t.Name;
			while (true)
			{
				if (PipelineForm.GetProcessorForm(basename + counter) == null)
					break;
				counter++;
			}
			Processor p = (Processor)Activator.CreateInstance(t, PipelineForm.CurrentPipeline, t.Name + counter);

			ProcessorForm pf = PipelineForm.AddProcessor(p, false);
			Point pfcenter = pf.GetCenter();
			Point offset = new Point(pfcenter.X - pf.Location.X, pfcenter.Y - pf.Location.Y);
			pf.Location = new Point(pos.X - offset.X, pos.Y - offset.Y);
		}

		private void AddAssembly(Assembly assembly)
		{
			Type[] processorTypes = assembly.GetTypes();
			foreach (Type processorType in processorTypes.Reverse())
			{
				if (processorType.IsSubclassOf(typeof(Processor)))
				{
					ProcessorMeta attr = (ProcessorMeta)Attribute.GetCustomAttribute(processorType, typeof(ProcessorMeta));
					if (attr != null && !attr.AllowUserSpawn)
					{
						//If the attribute is not null, and the AllowUserSpawn flag is false, then dont create a button for it
						continue;
					}
					m_Types.Add(processorType);
					Button b = new Button();
					b.Dock = DockStyle.Top;
					b.Text = processorType.Name;
					b.TextAlign = ContentAlignment.MiddleLeft;
					if (attr != null)
					{
						DescriptionTooltip.SetToolTip(b, attr.Description);
					}
					b.Click += delegate(object _sender, EventArgs _e)
					{
						SpawnProcessor(processorType);
					};
					ButtonPanel.Controls.Add(b);
				}
			}
		}

		#endregion Methods

		#region Event Handlers

		private void PipelineForm_PipelineChanged(Pipeline oldPipeline, Pipeline newPipeline)
		{
			PopulateButtonList();
		}

		#endregion Event Handlers
	}
}