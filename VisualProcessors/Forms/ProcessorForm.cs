using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualProcessors;
using VisualProcessors.Controls;
using VisualProcessors.Forms;
using VisualProcessors.Processing;

namespace VisualProcessors.Forms
{
	/// <summary>
	///  This class is used to visually represent a processor to the user, and can be used in
	///  PipelineForms as a design element
	/// </summary>
	public partial class ProcessorForm : Form
	{
		#region Properties

		private bool m_Closing = false;
		private LinkMode m_LinkMode = LinkMode.Disabled;
		private bool m_LocationChanging = false;
		private PipelineForm m_PipelineForm;
		private Processor m_Processor;
		private Type m_ProcessorType;
		private bool m_SizeChanging = false;

		/// <summary>
		///  Gets the PipelineForm the ProcessorForm belongs to.
		/// </summary>
		public PipelineForm PipelineForm
		{
			get { return m_PipelineForm; }
		}

		/// <summary>
		///  Gets the processor of the form
		/// </summary>
		public Processor Processor
		{
			get { return m_Processor; }
		}

		#endregion Properties

		#region Constructor

		/// <summary>
		///  Creates a new ProcessorForm to display a Processor instance on.
		/// </summary>
		/// <param name="pipelineform">The PipelineForm that owns the ProcessorForm</param>
		/// <param name="processor">   The Processor to display and configure with the form</param>
		/// <param name="useModel">    
		///  When true, the form is sized and positioned according to the processors Size and
		///  Location properties respectively. (Set to true if the processor was deserialized from
		///  XML)
		/// </param>
		public ProcessorForm(PipelineForm pipelineform, Processor processor, bool useModel)
		{
			InitializeComponent();

			//Initialize values
			m_PipelineForm = pipelineform;

			//Processor
			m_Processor = processor;
			m_ProcessorType = processor.GetType();
			if (!useModel)
			{
				Processor.Location = this.Location;
				processor.Size = this.Size;
			}
			Processor.NameChanged += ProcessorNameChanged;
			Processor.LocationChanged += ProcessorLocationChanged;
			Processor.SizeChanged += ProcessorSizeChanged;
			ProcessorNameChanged(Processor, Processor.Name, Processor.Name);
			if (useModel)
			{
				ProcessorLocationChanged(this, EventArgs.Empty);
				ProcessorSizeChanged(this, EventArgs.Empty);
			}

			//Input
			ProcessorInputView.Pipeline = PipelineForm;
			ProcessorInputView.Processor = Processor;
			InputTab.Text = Processor.Meta.InputTabTitle;
			if (Processor.Meta.InputTabMode == ProcessorTabMode.Hidden)
			{
				Tabs.TabPages.Remove(InputTab);
			}

			//Properties
			PropertiesTab.Text = Processor.Meta.PropertiesTabTitle;
			PropertyGrid.SelectedObject = Processor;
			if (Processor.Meta.PropertiesTabMode == ProcessorTabMode.Hidden)
			{
				Tabs.TabPages.Remove(PropertiesTab);
			}

			//Custom
			processor.GetUserInterface(CustomPanel);
			CustomTab.Text = Processor.Meta.CustomTabTitle;
			if (Processor.Meta.CustomTabMode == ProcessorTabMode.Hidden)
			{
				Tabs.TabPages.Remove(CustomTab);
			}

			//Output
			ProcessorOutputView.Pipeline = PipelineForm;
			ProcessorOutputView.Processor = Processor;
			OutputTab.Text = Processor.Meta.OutputTabTitle;
			if (Processor.Meta.OutputTabMode == ProcessorTabMode.Hidden)
			{
				Tabs.TabPages.Remove(OutputTab);
			}
		}

		#endregion Constructor

		#region Methods

		/// <summary>
		///  Closes the form without showing the confirmation MessageBox
		/// </summary>
		public void ForceClose()
		{
			m_Closing = true;
			Close();
		}

		/// <summary>
		///  Sets the linkmode of the form
		/// </summary>
		/// <param name="mode">  The current linkmode</param>
		/// <param name="origin">
		///  True if the current form is the form that started the linkmode session, false otherwise
		/// </param>
		public void SetLinkMode(LinkMode mode, bool origin)
		{
			m_LinkMode = mode;
			LinkButtons.Visible = (mode != LinkMode.Disabled);
			CancelLinkButton.Enabled = origin;
			switch (mode)
			{
				case LinkMode.InputFirst:
					ConfirmLinkButton.Text = "Link Output";
					ConfirmLinkButton.Enabled = (Processor.Meta.OutputTabMode != ProcessorTabMode.Hidden) && (Processor.OutputChannelCount > 0);
					break;

				case LinkMode.OutputFirst:
					ConfirmLinkButton.Text = "Link Input";
					ConfirmLinkButton.Enabled = (Processor.Meta.InputTabMode != ProcessorTabMode.Hidden) && (Processor.InputChannelCount > 0);
					break;
			}
		}

		/// <summary>
		///  Focusses the TabControl on the 'Input' tab, and selects the channel with the given name
		/// </summary>
		/// <param name="name">The name of the InputChannel to select</param>
		public void ShowInputChannel(string name)
		{
			if (ProcessorInputView.ShowInputChannel(name))
			{
				Tabs.SelectTab(InputTab);
			}
		}

		#endregion Methods

		#region Event Handlers

		private void LinkEndpointButton_Click(object sender, EventArgs e)
		{
			PipelineForm.CompleteLinkMode(this);
		}

		private void ProcessorForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (m_Closing)
			{
				return;
			}
			m_Closing = true;
			if (e.CloseReason == CloseReason.UserClosing)
			{
				var window = MessageBox.Show("Are you sure you want to delete the processor?", "Removing processor", MessageBoxButtons.YesNo);
				if (window == DialogResult.No)
				{
					e.Cancel = true;
					m_Closing = false;
				}
				else
				{
					PipelineForm.RemoveProcessor(this.Processor.Name);
				}
			}
		}

		private void ProcessorForm_LocationChanged(object sender, EventArgs e)
		{
			Processor.Location = this.Location;
		}

		private void ProcessorForm_SizeChanged(object sender, EventArgs e)
		{
			this.Processor.Size = this.Size;
		}

		private void ProcessorLocationChanged(object sender, EventArgs e)
		{
			if (m_LocationChanging || (Processor == null))
			{
				return;
			}
			m_LocationChanging = true;
			Location = Processor.Location;
			m_LocationChanging = false;
		}

		private void ProcessorNameChanged(Processor p, string oldname, string newname)
		{
			Text = Processor.GetType().Name + ": " + newname;
		}

		private void ProcessorSizeChanged(object sender, EventArgs e)
		{
			if (m_SizeChanging || (Processor == null))
			{
				return;
			}
			m_SizeChanging = true;
			Size = Processor.Size;
			m_SizeChanging = false;
		}

		#endregion Event Handlers
	}
}