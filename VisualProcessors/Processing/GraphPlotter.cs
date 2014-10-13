using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using ZedGraph;

namespace VisualProcessors.Processing
{
	[ProcessorMeta("Bram Kamies", "Plots its InputChannels onto a ZedGraph in realtime", "Red", "",
		AllowOptionalInputs = true,
		CustomTabTitle="Graph",
		OutputTabMode = ProcessorTabMode.Hidden)]
	public class GraphPlotter : Processor
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

		#region Constructor

		public GraphPlotter()
		{
			SetupLists();
		}

		public GraphPlotter(Pipeline pipeline, string name)
			: base(pipeline, name)
		{
			AddInputChannel("Red", true);
			AddInputChannel("Blue", true);
			AddInputChannel("Green", true);
			SetupLists();
		}

		private void SetupLists()
		{
			m_PointListRed = new RollingPointPairList(2000);
			m_PointListBlue = new RollingPointPairList(20);
			m_PointListGreen = new RollingPointPairList(20);
		}

		#endregion Constructor

		#region Methods

		public override void GetUserInterface(Panel panel)
		{
			m_Graph = new ZedGraphControl();
			m_Graph.Dock = DockStyle.Fill;

			m_Graph.GraphPane.AddCurve("Red", m_PointListRed, Color.Red, SymbolType.None);
			m_Graph.GraphPane.AddCurve("Blue", m_PointListBlue, Color.Blue, SymbolType.None);
			m_Graph.GraphPane.AddCurve("Green", m_PointListGreen, Color.Green, SymbolType.None);
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
			if (source.HasValue())
			{
				val = source.GetValue();
			}
			list.Add(DateTime.Now.Subtract(m_StartTimestamp).TotalSeconds, val);
		}

		private void RenderThreadMethod()
		{
			while (true)
			{
				if (m_Redraw)
				{
					lock (this)
					{
						m_Graph.GraphPane.XAxis.Scale.Min = DateTime.Now.Subtract(m_StartTimestamp).TotalSeconds - 2;
						m_Graph.GraphPane.XAxis.Scale.Max = DateTime.Now.Subtract(m_StartTimestamp).TotalSeconds;
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