namespace VisualProcessors.Controls
{
	partial class ProcessorOutputPanel
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.OutputGridView = new System.Windows.Forms.DataGridView();
			this.LinkOutputButton = new System.Windows.Forms.Button();
			this.OutputColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.TargetColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.InputColumn = new System.Windows.Forms.DataGridViewLinkColumn();
			this.OutputContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.UnlinkMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.OutputGridView)).BeginInit();
			this.OutputContextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// OutputGridView
			// 
			this.OutputGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.OutputGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OutputColumn,
            this.TargetColumn,
            this.InputColumn});
			this.OutputGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.OutputGridView.Location = new System.Drawing.Point(0, 0);
			this.OutputGridView.Name = "OutputGridView";
			this.OutputGridView.RowTemplate.ContextMenuStrip = this.OutputContextMenu;
			this.OutputGridView.Size = new System.Drawing.Size(330, 184);
			this.OutputGridView.TabIndex = 1;
			this.OutputGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.OutputGridView_CellContentClick);
			// 
			// LinkOutputButton
			// 
			this.LinkOutputButton.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.LinkOutputButton.Location = new System.Drawing.Point(0, 184);
			this.LinkOutputButton.Name = "LinkOutputButton";
			this.LinkOutputButton.Size = new System.Drawing.Size(330, 23);
			this.LinkOutputButton.TabIndex = 2;
			this.LinkOutputButton.Text = "Link Output";
			this.LinkOutputButton.UseVisualStyleBackColor = true;
			this.LinkOutputButton.Click += new System.EventHandler(this.LinkOutputButton_Click);
			// 
			// OutputColumn
			// 
			this.OutputColumn.HeaderText = "OutputChannel";
			this.OutputColumn.Name = "OutputColumn";
			// 
			// TargetColumn
			// 
			this.TargetColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.TargetColumn.HeaderText = "Target";
			this.TargetColumn.Name = "TargetColumn";
			this.TargetColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			// 
			// InputColumn
			// 
			this.InputColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.InputColumn.HeaderText = "InputChannel";
			this.InputColumn.Name = "InputColumn";
			// 
			// OutputContextMenu
			// 
			this.OutputContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UnlinkMenuItem});
			this.OutputContextMenu.Name = "OutputContextMenu";
			this.OutputContextMenu.Size = new System.Drawing.Size(153, 48);
			// 
			// UnlinkMenuItem
			// 
			this.UnlinkMenuItem.Name = "UnlinkMenuItem";
			this.UnlinkMenuItem.Size = new System.Drawing.Size(152, 22);
			this.UnlinkMenuItem.Text = "Unlink";
			this.UnlinkMenuItem.Click += new System.EventHandler(this.UnlinkMenuItem_Click);
			// 
			// ProcessorOutputPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.OutputGridView);
			this.Controls.Add(this.LinkOutputButton);
			this.Name = "ProcessorOutputPanel";
			this.Size = new System.Drawing.Size(330, 207);
			((System.ComponentModel.ISupportInitialize)(this.OutputGridView)).EndInit();
			this.OutputContextMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView OutputGridView;
		private System.Windows.Forms.Button LinkOutputButton;
		private System.Windows.Forms.DataGridViewTextBoxColumn OutputColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn TargetColumn;
		private System.Windows.Forms.DataGridViewLinkColumn InputColumn;
		private System.Windows.Forms.ContextMenuStrip OutputContextMenu;
		private System.Windows.Forms.ToolStripMenuItem UnlinkMenuItem;
	}
}
