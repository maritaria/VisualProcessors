namespace VisualProcessors.Forms
{
	partial class PipelineDataForm
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
			this.MdiMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.showProcessorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.layoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.arrangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cascadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tileHorizontallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tileVerticallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MdiMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// MdiMenuStrip
			// 
			this.MdiMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.layoutToolStripMenuItem,
            this.showProcessorToolStripMenuItem});
			this.MdiMenuStrip.Name = "MdiMenuStrip";
			this.MdiMenuStrip.Size = new System.Drawing.Size(158, 70);
			// 
			// showProcessorToolStripMenuItem
			// 
			this.showProcessorToolStripMenuItem.Name = "showProcessorToolStripMenuItem";
			this.showProcessorToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
			this.showProcessorToolStripMenuItem.Text = "Show Processor";
			// 
			// layoutToolStripMenuItem
			// 
			this.layoutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.arrangeToolStripMenuItem,
            this.cascadeToolStripMenuItem,
            this.tileHorizontallyToolStripMenuItem,
            this.tileVerticallyToolStripMenuItem});
			this.layoutToolStripMenuItem.Name = "layoutToolStripMenuItem";
			this.layoutToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
			this.layoutToolStripMenuItem.Text = "Layout";
			// 
			// arrangeToolStripMenuItem
			// 
			this.arrangeToolStripMenuItem.Name = "arrangeToolStripMenuItem";
			this.arrangeToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
			this.arrangeToolStripMenuItem.Text = "Arrange icons";
			this.arrangeToolStripMenuItem.Click += new System.EventHandler(this.arrangeToolStripMenuItem_Click);
			// 
			// cascadeToolStripMenuItem
			// 
			this.cascadeToolStripMenuItem.Name = "cascadeToolStripMenuItem";
			this.cascadeToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
			this.cascadeToolStripMenuItem.Text = "Cascade";
			this.cascadeToolStripMenuItem.Click += new System.EventHandler(this.cascadeToolStripMenuItem_Click);
			// 
			// tileHorizontallyToolStripMenuItem
			// 
			this.tileHorizontallyToolStripMenuItem.Name = "tileHorizontallyToolStripMenuItem";
			this.tileHorizontallyToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
			this.tileHorizontallyToolStripMenuItem.Text = "Tile horizontally";
			this.tileHorizontallyToolStripMenuItem.Click += new System.EventHandler(this.tileHorizontallyToolStripMenuItem_Click);
			// 
			// tileVerticallyToolStripMenuItem
			// 
			this.tileVerticallyToolStripMenuItem.Name = "tileVerticallyToolStripMenuItem";
			this.tileVerticallyToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
			this.tileVerticallyToolStripMenuItem.Text = "Tile vertically";
			this.tileVerticallyToolStripMenuItem.Click += new System.EventHandler(this.tileVerticallyToolStripMenuItem_Click);
			// 
			// PipelineDataForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(981, 401);
			this.ContextMenuStrip = this.MdiMenuStrip;
			this.IsMdiContainer = true;
			this.KeyPreview = true;
			this.Name = "PipelineDataForm";
			this.Text = "PipelineOutputForm";
			this.MdiMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip MdiMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem showProcessorToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem layoutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem arrangeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem cascadeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tileHorizontallyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tileVerticallyToolStripMenuItem;
	}
}