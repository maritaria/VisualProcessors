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
		private PipelineForm m_PipelineForm;
		private Processor m_Processor;
		private Type m_ProcessorType;
		private bool m_LocationChanging = false;
		private bool m_SizeChanging = false;
		/// <summary>
		///  Gets the PipelineForm the ProcessorForm belongs to.
		/// </summary>
		public PipelineForm Pipeline
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

		/// <summary>
		///  Gets or sets whether the Link button should be visible
		/// </summary>
		public bool ShowLinkButton
		{
			get
			{
				return LinkEndpointButton.Visible;
			}
			set
			{
				LinkEndpointButton.Visible = value;
			}
		}

		#endregion Properties

		#region Constructor

		public ProcessorForm(PipelineForm pipeline, Processor processor, bool useModel)
		{
			InitializeComponent();

			//Initialize values
			m_PipelineForm = pipeline;
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
			ProcessorInputView.Pipeline = Pipeline;
			ProcessorInputView.Processor = Processor;
			if (Processor.HideInputTab)
			{
				Tabs.TabPages.Remove(InputTab);
			}

			//Settings
			processor.GetUserInterface(SettingsPanel);
			if (Processor.HideSettingsTab)
			{
				Tabs.TabPages.Remove(SettingsTab);
			}

			//Output
			ProcessorOutputView.Pipeline = Pipeline;
			ProcessorOutputView.Processor = Processor;
			if (Processor.HideOutputTab)
			{
				Tabs.TabPages.Remove(OutputTab);
			}
		}

		void ProcessorSizeChanged(object sender, EventArgs e)
		{
			if (m_SizeChanging || (Processor==null))
			{
				return;
			}
			m_SizeChanging = true;
			Size = Processor.Size;
			m_SizeChanging = false;
		}

		void ProcessorLocationChanged(object sender, EventArgs e)
		{
			if (m_LocationChanging || (Processor == null))
			{
				return;
			}
			m_LocationChanging = true;
			Location = Processor.Location;
			m_LocationChanging = false;
		}

		#endregion Constructor

		#region Methods

		public void ShowInputChannel(string name)
		{
			if (ProcessorInputView.ShowInputChannel(name))
			{
				Tabs.SelectTab(InputTab);
			}
		}

		public void ForceClose()
		{
			m_Closing = true;
			Close();
		}

		#endregion Methods

		#region Events

		#endregion Events

		#region Event Handlers

		private void LinkEndpointButton_Click(object sender, EventArgs e)
		{
			Pipeline.CompleteLinkMode(this);
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
					Pipeline.RemoveProcessor(this.Processor.Name);
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

		private void ProcessorNameChanged(Processor p, string oldname, string newname)
		{
			Text = Processor.GetType().Name + ": " + newname;
		}

		#endregion Event Handlers

		#region Minimal View

		private FormBorderStyle m_PreviousFormBorderStyle;
		private Size m_PreviousSize;
		private TabPage m_PreviousTab;

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

		private void ProcessorForm_SizeChanged(object sender, EventArgs e)
		{
			if (!MinimalViewEnabled)
			{
				this.Processor.Size = this.Size;
			}
		}
	}
}