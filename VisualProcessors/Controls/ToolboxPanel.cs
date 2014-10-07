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
		public void SetPipelineForm(PipelineForm pipeline)
		{
			ClearButtonList();
			if (PipelineForm!=null)
			{
				PipelineForm.PipelineChanged -= PipelineForm_PipelineChanged;
			}
			m_PipelineForm = pipeline;
			if (PipelineForm != null)
			{
				PipelineForm.PipelineChanged += PipelineForm_PipelineChanged;
				PipelineForm_PipelineChanged(null, PipelineForm.CurrentPipeline);
			}
		}
		
		public void PopulateButtonList()
		{
			ClearButtonList();
			if (PipelineForm.CurrentPipeline != null)
			{
				foreach (Assembly asm in PipelineForm.CurrentPipeline.Assemblies)
				{
					AddAssembly(asm);
				}
			}
		}
		public void ClearButtonList()
		{

			foreach (Button b in ButtonPanel.Controls)
			{
				b.Dispose();
			}
			ButtonPanel.Controls.Clear();
		}

		private void AddAssembly(Assembly assembly)
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
			Processor p = (Processor)Activator.CreateInstance(t, t.Name + counter);

			ProcessorForm pf = PipelineForm.AddProcessor(p);
			Point pfcenter = pf.GetCenter();
			Point offset = new Point(pfcenter.X - pf.Location.X, pfcenter.Y - pf.Location.Y);
			pf.Location = new Point(pos.X - offset.X, pos.Y - offset.Y);
		}

		#endregion Methods

		#region Event Handlers

		void PipelineForm_PipelineChanged(Pipeline oldPipeline, Pipeline newPipeline)
		{
			if (oldPipeline != null)
			{
				oldPipeline.AssemblyAdded -= PipelineAssemblyAdded;
			}
			if (newPipeline != null)
			{
				newPipeline.AssemblyAdded += PipelineAssemblyAdded;
			}
			PopulateButtonList();
		}

		private void PipelineAssemblyAdded(Pipeline p, Assembly asm)
		{
			AddAssembly(asm);
		}
		#endregion
	}
}