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
	[ProcessorAttribute("Bram Kamies", "Calculates the delta of its input value, and the delta of the sample interval", "Input", "ValueDelta")]
	public class DeltaProcessor : Processor
	{
		#region Properties

		private double m_LastValue = 0;
		private bool m_FirstMoment = true;
		private DateTime m_LastMoment = DateTime.Now;

		#endregion

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

		#endregion

		#region Methods

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

		protected override void Prepare()
		{
			base.Prepare();
			m_FirstMoment = true;
		}
		public override void Start()
		{
			base.Start();
			m_LastMoment = DateTime.Now;
		}

		#endregion
	}
}