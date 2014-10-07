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
			this.MdiClientContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.AddMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.GotoMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.BringMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.emptyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyFromExistingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyFromCurrentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.asdfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.exiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.simulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.clearDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveStateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadStateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.resourcesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.Toolbox = new VisualProcessors.Controls.ToolboxPanel();
			this.minimalizeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.minimalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MdiClientContextMenu.SuspendLayout();
			this.menuStrip1.SuspendLayout();
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
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.simulationToolStripMenuItem,
            this.resourcesToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(693, 24);
			this.menuStrip1.TabIndex = 3;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.asdfToolStripMenuItem,
            this.toolStripSeparator2,
            this.exiToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.emptyToolStripMenuItem,
            this.copyFromExistingToolStripMenuItem,
            this.copyFromCurrentToolStripMenuItem});
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
			this.newToolStripMenuItem.Text = "New";
			// 
			// emptyToolStripMenuItem
			// 
			this.emptyToolStripMenuItem.Name = "emptyToolStripMenuItem";
			this.emptyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.emptyToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
			this.emptyToolStripMenuItem.Text = "Empty";
			// 
			// copyFromExistingToolStripMenuItem
			// 
			this.copyFromExistingToolStripMenuItem.Name = "copyFromExistingToolStripMenuItem";
			this.copyFromExistingToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.N)));
			this.copyFromExistingToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
			this.copyFromExistingToolStripMenuItem.Text = "Copy from existing";
			// 
			// copyFromCurrentToolStripMenuItem
			// 
			this.copyFromCurrentToolStripMenuItem.Name = "copyFromCurrentToolStripMenuItem";
			this.copyFromCurrentToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.N)));
			this.copyFromCurrentToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
			this.copyFromCurrentToolStripMenuItem.Text = "Copy from current";
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.openToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
			this.openToolStripMenuItem.Text = "Open";
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
			this.saveToolStripMenuItem.Text = "Save";
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
			this.saveAsToolStripMenuItem.Text = "Save as...";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(190, 6);
			// 
			// asdfToolStripMenuItem
			// 
			this.asdfToolStripMenuItem.Name = "asdfToolStripMenuItem";
			this.asdfToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
			this.asdfToolStripMenuItem.Text = "Recent";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(190, 6);
			// 
			// exiToolStripMenuItem
			// 
			this.exiToolStripMenuItem.Name = "exiToolStripMenuItem";
			this.exiToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.exiToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
			this.exiToolStripMenuItem.Text = "Exit";
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator3,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator6,
            this.minimalizeAllToolStripMenuItem});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.editToolStripMenuItem.Text = "Edit";
			// 
			// undoToolStripMenuItem
			// 
			this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
			this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
			this.undoToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.undoToolStripMenuItem.Text = "Undo";
			// 
			// redoToolStripMenuItem
			// 
			this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
			this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
			this.redoToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.redoToolStripMenuItem.Text = "Redo";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(141, 6);
			// 
			// cutToolStripMenuItem
			// 
			this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
			this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.cutToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.cutToolStripMenuItem.Text = "Cut";
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.copyToolStripMenuItem.Text = "Copy";
			// 
			// pasteToolStripMenuItem
			// 
			this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
			this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
			this.pasteToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.pasteToolStripMenuItem.Text = "Paste";
			// 
			// simulationToolStripMenuItem
			// 
			this.simulationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.toolStripSeparator4,
            this.clearDataToolStripMenuItem,
            this.saveStateToolStripMenuItem,
            this.loadStateToolStripMenuItem,
            this.toolStripSeparator5});
			this.simulationToolStripMenuItem.Name = "simulationToolStripMenuItem";
			this.simulationToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
			this.simulationToolStripMenuItem.Text = "Simulation";
			// 
			// startToolStripMenuItem
			// 
			this.startToolStripMenuItem.Name = "startToolStripMenuItem";
			this.startToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
			this.startToolStripMenuItem.Text = "Run";
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(127, 6);
			// 
			// clearDataToolStripMenuItem
			// 
			this.clearDataToolStripMenuItem.Name = "clearDataToolStripMenuItem";
			this.clearDataToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
			this.clearDataToolStripMenuItem.Text = "Reset state";
			// 
			// saveStateToolStripMenuItem
			// 
			this.saveStateToolStripMenuItem.Name = "saveStateToolStripMenuItem";
			this.saveStateToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
			this.saveStateToolStripMenuItem.Text = "Save state";
			// 
			// loadStateToolStripMenuItem
			// 
			this.loadStateToolStripMenuItem.Name = "loadStateToolStripMenuItem";
			this.loadStateToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
			this.loadStateToolStripMenuItem.Text = "Load state";
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(127, 6);
			// 
			// resourcesToolStripMenuItem
			// 
			this.resourcesToolStripMenuItem.Name = "resourcesToolStripMenuItem";
			this.resourcesToolStripMenuItem.Size = new System.Drawing.Size(120, 20);
			this.resourcesToolStripMenuItem.Text = "Assembly Manager";
			// 
			// Toolbox
			// 
			this.Toolbox.AutoScroll = true;
			this.Toolbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Toolbox.Dock = System.Windows.Forms.DockStyle.Left;
			this.Toolbox.Location = new System.Drawing.Point(0, 24);
			this.Toolbox.Name = "Toolbox";
			this.Toolbox.Pipeline = null;
			this.Toolbox.Size = new System.Drawing.Size(175, 410);
			this.Toolbox.TabIndex = 0;
			// 
			// minimalizeAllToolStripMenuItem
			// 
			this.minimalizeAllToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.minimalToolStripMenuItem});
			this.minimalizeAllToolStripMenuItem.Name = "minimalizeAllToolStripMenuItem";
			this.minimalizeAllToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.minimalizeAllToolStripMenuItem.Text = "View";
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(149, 6);
			// 
			// minimalToolStripMenuItem
			// 
			this.minimalToolStripMenuItem.Name = "minimalToolStripMenuItem";
			this.minimalToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.minimalToolStripMenuItem.Text = "Minimal";
			// 
			// PipelineForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(693, 434);
			this.Controls.Add(this.Toolbox);
			this.Controls.Add(this.menuStrip1);
			this.IsMdiContainer = true;
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "PipelineForm";
			this.Text = "Pipeline Editor";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.MdiClientContextMenu.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Controls.ToolboxPanel Toolbox;
		private System.Windows.Forms.ContextMenuStrip MdiClientContextMenu;
		private System.Windows.Forms.ToolStripMenuItem GotoMenu;
		private System.Windows.Forms.ToolStripMenuItem BringMenu;
		private System.Windows.Forms.ToolStripMenuItem AddMenu;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem asdfToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem exiToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem simulationToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem clearDataToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem saveStateToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadStateToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem resourcesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem emptyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyFromExistingToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyFromCurrentToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem minimalizeAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem minimalToolStripMenuItem;
		

	}
}

