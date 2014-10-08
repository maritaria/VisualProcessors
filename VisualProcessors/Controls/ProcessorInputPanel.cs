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
	public partial class ProcessorInputPanel : UserControl
	{
		#region Properties

		private bool m_Changing = false;
		private bool m_ConstantInputValid = true;
		private InputChannel m_InputChannel;
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

		public InputChannel SelectedInputChannel
		{
			get
			{
				return m_InputChannel;
			}
			set
			{
				SetInputChannel(value);
			}
		}

		#endregion Properties

		#region Constructor

		public ProcessorInputPanel()
		{
			InitializeComponent();
		}

		#endregion Constructor

		#region Methods

		/// <summary>
		///  Sets the InputChannel that the user wants to edit
		/// </summary>
		/// <param name="channel"></param>
		public void SetInputChannel(InputChannel channel)
		{
			m_Changing = true;

			if (m_InputChannel != null)
			{
				//Unsubscribe events
				m_InputChannel.SourceChanged -= SelectedInputChannelSourceChanged;
			}
			m_InputChannel = channel;
			if (m_InputChannel != null)
			{
				//Subscribe events
				m_InputChannel.SourceChanged += SelectedInputChannelSourceChanged;
			}
			UpdateChannel();
			m_Changing = false;
		}

		public void SetProcessor(Processor p)
		{
			if (m_Processor != null)
			{
				m_Processor.LinkAdded -= UpdateLinkData;
				m_Processor.LinkRemoved -= UpdateLinkData;
				m_Processor.InputChannelAdded -= ProcessorChannelsChanged;
				m_Processor.InputChannelRemoved -= ProcessorChannelsChanged;
			}
			m_Processor = p;
			if (m_Processor != null)
			{
				m_Processor.LinkAdded += UpdateLinkData;
				m_Processor.LinkRemoved += UpdateLinkData;
				m_Processor.InputChannelAdded += ProcessorChannelsChanged;
				m_Processor.InputChannelRemoved += ProcessorChannelsChanged;
				InputChannelList.Items.Clear();
				int index = -1;
				foreach (string channelname in m_Processor.GetInputChannelNames())
				{
					InputChannel channel = m_Processor.GetInputChannel(channelname);
					int i = InputChannelList.Items.Add(channel.Name);
					if (channelname == m_Processor.DefaultInput)
					{
						index = i;
					}
				}
				InputChannelList.SelectedIndex = index;
				OptionalCheckBox.Enabled = m_Processor.AllowOptionalInputs;
			}
			UpdateLinkData(this, EventArgs.Empty);
		}

		public bool ShowInputChannel(string name)
		{
			InputChannel channel = Processor.GetInputChannel(name);
			if (channel != null)
			{
				InputChannelList.SelectedItem = channel.Name;
				SetInputChannel(channel);
				return true;
			}
			return false;
		}

		private void UpdateChannel()
		{
			if (m_InputChannel == null)
			{
				LinkRadioButton.Enabled = false;
				ConstantRadioButton.Enabled = false;
				ConstantInput.Text = "";
				LinkButton.Enabled = false;
				ConstantInput.Enabled = false;
				UnlinkButton.Enabled = false;
				ConfigurationLabel.Text = "No channel selected";
				ConstantInput.BackColor = Color.White;
				OptionalCheckBox.Enabled = false;
				OptionalCheckBox.Checked = false;
			}
			else
			{
				if (!m_Changing)
				{
					m_InputChannel.IsConstant = ConstantRadioButton.Checked;
				}
				else
				{
					m_ConstantInputValid = true;
				}
				LinkRadioButton.Enabled = true;
				ConstantRadioButton.Enabled = true;
				LinkRadioButton.Checked = !m_InputChannel.IsConstant;
				ConstantRadioButton.Checked = m_InputChannel.IsConstant;
				LinkButton.Enabled = LinkRadioButton.Checked;
				ConstantInput.Enabled = ConstantRadioButton.Checked;
				UnlinkButton.Enabled = m_InputChannel.Source != null;
				ConstantInput.Text = m_InputChannel.ConstantValue.ToString();
				ConstantInput.BackColor = (m_ConstantInputValid) ? Color.White : Color.Red;
				OptionalCheckBox.Checked = OptionalCheckBox.Enabled && m_InputChannel.IsOptional;
				if (m_InputChannel.IsConstant)
				{
					ConfigurationLabel.Text = "Constant Value: " + m_InputChannel.ConstantValue;
				}
				else
				{
					if (m_InputChannel.Source != null)
					{
						ConfigurationLabel.Text = "Source: " + m_InputChannel.Source.Owner.Name + " (" + m_InputChannel.Source.Name + ")";
					}
					else
					{
						ConfigurationLabel.Text = "Unlinked";
					}
				}
			}
		}

		private void UpdateLinkData(object sender, EventArgs somethig)
		{
			UpdateChannel();
		}

		#endregion Methods

		#region EventHandlers

		private void ConstantInput_TextChanged(object sender, EventArgs e)
		{
			if (m_Changing)
			{
				return;
			}
			double var = 0;
			if (double.TryParse(ConstantInput.Text.Replace(".", ","), out var) && !double.IsNaN(var))
			{
				m_ConstantInputValid = true;
				m_InputChannel.ConstantValue = var;
			}
			else
			{
				m_ConstantInputValid = false;
			}
			ConstantInput.BackColor = (m_ConstantInputValid) ? Color.White : Color.Red;
		}

		private void InputChannelList_SelectedIndexChanged(object sender, EventArgs e)
		{
			string channelname = (string)InputChannelList.SelectedItem;
			SelectedInputChannel = Processor.GetInputChannel(channelname);
		}

		private void LinkButton_Click(object sender, EventArgs e)
		{
			Pipeline.StartLinkMode(Pipeline.GetProcessorForm(Processor.Name), SelectedInputChannel.Name, LinkMode.InputFirst);
			UpdateChannel();
		}

		private void OptionalCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (m_Changing)
			{
				return;
			}
			if (m_InputChannel != null)
			{
				m_InputChannel.IsOptional = OptionalCheckBox.Checked;
			}
			UpdateChannel();
		}

		private void ProcessorChannelsChanged(object sender, EventArgs e)
		{
			SetProcessor(Processor);
		}

		private void RadioButtonCheckedChanged(object sender, EventArgs e)
		{
			if (m_Changing)
			{
				return;
			}
			if (m_InputChannel != null)
			{
				m_InputChannel.IsConstant = ConstantRadioButton.Checked;
			}
			Pipeline.MdiClient.Invalidate();
			UpdateChannel();
		}

		private void SelectedInputChannelSourceChanged(object sender, EventArgs e)
		{
			Pipeline.MdiClient.Invalidate();
			UpdateChannel();
		}

		private void UnlinkButton_Click(object sender, EventArgs e)
		{
			if (m_InputChannel != null)
			{
				m_InputChannel.Unlink();
			}
			Pipeline.MdiClient.Invalidate();
			UpdateChannel();
		}

		#endregion EventHandlers
	}
}