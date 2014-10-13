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
				return (string)ChannelComboBox.SelectedItem;
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
			ChannelComboBox.Items.Clear();
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
			int index = 0;
			foreach (string cname in col)
			{
				int i = ChannelComboBox.Items.Add(cname);
				if (cname == def)
				{
					index = i;
				}
			}
			ChannelComboBox.SelectedIndex = index;
		}

		#endregion Methods

		private void ChannelSelectionBox_Load(object sender, EventArgs e)
		{
			UpdateChannelList();
		}
	}
}