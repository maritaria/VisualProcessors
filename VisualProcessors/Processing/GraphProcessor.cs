using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using ZedGraph;
using System.ComponentModel;

namespace VisualProcessors.Processing
{
	[ProcessorMeta("Bram Kamies", "Plots its InputChannels onto a ZedGraph in realtime", "Red", "",
		AllowOptionalInputs = true,
		CustomTabTitle = "Graph",
		OutputTabMode = ProcessorTabMode.Hidden)]
	public class GraphProcessor : Processor
	{
		#region Properties

		private ZedGraphControl m_Graph;
		private RollingPointPairList m_PointListBlue;
		private RollingPointPairList m_PointListGreen;
		private RollingPointPairList m_PointListRed;
		private bool m_Redraw = false;
		private Thread m_RenderThread;
		private DateTime m_StartTimestamp;

		#endregion Properties

		#region Options



		[Browsable(true)]
		[ReadOnly(false)]
		[DisplayName("Buffer size")]
		[Category("Settings")]
		[Description("The amount of values to store per curve")]
		[DefaultValue(200)]
		public int BufferSize
		{
			get
			{
				return int.Parse(Options.GetOption("BufferSize", "200"));
			}
			set
			{
				Options.SetOption("BufferSize", value.ToString());
				OnModified(HaltTypes.ShouldHalt);
				IsPrepared = false;
			}
		}

		[Browsable(true)]
		[ReadOnly(false)]
		[DisplayName("ForceScroll")]
		[Category("Settings")]
		[Description("When true, the graph will scroll in real-time showing the last received samples. When false, the graph will autozoom to fit the curve entirely on the panel")]
		[DefaultValue(true)]
		public bool ForceScroll
		{
			get
			{
				return bool.Parse(Options.GetOption("ForceScroll", "True"));
			}
			set
			{
				Options.SetOption("ForceScroll", value.ToString());
				OnModified(HaltTypes.Continue);
			}
		}

		#endregion

		#region Constructor

		public GraphProcessor() : base()
		{
		}

		public GraphProcessor(Pipeline pipeline, string name)
			: base(pipeline, name)
		{
			AddInputChannel("Red", true);
			AddInputChannel("Blue", true);
			AddInputChannel("Green", true);
		}

		private void SetupLists()
		{
			m_Graph.GraphPane.CurveList.Clear();
			m_PointListRed = new RollingPointPairList(BufferSize);
			m_PointListBlue = new RollingPointPairList(BufferSize);
			m_PointListGreen = new RollingPointPairList(BufferSize);
			m_Graph.GraphPane.AddCurve("Red", m_PointListRed, Color.Red, SymbolType.None);
			m_Graph.GraphPane.AddCurve("Blue", m_PointListBlue, Color.Blue, SymbolType.None);
			m_Graph.GraphPane.AddCurve("Green", m_PointListGreen, Color.Green, SymbolType.None);
		}

		#endregion Constructor

		#region Methods

		public override void GetUserInterface(Panel panel)
		{
			m_Graph = new ZedGraphControl();
			m_Graph.Dock = DockStyle.Fill;

			SetupLists();

			panel.Controls.Add(m_Graph);
			base.GetUserInterface(panel);
		}

		public override void Start()
		{
			if (m_RenderThread != null)
			{
				Stop();
			}
			m_RenderThread = new Thread(new ThreadStart(RenderThreadMethod));
			m_RenderThread.IsBackground = true;
			m_RenderThread.Start();
			base.Start();
		}

		public override void Stop()
		{
			if (m_RenderThread != null)
			{
				if (m_RenderThread.IsAlive)
				{
					m_RenderThread.Abort();
				}
				while (m_RenderThread.IsAlive) ;
				m_RenderThread = null;
			}
			base.Stop();
		}

		protected override void Prepare()
		{
			m_StartTimestamp = DateTime.Now;
			SetupLists();
			base.Prepare();
		}

		protected override void Process()
		{
			lock (this)
			{
				ReadAndWrite(GetInputChannel("Red"), m_PointListRed);
				ReadAndWrite(GetInputChannel("Blue"), m_PointListBlue);
				ReadAndWrite(GetInputChannel("Green"), m_PointListGreen);
			}
			m_Redraw = true;
		}

		private void ReadAndWrite(InputChannel source, RollingPointPairList list)
		{
			double val = 0;
			while (source.HasValue())
			{
				val = source.GetValue();
				list.Add(DateTime.Now.Subtract(m_StartTimestamp).TotalSeconds, val);
			}
		}

		private void RenderThreadMethod()
		{
			while (true)
			{
				if (m_Redraw)
				{
					lock (this)
					{
						if (ForceScroll)
						{
							m_Graph.GraphPane.XAxis.Scale.Min = DateTime.Now.Subtract(m_StartTimestamp).TotalSeconds - 2;
							m_Graph.GraphPane.XAxis.Scale.Max = DateTime.Now.Subtract(m_StartTimestamp).TotalSeconds;
						}
						m_Graph.AxisChange();
						m_Graph.Invalidate();
						m_Redraw = false;
					}
				}
				Thread.Sleep(16);
			}
		}

		#endregion Methods
	}
}