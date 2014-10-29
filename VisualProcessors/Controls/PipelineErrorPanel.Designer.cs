namespace VisualProcessors.Controls
{
	partial class PipelineErrorPanel
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
			this.ErrorGridView = new System.Windows.Forms.DataGridView();
			this.TypeColumn = new System.Windows.Forms.DataGridViewImageColumn();
			this.SourceColumn = new System.Windows.Forms.DataGridViewLinkColumn();
			this.TitleColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.MessageColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.HaltTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.CallbackColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ErrorMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.clearAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.ErrorGridView)).BeginInit();
			this.ErrorMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// ErrorGridView
			// 
			this.ErrorGridView.AllowUserToAddRows = false;
			this.ErrorGridView.AllowUserToDeleteRows = false;
			this.ErrorGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.ErrorGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TypeColumn,
            this.SourceColumn,
            this.TitleColumn,
            this.MessageColumn,
            this.HaltTypeColumn,
            this.CallbackColumn});
			this.ErrorGridView.ContextMenuStrip = this.ErrorMenuStrip;
			this.ErrorGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ErrorGridView.Location = new System.Drawing.Point(0, 0);
			this.ErrorGridView.Name = "ErrorGridView";
			this.ErrorGridView.Size = new System.Drawing.Size(922, 282);
			this.ErrorGridView.TabIndex = 1;
			this.ErrorGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ErrorGridView_CellContentClick);
			// 
			// TypeColumn
			// 
			this.TypeColumn.HeaderText = "Type";
			this.TypeColumn.Name = "TypeColumn";
			this.TypeColumn.ReadOnly = true;
			this.TypeColumn.Width = 40;
			// 
			// SourceColumn
			// 
			this.SourceColumn.HeaderText = "Source";
			this.SourceColumn.Name = "SourceColumn";
			this.SourceColumn.ReadOnly = true;
			this.SourceColumn.Width = 120;
			// 
			// TitleColumn
			// 
			this.TitleColumn.HeaderText = "Title";
			this.TitleColumn.Name = "TitleColumn";
			this.TitleColumn.ReadOnly = true;
			this.TitleColumn.Width = 200;
			// 
			// MessageColumn
			// 
			this.MessageColumn.HeaderText = "Message";
			this.MessageColumn.Name = "MessageColumn";
			this.MessageColumn.ReadOnly = true;
			this.MessageColumn.Width = 400;
			// 
			// HaltTypeColumn
			// 
			this.HaltTypeColumn.HeaderText = "HaltType";
			this.HaltTypeColumn.Name = "HaltTypeColumn";
			this.HaltTypeColumn.ReadOnly = true;
			// 
			// CallbackColumn
			// 
			this.CallbackColumn.HeaderText = "Callback";
			this.CallbackColumn.Name = "CallbackColumn";
			this.CallbackColumn.ReadOnly = true;
			this.CallbackColumn.Visible = false;
			// 
			// ErrorMenuStrip
			// 
			this.ErrorMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem,
            this.clearAllToolStripMenuItem});
			this.ErrorMenuStrip.Name = "ErrorMenuStrip";
			this.ErrorMenuStrip.Size = new System.Drawing.Size(153, 70);
			// 
			// refreshToolStripMenuItem
			// 
			this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
			this.refreshToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.refreshToolStripMenuItem.Text = "Refresh";
			this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
			// 
			// clearAllToolStripMenuItem
			// 
			this.clearAllToolStripMenuItem.Name = "clearAllToolStripMenuItem";
			this.clearAllToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.clearAllToolStripMenuItem.Text = "Clear All";
			this.clearAllToolStripMenuItem.Click += new System.EventHandler(this.clearAllToolStripMenuItem_Click);
			// 
			// PipelineErrorPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.ErrorGridView);
			this.Name = "PipelineErrorPanel";
			this.Size = new System.Drawing.Size(922, 282);
			((System.ComponentModel.ISupportInitialize)(this.ErrorGridView)).EndInit();
			this.ErrorMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView ErrorGridView;
		private System.Windows.Forms.ContextMenuStrip ErrorMenuStrip;
		private System.Windows.Forms.DataGridViewImageColumn TypeColumn;
		private System.Windows.Forms.DataGridViewLinkColumn SourceColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn TitleColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn MessageColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn HaltTypeColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn CallbackColumn;
		private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem clearAllToolStripMenuItem;
	}
}
