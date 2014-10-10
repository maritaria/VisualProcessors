namespace VisualProcessors.Forms
{
	partial class PipelineForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PipelineForm));
			this.MdiClientContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.AddMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.GotoMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.BringMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.MainMenu = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.simulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.saveStateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadStateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.assemblyInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MainMenuSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.MainMenuOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.ErrorIcon = new System.Windows.Forms.ErrorProvider(this.components);
			this.MdiStatusStrip = new System.Windows.Forms.StatusStrip();
			this.SimulationStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.Toolbox = new VisualProcessors.Controls.ToolboxPanel();
			this.MdiClientContextMenu.SuspendLayout();
			this.MainMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ErrorIcon)).BeginInit();
			this.MdiStatusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// MdiClientContextMenu
			// 
			this.MdiClientContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddMenu,
            this.GotoMenu,
            this.BringMenu});
			this.MdiClientContextMenu.Name = "FormViewContextMenu";
			this.MdiClientContextMenu.Size = new System.Drawing.Size(157, 70);
			this.MdiClientContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.MdiClientContextMenuOpening);
			// 
			// AddMenu
			// 
			this.AddMenu.Name = "AddMenu";
			this.AddMenu.Size = new System.Drawing.Size(156, 22);
			this.AddMenu.Text = "Add Processor";
			// 
			// GotoMenu
			// 
			this.GotoMenu.Name = "GotoMenu";
			this.GotoMenu.Size = new System.Drawing.Size(156, 22);
			this.GotoMenu.Text = "Goto Processor";
			// 
			// BringMenu
			// 
			this.BringMenu.Name = "BringMenu";
			this.BringMenu.Size = new System.Drawing.Size(156, 22);
			this.BringMenu.Text = "Bring Processor";
			// 
			// MainMenu
			// 
			this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.simulationToolStripMenuItem,
            this.toolsToolStripMenuItem});
			this.MainMenu.Location = new System.Drawing.Point(0, 0);
			this.MainMenu.Name = "MainMenu";
			this.MainMenu.Size = new System.Drawing.Size(693, 24);
			this.MainMenu.TabIndex = 2;
			this.MainMenu.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.toolStripSeparator,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
			this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.newToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.newToolStripMenuItem.Text = "&New";
			this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
			this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.openToolStripMenuItem.Text = "&Open";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// closeToolStripMenuItem
			// 
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.closeToolStripMenuItem.Text = "Close";
			this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
			// 
			// toolStripSeparator
			// 
			this.toolStripSeparator.Name = "toolStripSeparator";
			this.toolStripSeparator.Size = new System.Drawing.Size(143, 6);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
			this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.saveToolStripMenuItem.Text = "&Save";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
			this.saveAsToolStripMenuItem.ShowShortcutKeys = false;
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.saveAsToolStripMenuItem.Text = "Save &As";
			this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(143, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// simulationToolStripMenuItem
			// 
			this.simulationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runToolStripMenuItem,
            this.resetToolStripMenuItem,
            this.toolStripSeparator6,
            this.saveStateToolStripMenuItem,
            this.loadStateToolStripMenuItem});
			this.simulationToolStripMenuItem.Name = "simulationToolStripMenuItem";
			this.simulationToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
			this.simulationToolStripMenuItem.Text = "&Simulation";
			// 
			// runToolStripMenuItem
			// 
			this.runToolStripMenuItem.Name = "runToolStripMenuItem";
			this.runToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
			this.runToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
			this.runToolStripMenuItem.Text = "Run";
			this.runToolStripMenuItem.Click += new System.EventHandler(this.runToolStripMenuItem_Click);
			// 
			// resetToolStripMenuItem
			// 
			this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
			this.resetToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.R)));
			this.resetToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
			this.resetToolStripMenuItem.Text = "Reset";
			this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(172, 6);
			// 
			// saveStateToolStripMenuItem
			// 
			this.saveStateToolStripMenuItem.Name = "saveStateToolStripMenuItem";
			this.saveStateToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
			this.saveStateToolStripMenuItem.Text = "Save state";
			// 
			// loadStateToolStripMenuItem
			// 
			this.loadStateToolStripMenuItem.Name = "loadStateToolStripMenuItem";
			this.loadStateToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
			this.loadStateToolStripMenuItem.Text = "Load state";
			// 
			// toolsToolStripMenuItem
			// 
			this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.assemblyInformationToolStripMenuItem,
            this.optionsToolStripMenuItem});
			this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
			this.toolsToolStripMenuItem.Text = "&Tools";
			// 
			// assemblyInformationToolStripMenuItem
			// 
			this.assemblyInformationToolStripMenuItem.Name = "assemblyInformationToolStripMenuItem";
			this.assemblyInformationToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
			this.assemblyInformationToolStripMenuItem.Text = "Assembly Information";
			this.assemblyInformationToolStripMenuItem.Click += new System.EventHandler(this.assemblyInformationToolStripMenuItem_Click);
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
			this.optionsToolStripMenuItem.Text = "&Options";
			// 
			// MainMenuSaveFileDialog
			// 
			this.MainMenuSaveFileDialog.Filter = "XML file|*.xml|All files|*.*";
			this.MainMenuSaveFileDialog.Title = "Save File";
			// 
			// MainMenuOpenFileDialog
			// 
			this.MainMenuOpenFileDialog.Filter = "XML file|*.xml|All files|*.*";
			this.MainMenuOpenFileDialog.Title = "Load File";
			// 
			// ErrorIcon
			// 
			this.ErrorIcon.ContainerControl = this;
			// 
			// MdiStatusStrip
			// 
			this.MdiStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SimulationStatusLabel});
			this.MdiStatusStrip.Location = new System.Drawing.Point(175, 412);
			this.MdiStatusStrip.Name = "MdiStatusStrip";
			this.MdiStatusStrip.Size = new System.Drawing.Size(518, 22);
			this.MdiStatusStrip.TabIndex = 4;
			this.MdiStatusStrip.Text = "statusStrip1";
			// 
			// SimulationStatusLabel
			// 
			this.SimulationStatusLabel.Name = "SimulationStatusLabel";
			this.SimulationStatusLabel.Size = new System.Drawing.Size(42, 17);
			this.SimulationStatusLabel.Text = "No file";
			// 
			// Toolbox
			// 
			this.Toolbox.AutoScroll = true;
			this.Toolbox.Dock = System.Windows.Forms.DockStyle.Left;
			this.Toolbox.Location = new System.Drawing.Point(0, 24);
			this.Toolbox.Name = "Toolbox";
			this.Toolbox.PipelineForm = null;
			this.Toolbox.Size = new System.Drawing.Size(175, 410);
			this.Toolbox.TabIndex = 0;
			// 
			// PipelineForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(693, 434);
			this.Controls.Add(this.MdiStatusStrip);
			this.Controls.Add(this.Toolbox);
			this.Controls.Add(this.MainMenu);
			this.IsMdiContainer = true;
			this.Name = "PipelineForm";
			this.Text = "Pipeline Editor";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.MdiClientContextMenu.ResumeLayout(false);
			this.MainMenu.ResumeLayout(false);
			this.MainMenu.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.ErrorIcon)).EndInit();
			this.MdiStatusStrip.ResumeLayout(false);
			this.MdiStatusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Controls.ToolboxPanel Toolbox;
		private System.Windows.Forms.ContextMenuStrip MdiClientContextMenu;
		private System.Windows.Forms.ToolStripMenuItem GotoMenu;
		private System.Windows.Forms.ToolStripMenuItem BringMenu;
		private System.Windows.Forms.ToolStripMenuItem AddMenu;
		private System.Windows.Forms.MenuStrip MainMenu;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem simulationToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem saveStateToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadStateToolStripMenuItem;
		private System.Windows.Forms.SaveFileDialog MainMenuSaveFileDialog;
		private System.Windows.Forms.OpenFileDialog MainMenuOpenFileDialog;
		private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem assemblyInformationToolStripMenuItem;
		private System.Windows.Forms.ErrorProvider ErrorIcon;
		private System.Windows.Forms.StatusStrip MdiStatusStrip;
		private System.Windows.Forms.ToolStripStatusLabel SimulationStatusLabel;
		

	}
}

