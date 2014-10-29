using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualProcessors.Forms;
using VisualProcessors.Processing;
namespace VisualProcessors.Controls
{
	public partial class PipelineErrorPanel : UserControl
	{
		#region Properties

		private PipelineForm m_Master;

		public PipelineForm Master
		{
			get
			{
				return m_Master;
			}
			set
			{
				if (m_Master != null)
				{
					m_Master.PipelineChanged -= MasterPipelineChanged;
				}
				m_Master = value;
				if (m_Master != null)
				{
					m_Master.PipelineChanged += MasterPipelineChanged;
				}
			}
		}

		#endregion Properties

		#region Constructor

		public PipelineErrorPanel()
		{
			InitializeComponent();
		}

		public PipelineErrorPanel(PipelineForm master) : this()
		{

		}

		#endregion Constructor

		#region Methods

		public void UpdateErrorList(bool wipe)
		{
			if (wipe)
			{
				ErrorGridView.Rows.Clear();
			}
			else
			{
				int i = 0;
				while(ErrorGridView.Rows.Count > i)
				{
					DataGridViewRow row = ErrorGridView.Rows[i];
					Func<bool> callback = row.Cells[5].Value as Func<bool>;
					if (callback == null || !callback())
					{
						ErrorGridView.Rows.RemoveAt(i);
						continue;
					}
					i++;
				}
			}
		}

		#endregion Methods

		#region Events

		#endregion Events

		#region Event Handlers

		private void MasterPipelineChanged(Pipeline arg1, Pipeline arg2)
		{
			if (arg1!=null)
			{
				arg1.Error -= arg2_Error;
			}
			if (arg2 != null)
			{
				arg2.Error += arg2_Error;
			}
			UpdateErrorList(true);
		}

		void arg2_Error(object sender, ProcessorErrorEventArgs e)
		{
			MethodInvoker action = delegate
			{
				ErrorGridView.Rows.Add(e.IsWarning ? SystemIcons.Warning : SystemIcons.Error, e.Source, e.Title, e.Message, e.HaltType, e.Callback);
			};
			this.BeginInvoke(action);
		}
		#endregion Event Handlers

		private void RefreshButton_Click(object sender, EventArgs e)
		{
			UpdateErrorList(false);
		}

		private void ErrorGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (ErrorGridView.Columns[e.ColumnIndex] == SourceColumn)
			{
				DataGridViewRow row = ErrorGridView.Rows[e.RowIndex];
				DataGridViewCellCollection cells = row.Cells;
				Processor p = (Processor)cells[SourceColumn.Name].Value;
				Master.ShowProcessor(p.Name);
				return;
			}
		}

		private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UpdateErrorList(false);
		}

		private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UpdateErrorList(true);
		}
	}
}