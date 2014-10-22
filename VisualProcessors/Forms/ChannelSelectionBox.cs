using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualProcessors.Processing;

namespace VisualProcessors.Forms
{
	public partial class ChannelSelectionBox : Form
	{
		#region Properties

		private Processor m_Processor;

		private ChannelType m_Type;

		public string Choice
		{
			get
			{
				if (ChannelListView.SelectedItems.Count == 1)
				{
					return ChannelListView.SelectedItems[0].Text;
				}
				else
				{
					return "_ERROR_NO_SELECTION_";
				}
			}
		}

		/// <summary>
		///  Gets or sets the processor of the selector
		/// </summary>
		public Processor Processor
		{
			get
			{
				return m_Processor;
			}
		}

		#endregion Properties

		#region Constructor

		public ChannelSelectionBox(ChannelType type, Processor p)
		{
			m_Type = type;
			m_Processor = p;
			InitializeComponent();
		}

		#endregion Constructor

		#region Methods

		private void UpdateChannelList()
		{
			ChannelListView.Items.Clear();
			string[] col;
			string def = "";
			if (m_Type == ChannelType.InputChannel)
			{
				col = Processor.GetInputChannelNames();
				def = Processor.Meta.DefaultInput;
			}
			else
			{
				col = Processor.GetOutputChannelNames();
				def = Processor.Meta.DefaultOutput;
			}
			foreach (string cname in col)
			{
				var item = ChannelListView.Items.Add(cname);
				if (cname == def)
				{
					item.Selected = true;
				}
			}
		}

		#endregion Methods

		private void ChannelSelectionBox_Load(object sender, EventArgs e)
		{
			UpdateChannelList();
		}

		private void listView1_DoubleClick(object sender, EventArgs e)
		{
			if (ChannelListView.SelectedItems.Count==1)
			{
				this.DialogResult = DialogResult.OK;
				Close();
			}
		}
	}
}