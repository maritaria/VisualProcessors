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
			m_PreviousTab = Tabs.SelectedTab;

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
			if (Processor.HideInputTab)
			{
				Tabs.TabPages.Remove(InputTab);
			}

			//Settings
			processor.GetUserInterface(SettingsPanel);
			SettingsTab.Text = Processor.SettingsTabLabel;
			if (Processor.HideSettingsTab)
			{
				Tabs.TabPages.Remove(SettingsTab);
			}

			//Output
			ProcessorOutputView.Pipeline = PipelineForm;
			ProcessorOutputView.Processor = Processor;
			if (Processor.HideOutputTab)
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
					ConfirmLinkButton.Enabled = !Processor.HideOutputTab && (Processor.OutputChannelCount > 0);
					break;

				case LinkMode.OutputFirst:
					ConfirmLinkButton.Text = "Link Input";
					ConfirmLinkButton.Enabled = !Processor.HideInputTab && (Processor.InputChannelCount > 0);
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
			if (!MinimalViewEnabled)
			{
				Processor.Location = this.Location;
			}
		}

		private void ProcessorForm_SizeChanged(object sender, EventArgs e)
		{
			if (!MinimalViewEnabled)
			{
				this.Processor.Size = this.Size;
			}
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

		#region Minimal View

		private FormBorderStyle m_PreviousFormBorderStyle;
		private Size m_PreviousSize;
		private TabPage m_PreviousTab;

		/// <summary>
		///  Whether to use minimalview on this form, switches to the 'Minimal' tab automaticly if
		///  required.
		/// </summary>
		public bool MinimalViewEnabled
		{
			get
			{
				return Tabs.SelectedTab == MinimalTab;
			}
			set
			{
				if (value != MinimalViewEnabled)
				{
					ToggleMinimalView();
				}
			}
		}

		/// <summary>
		///  Toggles the minimalviewmode
		/// </summary>
		public void ToggleMinimalView()
		{
			if (Tabs.SelectedTab != MinimalTab)
			{
				Tabs.SelectedTab = MinimalTab;
			}
			else
			{
				Tabs.SelectedTab = m_PreviousTab;
			}
		}

		private void Tabs_SelectedIndexChanged(object sender, EventArgs e)
		{
			Point oldcenter = this.GetCenter();
			bool modified = false;
			if (Tabs.SelectedTab == MinimalTab)
			{
				//Save state
				m_PreviousFormBorderStyle = this.FormBorderStyle;
				m_PreviousSize = this.Size;

				//Switch to minimal mode
				this.FormBorderStyle = FormBorderStyle.None;
				this.Size = new Size(250, 200);
				modified = true;
			}
			else if (m_PreviousTab == MinimalTab)
			{
				//Switch from minimal mode
				this.FormBorderStyle = m_PreviousFormBorderStyle;
				this.Size = m_PreviousSize;
				modified = true;
			}
			if (modified)
			{
				Point newcenter = this.GetCenter();
				Point offset = new Point(oldcenter.X - newcenter.X, oldcenter.Y - newcenter.Y);
				Point newpos = this.Location;
				newpos.Offset(offset);
				this.Location = newpos;
			}
			m_PreviousTab = Tabs.SelectedTab;
		}

		#endregion Minimal View
	}
}