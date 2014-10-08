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
			this.OutputContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.UnlinkMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.LinkOutputButton = new System.Windows.Forms.Button();
			this.LinkPanel = new System.Windows.Forms.Panel();
			this.OutputComboBox = new System.Windows.Forms.ComboBox();
			this.OutputColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.TargetColumn = new System.Windows.Forms.DataGridViewLinkColumn();
			this.InputColumn = new System.Windows.Forms.DataGridViewLinkColumn();
			((System.ComponentModel.ISupportInitialize)(this.OutputGridView)).BeginInit();
			this.OutputContextMenu.SuspendLayout();
			this.LinkPanel.SuspendLayout();
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
			// OutputContextMenu
			// 
			this.OutputContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UnlinkMenuItem});
			this.OutputContextMenu.Name = "OutputContextMenu";
			this.OutputContextMenu.Size = new System.Drawing.Size(109, 26);
			// 
			// UnlinkMenuItem
			// 
			this.UnlinkMenuItem.Name = "UnlinkMenuItem";
			this.UnlinkMenuItem.Size = new System.Drawing.Size(108, 22);
			this.UnlinkMenuItem.Text = "Unlink";
			this.UnlinkMenuItem.Click += new System.EventHandler(this.UnlinkMenuItem_Click);
			// 
			// LinkOutputButton
			// 
			this.LinkOutputButton.Dock = System.Windows.Forms.DockStyle.Right;
			this.LinkOutputButton.Location = new System.Drawing.Point(217, 0);
			this.LinkOutputButton.Name = "LinkOutputButton";
			this.LinkOutputButton.Size = new System.Drawing.Size(113, 23);
			this.LinkOutputButton.TabIndex = 2;
			this.LinkOutputButton.Text = "Link Output";
			this.LinkOutputButton.UseVisualStyleBackColor = true;
			this.LinkOutputButton.Click += new System.EventHandler(this.LinkOutputButton_Click);
			// 
			// LinkPanel
			// 
			this.LinkPanel.Controls.Add(this.OutputComboBox);
			this.LinkPanel.Controls.Add(this.LinkOutputButton);
			this.LinkPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.LinkPanel.Location = new System.Drawing.Point(0, 184);
			this.LinkPanel.Name = "LinkPanel";
			this.LinkPanel.Size = new System.Drawing.Size(330, 23);
			this.LinkPanel.TabIndex = 3;
			// 
			// OutputComboBox
			// 
			this.OutputComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.OutputComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.OutputComboBox.FormattingEnabled = true;
			this.OutputComboBox.Location = new System.Drawing.Point(0, 1);
			this.OutputComboBox.Name = "OutputComboBox";
			this.OutputComboBox.Size = new System.Drawing.Size(217, 21);
			this.OutputComboBox.TabIndex = 3;
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
			this.TargetColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.TargetColumn.VisitedLinkColor = System.Drawing.Color.Blue;
			// 
			// InputColumn
			// 
			this.InputColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.InputColumn.HeaderText = "InputChannel";
			this.InputColumn.Name = "InputColumn";
			this.InputColumn.VisitedLinkColor = System.Drawing.Color.Blue;
			// 
			// ProcessorOutputPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.OutputGridView);
			this.Controls.Add(this.LinkPanel);
			this.Name = "ProcessorOutputPanel";
			this.Size = new System.Drawing.Size(330, 207);
			((System.ComponentModel.ISupportInitialize)(this.OutputGridView)).EndInit();
			this.OutputContextMenu.ResumeLayout(false);
			this.LinkPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView OutputGridView;
		private System.Windows.Forms.Button LinkOutputButton;
		private System.Windows.Forms.ContextMenuStrip OutputContextMenu;
		private System.Windows.Forms.ToolStripMenuItem UnlinkMenuItem;
		private System.Windows.Forms.Panel LinkPanel;
		private System.Windows.Forms.ComboBox OutputComboBox;
		private System.Windows.Forms.DataGridViewTextBoxColumn OutputColumn;
		private System.Windows.Forms.DataGridViewLinkColumn TargetColumn;
		private System.Windows.Forms.DataGridViewLinkColumn InputColumn;
	}
}
