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

		#region Events

		/// <summary>
		///  Invoked when the user clicks the LinkOutputButton and wants to link the output to
		///  another processors input
		/// </summary>
		public event EventHandler RequestLink;

		/// <summary>
		///  Invoked when the user clicks a hyperlink in the OutputGridView. The delegate has two
		///  parameters: a Processor and a string containing the name of the channel.
		/// </summary>
		public event Action<string, string> RequestShowChannel;

		/// <summary>
		///  Invoked just after the user has unlinked one or more links from the processor
		/// </summary>
		public event EventHandler RequestUnlink;

		private void OnRequestLink()
		{
			if (RequestLink != null)
			{
				RequestLink(this, EventArgs.Empty);
			}
		}

		private void OnRequestShowChannel(string processor, string channelname)
		{
			if (RequestShowChannel != null)
			{
				RequestShowChannel(processor, channelname);
			}
		}

		private void OnRequestUnlink()
		{
			if (RequestUnlink != null)
			{
				RequestUnlink(this, EventArgs.Empty);
			}
		}

		#endregion Events

		#region Event Handlers

		private void LinkOutputButton_Click(object sender, EventArgs e)
		{
			OnRequestLink();
		}

		private void OutputGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (OutputGridView.Columns[e.ColumnIndex] == InputColumn)
			{
				DataGridViewRow row = OutputGridView.Rows[e.RowIndex];
				DataGridViewCellCollection cells = row.Cells;
				string processorname = (string)cells[TargetColumn.Name].Value;
				string channelname = (string)cells[InputColumn.Name].Value;
				OnRequestShowChannel(processorname, channelname);
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
				OnRequestUnlink();
			}
		}

		#endregion Event Handlers
	}
}