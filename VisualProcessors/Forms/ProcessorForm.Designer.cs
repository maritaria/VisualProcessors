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
			this.components = new System.ComponentModel.Container();
			this.Tabs = new System.Windows.Forms.TabControl();
			this.InputTab = new System.Windows.Forms.TabPage();
			this.ProcessorInputView = new VisualProcessors.Controls.ProcessorInputPanel();
			this.SettingsTab = new System.Windows.Forms.TabPage();
			this.OutputTab = new System.Windows.Forms.TabPage();
			this.ProcessorOutputView = new VisualProcessors.Controls.ProcessorOutputPanel();
			this.MinimalTab = new System.Windows.Forms.TabPage();
			this.LinkEndpointButton = new System.Windows.Forms.Button();
			this.ErrorIcon = new System.Windows.Forms.ErrorProvider(this.components);
			this.SettingsPanel = new System.Windows.Forms.Panel();
			this.Tabs.SuspendLayout();
			this.InputTab.SuspendLayout();
			this.SettingsTab.SuspendLayout();
			this.OutputTab.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ErrorIcon)).BeginInit();
			this.SuspendLayout();
			// 
			// Tabs
			// 
			this.Tabs.Controls.Add(this.InputTab);
			this.Tabs.Controls.Add(this.SettingsTab);
			this.Tabs.Controls.Add(this.OutputTab);
			this.Tabs.Controls.Add(this.MinimalTab);
			this.Tabs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Tabs.Location = new System.Drawing.Point(0, 0);
			this.Tabs.Name = "Tabs";
			this.Tabs.SelectedIndex = 0;
			this.Tabs.Size = new System.Drawing.Size(378, 198);
			this.Tabs.TabIndex = 0;
			this.Tabs.SelectedIndexChanged += new System.EventHandler(this.Tabs_SelectedIndexChanged);
			// 
			// InputTab
			// 
			this.InputTab.Controls.Add(this.ProcessorInputView);
			this.InputTab.Location = new System.Drawing.Point(4, 22);
			this.InputTab.Name = "InputTab";
			this.InputTab.Padding = new System.Windows.Forms.Padding(3);
			this.InputTab.Size = new System.Drawing.Size(370, 172);
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
			this.ProcessorInputView.Size = new System.Drawing.Size(364, 166);
			this.ProcessorInputView.TabIndex = 0;
			// 
			// SettingsTab
			// 
			this.SettingsTab.Controls.Add(this.SettingsPanel);
			this.SettingsTab.Location = new System.Drawing.Point(4, 22);
			this.SettingsTab.Name = "SettingsTab";
			this.SettingsTab.Size = new System.Drawing.Size(370, 172);
			this.SettingsTab.TabIndex = 2;
			this.SettingsTab.Text = "Settings";
			this.SettingsTab.UseVisualStyleBackColor = true;
			// 
			// OutputTab
			// 
			this.OutputTab.Controls.Add(this.ProcessorOutputView);
			this.OutputTab.Location = new System.Drawing.Point(4, 22);
			this.OutputTab.Name = "OutputTab";
			this.OutputTab.Padding = new System.Windows.Forms.Padding(3);
			this.OutputTab.Size = new System.Drawing.Size(370, 172);
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
			this.ProcessorOutputView.Size = new System.Drawing.Size(364, 166);
			this.ProcessorOutputView.TabIndex = 0;
			// 
			// MinimalTab
			// 
			this.MinimalTab.Location = new System.Drawing.Point(4, 22);
			this.MinimalTab.Name = "MinimalTab";
			this.MinimalTab.Size = new System.Drawing.Size(370, 172);
			this.MinimalTab.TabIndex = 3;
			this.MinimalTab.Text = "Minimal";
			this.MinimalTab.UseVisualStyleBackColor = true;
			// 
			// LinkEndpointButton
			// 
			this.LinkEndpointButton.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.LinkEndpointButton.Location = new System.Drawing.Point(0, 198);
			this.LinkEndpointButton.Name = "LinkEndpointButton";
			this.LinkEndpointButton.Size = new System.Drawing.Size(378, 25);
			this.LinkEndpointButton.TabIndex = 2;
			this.LinkEndpointButton.Text = "Link Endpoint";
			this.LinkEndpointButton.UseVisualStyleBackColor = true;
			this.LinkEndpointButton.Visible = false;
			this.LinkEndpointButton.Click += new System.EventHandler(this.LinkEndpointButton_Click);
			// 
			// ErrorIcon
			// 
			this.ErrorIcon.ContainerControl = this;
			// 
			// SettingsPanel
			// 
			this.SettingsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SettingsPanel.Location = new System.Drawing.Point(0, 0);
			this.SettingsPanel.Name = "SettingsPanel";
			this.SettingsPanel.Size = new System.Drawing.Size(370, 172);
			this.SettingsPanel.TabIndex = 0;
			// 
			// ProcessorForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(378, 223);
			this.Controls.Add(this.Tabs);
			this.Controls.Add(this.LinkEndpointButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ProcessorForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "ProcessorForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProcessorForm_FormClosing);
			this.LocationChanged += new System.EventHandler(this.ProcessorForm_LocationChanged);
			this.Tabs.ResumeLayout(false);
			this.InputTab.ResumeLayout(false);
			this.SettingsTab.ResumeLayout(false);
			this.OutputTab.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ErrorIcon)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button LinkEndpointButton;
		private System.Windows.Forms.ErrorProvider ErrorIcon;
		private Controls.ProcessorOutputPanel ProcessorOutputView;
		private System.Windows.Forms.TabControl Tabs;
		private System.Windows.Forms.TabPage InputTab;
		private System.Windows.Forms.TabPage OutputTab;
		private System.Windows.Forms.TabPage SettingsTab;
		private System.Windows.Forms.TabPage MinimalTab;
		private Controls.ProcessorInputPanel ProcessorInputView;
		private System.Windows.Forms.Panel SettingsPanel;

	}
}