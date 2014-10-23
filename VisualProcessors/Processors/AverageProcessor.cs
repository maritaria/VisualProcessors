using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using VisualProcessors.Controls;
using VisualProcessors.Processing;

namespace VisualProcessors.Processors
{
	[ProcessorMeta("Bram Kamies", "Calculates the average of a signal over time", "Input", "Output",
		CustomTabMode = ProcessorTabMode.Hide)]
	public class AverageProcessor : Processor
	{
		#region Properties

		private List<double> m_History = new List<double>();

		#endregion Properties

		#region Constructor

		public AverageProcessor()
		{
		}

		public AverageProcessor(Pipeline pipeline, string name)
			: base(pipeline, name)
		{
			AddInputChannel("Input", false);
			AddInputChannel("HistorySize", false);
			var hsize = GetInputChannel("HistorySize");
			hsize.IsConstant = true;
			hsize.ConstantValue = 1;
			AddOutputChannel("Output");
		}

		#endregion Constructor

		#region Methods

		protected override void Process()
		{
			double input = GetInputChannel("Input").GetValue();
			int historySize = (int)GetInputChannel("HistorySize").GetValue();
			m_History.Add(input);
			while (m_History.Count > historySize)
			{
				m_History.RemoveAt(0);
			}
			double b = 0;
			foreach (double val in m_History)
			{
				b += val;
			}
			GetOutputChannel("Output").WriteValue(b / historySize);
		}

		#endregion Methods
	}
}