using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using VisualProcessors.Controls;

namespace VisualProcessors.Processing
{
	[ProcessorAttribute("Bram Kamies", "Generates a pulse on a fixed interval", "", "Output",
		HideInputTab = true)]
	public class PulseProcessor : Processor
	{
		#region Properties

		public int Frequency
		{
			get
			{
				return int.Parse(Options.GetOption("Frequency"));
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", "Cannot be lower or equal to zero");
				}
				Options.SetOption("Frequency", value.ToString());
			}
		}
		public int Value
		{
			get
			{
				return int.Parse(Options.GetOption("Value"));
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", "Cannot be lower or equal to zero");
				}
				Options.SetOption("Value", value.ToString());
			}
		}

		#endregion Properties

		#region Constructor

		public PulseProcessor()
			: base()
		{
			Frequency = 1;
			Value = 0;
		}

		public PulseProcessor(Pipeline p, string name)
			: base(p, name)
		{
			AddOutputChannel("Output");
			Frequency = 1;
			Value = 0;
		}

		#endregion Constructor

		#region Methods

		public override void GetUserInterface(Panel panel)
		{
			NumericInputPanel freqInput = new NumericInputPanel();
			freqInput.Dock = DockStyle.Top;
			freqInput.InputTitle = "Frequency (Hz):";
			freqInput.InputMinimum = 1;
			freqInput.InputMaximum = 100000;
			freqInput.InputCompleted += freqInput_InputCompleted;
			freqInput.InputIncrement = 1;

			NumericInputPanel valueInput = new NumericInputPanel();
			valueInput.Dock = DockStyle.Top;
			valueInput.InputTitle = "Output value:";
			valueInput.InputMinimum = -10;
			valueInput.InputMaximum = 10;
			valueInput.InputCompleted += valueInput_InputCompleted;
			valueInput.InputIncrement = 1;

			panel.Controls.Add(valueInput);
			panel.Controls.Add(freqInput);
			base.GetUserInterface(panel);
		}

		void valueInput_InputCompleted(object sender, EventArgs e)
		{
			Value = (int)(sender as NumericInputPanel).InputValue;
		}

		void freqInput_InputCompleted(object sender, EventArgs e)
		{
			Frequency = (int)(sender as NumericInputPanel).InputValue;
		}

		protected override void WorkerMethod()
		{
			while(true)
			{
				GetOutputChannel("Output").WriteValue(Value);
				Thread.Sleep(1 / Frequency);
			}
		}

		#endregion Methods

		#region Event Handlers

		#endregion Event Handlers
	}
}