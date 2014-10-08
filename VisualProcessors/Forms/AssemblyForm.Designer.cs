namespace VisualProcessors.Forms
{
	partial class AssemblyForm
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
			this.ProcessorTree = new System.Windows.Forms.TreeView();
			this.InformationGroupBox = new System.Windows.Forms.GroupBox();
			this.InformationList = new System.Windows.Forms.ListBox();
			this.SplitView = new System.Windows.Forms.SplitContainer();
			this.InformationGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.SplitView)).BeginInit();
			this.SplitView.Panel1.SuspendLayout();
			this.SplitView.Panel2.SuspendLayout();
			this.SplitView.SuspendLayout();
			this.SuspendLayout();
			// 
			// ProcessorTree
			// 
			this.ProcessorTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ProcessorTree.Location = new System.Drawing.Point(3, 3);
			this.ProcessorTree.Name = "ProcessorTree";
			this.ProcessorTree.Size = new System.Drawing.Size(541, 229);
			this.ProcessorTree.TabIndex = 2;
			this.ProcessorTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.ProcessorTree_NodeMouseClick);
			this.ProcessorTree.Click += new System.EventHandler(this.ProcessorTree_Click);
			// 
			// InformationGroupBox
			// 
			this.InformationGroupBox.Controls.Add(this.InformationList);
			this.InformationGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.InformationGroupBox.Location = new System.Drawing.Point(0, 0);
			this.InformationGroupBox.Name = "InformationGroupBox";
			this.InformationGroupBox.Size = new System.Drawing.Size(547, 148);
			this.InformationGroupBox.TabIndex = 0;
			this.InformationGroupBox.TabStop = false;
			this.InformationGroupBox.Text = "Information";
			// 
			// InformationList
			// 
			this.InformationList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.InformationList.FormattingEnabled = true;
			this.InformationList.IntegralHeight = false;
			this.InformationList.Location = new System.Drawing.Point(3, 16);
			this.InformationList.Name = "InformationList";
			this.InformationList.Size = new System.Drawing.Size(541, 129);
			this.InformationList.TabIndex = 0;
			// 
			// SplitView
			// 
			this.SplitView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SplitView.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.SplitView.Location = new System.Drawing.Point(0, 0);
			this.SplitView.Name = "SplitView";
			this.SplitView.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// SplitView.Panel1
			// 
			this.SplitView.Panel1.Controls.Add(this.InformationGroupBox);
			// 
			// SplitView.Panel2
			// 
			this.SplitView.Panel2.Controls.Add(this.ProcessorTree);
			this.SplitView.Size = new System.Drawing.Size(547, 387);
			this.SplitView.SplitterDistance = 148;
			this.SplitView.TabIndex = 3;
			// 
			// AssemblyForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(547, 387);
			this.Controls.Add(this.SplitView);
			this.Name = "AssemblyForm";
			this.Text = "AssemblyForm";
			this.Load += new System.EventHandler(this.AssemblyForm_Load);
			this.InformationGroupBox.ResumeLayout(false);
			this.SplitView.Panel1.ResumeLayout(false);
			this.SplitView.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.SplitView)).EndInit();
			this.SplitView.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView ProcessorTree;
		private System.Windows.Forms.GroupBox InformationGroupBox;
		private System.Windows.Forms.ListBox InformationList;
		private System.Windows.Forms.SplitContainer SplitView;

	}
}