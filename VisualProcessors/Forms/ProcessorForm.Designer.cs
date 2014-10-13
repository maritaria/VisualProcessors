namespace VisualProcessors.Forms
{
	partial class ProcessorForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.ConfirmLinkButton = new System.Windows.Forms.Button();
			this.LinkButtons = new System.Windows.Forms.SplitContainer();
			this.CancelLinkButton = new System.Windows.Forms.Button();
			this.OutputTab = new System.Windows.Forms.TabPage();
			this.ProcessorOutputView = new VisualProcessors.Controls.ProcessorOutputPanel();
			this.PropertiesTab = new System.Windows.Forms.TabPage();
			this.CustomPanel = new System.Windows.Forms.Panel();
			this.InputTab = new System.Windows.Forms.TabPage();
			this.ProcessorInputView = new VisualProcessors.Controls.ProcessorInputPanel();
			this.Tabs = new System.Windows.Forms.TabControl();
			this.CustomTab = new System.Windows.Forms.TabPage();
			this.PropertyGrid = new System.Windows.Forms.PropertyGrid();
			((System.ComponentModel.ISupportInitialize)(this.LinkButtons)).BeginInit();
			this.LinkButtons.Panel1.SuspendLayout();
			this.LinkButtons.Panel2.SuspendLayout();
			this.LinkButtons.SuspendLayout();
			this.OutputTab.SuspendLayout();
			this.PropertiesTab.SuspendLayout();
			this.InputTab.SuspendLayout();
			this.Tabs.SuspendLayout();
			this.CustomTab.SuspendLayout();
			this.SuspendLayout();
			// 
			// ConfirmLinkButton
			// 
			this.ConfirmLinkButton.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ConfirmLinkButton.Location = new System.Drawing.Point(0, 0);
			this.ConfirmLinkButton.Name = "ConfirmLinkButton";
			this.ConfirmLinkButton.Size = new System.Drawing.Size(198, 23);
			this.ConfirmLinkButton.TabIndex = 2;
			this.ConfirmLinkButton.Text = "Link Endpoint";
			this.ConfirmLinkButton.UseVisualStyleBackColor = true;
			this.ConfirmLinkButton.Click += new System.EventHandler(this.LinkEndpointButton_Click);
			// 
			// LinkButtons
			// 
			this.LinkButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.LinkButtons.IsSplitterFixed = true;
			this.LinkButtons.Location = new System.Drawing.Point(0, 200);
			this.LinkButtons.Name = "LinkButtons";
			// 
			// LinkButtons.Panel1
			// 
			this.LinkButtons.Panel1.Controls.Add(this.CancelLinkButton);
			// 
			// LinkButtons.Panel2
			// 
			this.LinkButtons.Panel2.Controls.Add(this.ConfirmLinkButton);
			this.LinkButtons.Size = new System.Drawing.Size(398, 23);
			this.LinkButtons.SplitterDistance = 198;
			this.LinkButtons.SplitterWidth = 2;
			this.LinkButtons.TabIndex = 3;
			this.LinkButtons.Visible = false;
			// 
			// CancelLinkButton
			// 
			this.CancelLinkButton.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CancelLinkButton.Location = new System.Drawing.Point(0, 0);
			this.CancelLinkButton.Name = "CancelLinkButton";
			this.CancelLinkButton.Size = new System.Drawing.Size(198, 23);
			this.CancelLinkButton.TabIndex = 0;
			this.CancelLinkButton.Text = "Cancel";
			this.CancelLinkButton.UseVisualStyleBackColor = true;
			// 
			// OutputTab
			// 
			this.OutputTab.Controls.Add(this.ProcessorOutputView);
			this.OutputTab.Location = new System.Drawing.Point(4, 22);
			this.OutputTab.Name = "OutputTab";
			this.OutputTab.Padding = new System.Windows.Forms.Padding(3);
			this.OutputTab.Size = new System.Drawing.Size(390, 174);
			this.OutputTab.TabIndex = 0;
			this.OutputTab.Text = "Output";
			this.OutputTab.UseVisualStyleBackColor = true;
			// 
			// ProcessorOutputView
			// 
			this.ProcessorOutputView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ProcessorOutputView.Location = new System.Drawing.Point(3, 3);
			this.ProcessorOutputView.Name = "ProcessorOutputView";
			this.ProcessorOutputView.Pipeline = null;
			this.ProcessorOutputView.Processor = null;
			this.ProcessorOutputView.Size = new System.Drawing.Size(384, 168);
			this.ProcessorOutputView.TabIndex = 0;
			// 
			// PropertiesTab
			// 
			this.PropertiesTab.Controls.Add(this.PropertyGrid);
			this.PropertiesTab.Location = new System.Drawing.Point(4, 22);
			this.PropertiesTab.Name = "PropertiesTab";
			this.PropertiesTab.Size = new System.Drawing.Size(390, 174);
			this.PropertiesTab.TabIndex = 2;
			this.PropertiesTab.Text = "Properties";
			this.PropertiesTab.UseVisualStyleBackColor = true;
			// 
			// CustomPanel
			// 
			this.CustomPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CustomPanel.Location = new System.Drawing.Point(0, 0);
			this.CustomPanel.Name = "CustomPanel";
			this.CustomPanel.Size = new System.Drawing.Size(390, 174);
			this.CustomPanel.TabIndex = 0;
			// 
			// InputTab
			// 
			this.InputTab.Controls.Add(this.ProcessorInputView);
			this.InputTab.Location = new System.Drawing.Point(4, 22);
			this.InputTab.Name = "InputTab";
			this.InputTab.Padding = new System.Windows.Forms.Padding(3);
			this.InputTab.Size = new System.Drawing.Size(390, 174);
			this.InputTab.TabIndex = 1;
			this.InputTab.Text = "Input";
			this.InputTab.UseVisualStyleBackColor = true;
			// 
			// ProcessorInputView
			// 
			this.ProcessorInputView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ProcessorInputView.Location = new System.Drawing.Point(3, 3);
			this.ProcessorInputView.Name = "ProcessorInputView";
			this.ProcessorInputView.Pipeline = null;
			this.ProcessorInputView.Processor = null;
			this.ProcessorInputView.SelectedInputChannel = null;
			this.ProcessorInputView.Size = new System.Drawing.Size(384, 168);
			this.ProcessorInputView.TabIndex = 0;
			// 
			// Tabs
			// 
			this.Tabs.Controls.Add(this.InputTab);
			this.Tabs.Controls.Add(this.PropertiesTab);
			this.Tabs.Controls.Add(this.CustomTab);
			this.Tabs.Controls.Add(this.OutputTab);
			this.Tabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Tabs.Location = new System.Drawing.Point(0, 0);
			this.Tabs.Name = "Tabs";
			this.Tabs.SelectedIndex = 0;
			this.Tabs.Size = new System.Drawing.Size(398, 200);
			this.Tabs.TabIndex = 0;
			// 
			// CustomTab
			// 
			this.CustomTab.Controls.Add(this.CustomPanel);
			this.CustomTab.Location = new System.Drawing.Point(4, 22);
			this.CustomTab.Name = "CustomTab";
			this.CustomTab.Size = new System.Drawing.Size(390, 174);
			this.CustomTab.TabIndex = 3;
			this.CustomTab.Text = "Custom";
			this.CustomTab.UseVisualStyleBackColor = true;
			// 
			// PropertyGrid
			// 
			this.PropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PropertyGrid.Location = new System.Drawing.Point(0, 0);
			this.PropertyGrid.Name = "PropertyGrid";
			this.PropertyGrid.Size = new System.Drawing.Size(390, 174);
			this.PropertyGrid.TabIndex = 0;
			// 
			// ProcessorForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(398, 223);
			this.Controls.Add(this.Tabs);
			this.Controls.Add(this.LinkButtons);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ProcessorForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "ProcessorForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProcessorForm_FormClosing);
			this.LocationChanged += new System.EventHandler(this.ProcessorForm_LocationChanged);
			this.SizeChanged += new System.EventHandler(this.ProcessorForm_SizeChanged);
			this.LinkButtons.Panel1.ResumeLayout(false);
			this.LinkButtons.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.LinkButtons)).EndInit();
			this.LinkButtons.ResumeLayout(false);
			this.OutputTab.ResumeLayout(false);
			this.PropertiesTab.ResumeLayout(false);
			this.InputTab.ResumeLayout(false);
			this.Tabs.ResumeLayout(false);
			this.CustomTab.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button ConfirmLinkButton;
		private System.Windows.Forms.SplitContainer LinkButtons;
		private System.Windows.Forms.Button CancelLinkButton;
		private System.Windows.Forms.TabPage OutputTab;
		private Controls.ProcessorOutputPanel ProcessorOutputView;
		private System.Windows.Forms.TabPage PropertiesTab;
		private System.Windows.Forms.Panel CustomPanel;
		private System.Windows.Forms.TabPage InputTab;
		private Controls.ProcessorInputPanel ProcessorInputView;
		private System.Windows.Forms.TabControl Tabs;
		private System.Windows.Forms.TabPage CustomTab;
		private System.Windows.Forms.PropertyGrid PropertyGrid;

	}
}