using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualProcessors;
using VisualProcessors.Controls;
using VisualProcessors.Forms;
using VisualProcessors.Processing;

namespace VisualProcessors.Controls
{
	public partial class ProcessorOutputPanel : UserControl
	{
		#region Properties

		private Processor m_Processor;

		public PipelineForm Pipeline { get; set; }

		public Processor Processor
		{
			get
			{
				return m_Processor;
			}
			set
			{
				SetProcessor(value);
			}
		}

		#endregion Properties

		#region Constructor

		public ProcessorOutputPanel()
		{
			InitializeComponent();
		}

		#endregion Constructor

		#region Methods

		public void SetProcessor(Processor p)
		{
			if (Processor != null)
			{
				Processor.LinkAdded -= UpdateLinkData;
				Processor.LinkRemoved -= UpdateLinkData;
			}
			m_Processor = p;
			if (Processor != null)
			{
				Processor.LinkAdded += UpdateLinkData;
				Processor.LinkRemoved += UpdateLinkData;
			}
			UpdateLinkData(this, EventArgs.Empty);
		}

		private void UpdateLinkData(object sender, EventArgs e)
		{
			OutputGridView.Rows.Clear();
			if (Processor != null)
			{
				foreach (string name in Processor.GetOutputChannelNames())
				{
					OutputChannel channel = Processor.GetOutputChannel(name);
					foreach (InputChannel target in channel.Targets)
					{
						OutputGridView.Rows.Add(channel.Name, target.Owner.Name, target.Name);
					}
				}
			}
		}

		#endregion Methods

		#region Event Handlers

		private void LinkOutputButton_Click(object sender, EventArgs e)
		{
			if (Processor.HasOutputChannels)
			{
				ChannelSelectionBox csbox = new ChannelSelectionBox(ChannelType.OutputChannel, Processor);
				if (csbox.ShowDialog().HasFlag(DialogResult.OK))
				{
					Pipeline.StartLinkMode(Pipeline.GetProcessorForm(Processor.Name), csbox.Choice, LinkMode.OutputFirst);
				}
			}
		}

		private void OutputGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (OutputGridView.Columns[e.ColumnIndex] == InputColumn)
			{
				DataGridViewRow row = OutputGridView.Rows[e.RowIndex];
				DataGridViewCellCollection cells = row.Cells;
				string processorname = (string)cells[TargetColumn.Name].Value;
				string channelname = (string)cells[InputColumn.Name].Value;
				Pipeline.ShowInputChannel(processorname, channelname);
				return;
			}
			if (OutputGridView.Columns[e.ColumnIndex] == TargetColumn)
			{
				DataGridViewRow row = OutputGridView.Rows[e.RowIndex];
				DataGridViewCellCollection cells = row.Cells;
				string processorname = (string)cells[TargetColumn.Name].Value;
				Pipeline.ShowProcessor(processorname, true);
				return;
			}
		}

		private void UnlinkMenuItem_Click(object sender, EventArgs e)
		{
			List<int> rows = new List<int>();
			foreach (DataGridViewCell cell in OutputGridView.SelectedCells)
			{
				if (!rows.Contains(cell.RowIndex))
				{
					rows.Add(cell.RowIndex);
				}
			}
			foreach (int rownum in rows)
			{
				DataGridViewRow row = OutputGridView.Rows[rownum];
				string outputchannelname = (string)row.Cells[OutputColumn.Name].Value;
				string inputchannelname = (string)row.Cells[InputColumn.Name].Value;
				string processorname = (string)row.Cells[TargetColumn.Name].Value;
				OutputChannel outputchannel = Processor.GetOutputChannel(outputchannelname);
				outputchannel.Unlink(processorname, inputchannelname);
			}
			if (rows.Count > 0)
			{
				Pipeline.InvalidateMdi();
			}
		}

		#endregion Event Handlers
	}
}