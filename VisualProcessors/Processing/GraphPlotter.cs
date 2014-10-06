using System;
using System.Collections.Generic;
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
	public class GraphPlotter : Processor
	{
		#region Properties

		private RollingPointPairList m_PointListBlue;
		private RollingPointPairList m_PointListGreen;
		private RollingPointPairList m_PointListRed;
		private DateTime m_StartTimestamp;

		public override bool AllowOptionalChannels
		{
			get
			{
				return true;
			}
		}

		#endregion Properties

		#region Constructor

		public GraphPlotter()
		{
		}

		public GraphPlotter(string name)
			: base(name)
		{
			AddInputChannel("Red", true);
			AddInputChannel("Blue", true);
			AddInputChannel("Green", true);
		}

		#endregion Constructor

		#region Methods

		public override void Start()
		{
			m_StartTimestamp = DateTime.Now;
			base.Start();
		}

		protected override void Process()
		{
			ReadAndWrite(GetInputChannel("Red"), m_PointListRed);
			ReadAndWrite(GetInputChannel("Blue"), m_PointListBlue);
			ReadAndWrite(GetInputChannel("Green"), m_PointListGreen);
			Thread.Sleep(100);
		}

		private void ReadAndWrite(InputChannel source, RollingPointPairList list)
		{
			if (source.HasData())
			{
				list.Add(DateTime.Now.Subtract(m_StartTimestamp).TotalSeconds, source.GetValue());
			}
		}

		#endregion Methods
	}
}