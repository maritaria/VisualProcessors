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
				Processor.OutputChannelAdded -= UpdateLinkData;
				Processor.OutputChannelRemoved -= UpdateLinkData;
			}
			m_Processor = p;
			if (Processor != null)
			{
				Processor.LinkAdded += UpdateLinkData;
				Processor.LinkRemoved += UpdateLinkData;
				Processor.OutputChannelAdded += UpdateLinkData;
				Processor.OutputChannelRemoved += UpdateLinkData;
			}
			UpdateLinkData(this, EventArgs.Empty);
		}

		private void UpdateLinkData(object sender, EventArgs e)
		{
			string selected = (string)OutputComboBox.SelectedItem;
			OutputComboBox.Items.Clear();
			OutputGridView.Rows.Clear();
			if (Processor != null)
			{
				int index = 0;
				foreach (string name in Processor.GetOutputChannelNames())
				{
					int i = OutputComboBox.Items.Add(name);
					if (name == Processor.Meta.DefaultOutput)
					{
						index = i;
					}
					OutputChannel channel = Processor.GetOutputChannel(name);
					foreach (InputChannel target in channel.Targets)
					{
						OutputGridView.Rows.Add(channel.Name, target.Owner.Name, target.Name);
					}
				}
				if (OutputComboBox.Items.Count != 0)
				{
					if (selected == null)
					{
						OutputComboBox.SelectedIndex = index;
					}
					else
					{
						OutputComboBox.SelectedItem = selected;
					}
				}
			}
		}

		#endregion Methods

		#region Event Handlers

		private void LinkOutputButton_Click(object sender, EventArgs e)
		{
			if (Processor.GetOutputChannelNames().Length > 0)
			{
				Pipeline.StartLinkMode(Pipeline.GetProcessorForm(Processor.Name), (string)OutputComboBox.SelectedItem, LinkMode.OutputFirst);
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
				Pipeline.ShowProcessor(processorname);
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
				Pipeline.MdiClient.Invalidate();
			}
		}

		#endregion Event Handlers
	}
}