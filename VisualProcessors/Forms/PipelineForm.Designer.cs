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
			this.MainToolStrip = new System.Windows.Forms.ToolStrip();
			this.SimulationComboBox = new System.Windows.Forms.ToolStripComboBox();
			this.MdiClientContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.GotoMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.BringMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.AddMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.Toolbox = new VisualProcessors.Controls.ToolboxPanel();
			this.MainToolStrip.SuspendLayout();
			this.MdiClientContextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// MainToolStrip
			// 
			this.MainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SimulationComboBox});
			this.MainToolStrip.Location = new System.Drawing.Point(0, 0);
			this.MainToolStrip.Name = "MainToolStrip";
			this.MainToolStrip.Size = new System.Drawing.Size(693, 25);
			this.MainToolStrip.TabIndex = 1;
			this.MainToolStrip.Text = "toolStrip1";
			// 
			// SimulationComboBox
			// 
			this.SimulationComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.SimulationComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
			this.SimulationComboBox.Items.AddRange(new object[] {
            "Paused",
            "Running"});
			this.SimulationComboBox.Name = "SimulationComboBox";
			this.SimulationComboBox.Size = new System.Drawing.Size(121, 25);
			this.SimulationComboBox.SelectedIndexChanged += new System.EventHandler(this.SimulationComboBox_SelectedIndexChanged);
			// 
			// MdiClientContextMenu
			// 
			this.MdiClientContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddMenu,
            this.GotoMenu,
            this.BringMenu});
			this.MdiClientContextMenu.Name = "FormViewContextMenu";
			this.MdiClientContextMenu.Size = new System.Drawing.Size(157, 92);
			this.MdiClientContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.MdiClientContextMenuOpening);
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
			// AddMenu
			// 
			this.AddMenu.Name = "AddMenu";
			this.AddMenu.Size = new System.Drawing.Size(156, 22);
			this.AddMenu.Text = "Add Processor";
			// 
			// Toolbox
			// 
			this.Toolbox.AutoScroll = true;
			this.Toolbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Toolbox.Dock = System.Windows.Forms.DockStyle.Left;
			this.Toolbox.Location = new System.Drawing.Point(0, 25);
			this.Toolbox.Name = "Toolbox";
			this.Toolbox.Pipeline = null;
			this.Toolbox.Size = new System.Drawing.Size(175, 409);
			this.Toolbox.TabIndex = 0;
			// 
			// PipelineForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(693, 434);
			this.Controls.Add(this.Toolbox);
			this.Controls.Add(this.MainToolStrip);
			this.IsMdiContainer = true;
			this.Name = "PipelineForm";
			this.Text = "Form1";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Shown += new System.EventHandler(this.PipelineFormShown);
			this.MainToolStrip.ResumeLayout(false);
			this.MainToolStrip.PerformLayout();
			this.MdiClientContextMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip MainToolStrip;
		private System.Windows.Forms.ToolStripComboBox SimulationComboBox;
		private Controls.ToolboxPanel Toolbox;
		private System.Windows.Forms.ContextMenuStrip MdiClientContextMenu;
		private System.Windows.Forms.ToolStripMenuItem GotoMenu;
		private System.Windows.Forms.ToolStripMenuItem BringMenu;
		private System.Windows.Forms.ToolStripMenuItem AddMenu;
		

	}
}

