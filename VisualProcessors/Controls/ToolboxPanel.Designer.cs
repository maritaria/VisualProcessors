namespace VisualProcessors.Controls
{
	partial class ToolboxPanel
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
			this.ButtonPanel = new System.Windows.Forms.Panel();
			this.ButtonGroupBox = new System.Windows.Forms.GroupBox();
			this.DescriptionTooltip = new System.Windows.Forms.ToolTip(this.components);
			this.MainHorizontalSplit = new System.Windows.Forms.SplitContainer();
			this.ProcessorPropertyGrid = new System.Windows.Forms.PropertyGrid();
			this.ButtonGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.MainHorizontalSplit)).BeginInit();
			this.MainHorizontalSplit.Panel1.SuspendLayout();
			this.MainHorizontalSplit.Panel2.SuspendLayout();
			this.MainHorizontalSplit.SuspendLayout();
			this.SuspendLayout();
			// 
			// ButtonPanel
			// 
			this.ButtonPanel.AutoScroll = true;
			this.ButtonPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ButtonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ButtonPanel.Location = new System.Drawing.Point(3, 16);
			this.ButtonPanel.Name = "ButtonPanel";
			this.ButtonPanel.Size = new System.Drawing.Size(147, 188);
			this.ButtonPanel.TabIndex = 0;
			// 
			// ButtonGroupBox
			// 
			this.ButtonGroupBox.Controls.Add(this.ButtonPanel);
			this.ButtonGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ButtonGroupBox.Location = new System.Drawing.Point(0, 0);
			this.ButtonGroupBox.Name = "ButtonGroupBox";
			this.ButtonGroupBox.Size = new System.Drawing.Size(153, 207);
			this.ButtonGroupBox.TabIndex = 0;
			this.ButtonGroupBox.TabStop = false;
			this.ButtonGroupBox.Text = "New Processor";
			// 
			// DescriptionTooltip
			// 
			this.DescriptionTooltip.ToolTipTitle = "Description";
			// 
			// MainHorizontalSplit
			// 
			this.MainHorizontalSplit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainHorizontalSplit.Location = new System.Drawing.Point(0, 0);
			this.MainHorizontalSplit.Name = "MainHorizontalSplit";
			this.MainHorizontalSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// MainHorizontalSplit.Panel1
			// 
			this.MainHorizontalSplit.Panel1.Controls.Add(this.ButtonGroupBox);
			// 
			// MainHorizontalSplit.Panel2
			// 
			this.MainHorizontalSplit.Panel2.Controls.Add(this.ProcessorPropertyGrid);
			this.MainHorizontalSplit.Size = new System.Drawing.Size(153, 414);
			this.MainHorizontalSplit.SplitterDistance = 207;
			this.MainHorizontalSplit.TabIndex = 1;
			// 
			// ProcessorPropertyGrid
			// 
			this.ProcessorPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ProcessorPropertyGrid.Location = new System.Drawing.Point(0, 0);
			this.ProcessorPropertyGrid.Name = "ProcessorPropertyGrid";
			this.ProcessorPropertyGrid.Size = new System.Drawing.Size(153, 203);
			this.ProcessorPropertyGrid.TabIndex = 0;
			// 
			// ToolboxPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.Controls.Add(this.MainHorizontalSplit);
			this.Name = "ToolboxPanel";
			this.Size = new System.Drawing.Size(153, 414);
			this.ButtonGroupBox.ResumeLayout(false);
			this.MainHorizontalSplit.Panel1.ResumeLayout(false);
			this.MainHorizontalSplit.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.MainHorizontalSplit)).EndInit();
			this.MainHorizontalSplit.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel ButtonPanel;
		private System.Windows.Forms.GroupBox ButtonGroupBox;
		private System.Windows.Forms.ToolTip DescriptionTooltip;
		private System.Windows.Forms.SplitContainer MainHorizontalSplit;
		private System.Windows.Forms.PropertyGrid ProcessorPropertyGrid;


	}
}
