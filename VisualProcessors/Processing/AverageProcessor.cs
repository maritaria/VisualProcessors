using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using VisualProcessors.Controls;

namespace VisualProcessors.Processing
{
	[ProcessorMeta(Author = "Bram Kamies", Description = "Calculates the average of a signal over time")]
	public class AverageProcessor : Processor
	{
		private List<double> m_History = new List<double>();

		public AverageProcessor()
		{
		}

		public AverageProcessor(string name)
			: base(name)
		{
			AddInputChannel("Input", false);
			AddInputChannel("HistorySize", false);
			var hsize = GetInputChannel("HistorySize");
			hsize.IsConstant = true;
			hsize.ConstantValue = 1;
			AddOutputChannel("Output");
		}

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
	}
}