using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using VisualProcessors.Processing;

namespace VisualProcessors.Processors
{
	[ProcessorMeta("Bram Kamies", "Calculates the delta of its input value, and the delta of the sample interval", "Input", "ValueDelta",
		CustomTabMode = ProcessorVisibility.Hide)]
	public class DeltaProcessor : Processor
	{
		#region Properties

		private bool m_FirstMoment = true;
		private DateTime m_LastMoment = DateTime.Now;
		private double m_LastValue = 0;

		#endregion Properties

		#region Constructor

		public DeltaProcessor()
		{
		}

		public DeltaProcessor(Pipeline pipeline, string name)
			: base(pipeline, name)
		{
			AddInputChannel("Input", false);
			AddOutputChannel("ValueDelta");
			AddOutputChannel("TimeDelta");
		}

		#endregion Constructor

		#region Methods

		public override void Start()
		{
			base.Start();
			m_LastMoment = DateTime.Now;
		}

		protected override void Prepare()
		{
			base.Prepare();
			m_FirstMoment = true;
		}

		protected override void Process()
		{
			double current = GetInputChannel("Input").GetValue();
			DateTime now = DateTime.Now;
			if (m_FirstMoment)
			{
				m_FirstMoment = false;
			}
			else
			{
				GetOutputChannel("ValueDelta").WriteValue(current - m_LastValue);
				GetOutputChannel("TimeDelta").WriteValue(now.Subtract(m_LastMoment).TotalSeconds);
			}
			m_LastValue = current;
			m_LastMoment = now;
		}

		#endregion Methods
	}
}