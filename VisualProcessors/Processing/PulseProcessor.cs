using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualProcessors.Controls;

namespace VisualProcessors.Processing
{
	[ProcessorMeta("Bram Kamies", "Generates a pulse on a fixed interval", "", "Output",
		InputTabMode = ProcessorTabMode.Hidden)]
	public class PulseProcessor : Processor
	{
		#region Properties

		#endregion Properties

		#region Options

		[Browsable(true)]
		[ReadOnly(false)]
		[DisplayName("Frequency")]
		[Category("Settings")]
		[Description("The number of pulses per second to write to the OutputChannel")]
		[DefaultValue(1)]
		public int Frequency
		{
			get
			{
				return int.Parse(Options.GetOption("Frequency", "1"));
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

		[Browsable(true)]
		[ReadOnly(false)]
		[DisplayName("Value")]
		[Category("Settings")]
		[Description("The value of a pulse")]
		[DefaultValue(0)]
		public double Value
		{
			get
			{
				return double.Parse(Options.GetOption("Value", "0"));
			}
			set
			{
				Options.SetOption("Value", value.ToString());
			}
		}

		#endregion Options

		#region Constructor

		public PulseProcessor()
			: base()
		{
		}

		public PulseProcessor(Pipeline p, string name)
			: base(p, name)
		{
			AddOutputChannel("Output");
		}

		#endregion Constructor

		#region Methods

		protected override void WorkerMethod()
		{
			while (true)
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