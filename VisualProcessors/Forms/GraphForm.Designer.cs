namespace VisualProcessors.Forms
{
	partial class GraphForm
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
			this.Graph = new ZedGraph.ZedGraphControl();
			this.SuspendLayout();
			// 
			// Graph
			// 
			this.Graph.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Graph.Location = new System.Drawing.Point(0, 0);
			this.Graph.Name = "Graph";
			this.Graph.ScrollGrace = 0D;
			this.Graph.ScrollMaxX = 0D;
			this.Graph.ScrollMaxY = 0D;
			this.Graph.ScrollMaxY2 = 0D;
			this.Graph.ScrollMinX = 0D;
			this.Graph.ScrollMinY = 0D;
			this.Graph.ScrollMinY2 = 0D;
			this.Graph.Size = new System.Drawing.Size(284, 262);
			this.Graph.TabIndex = 0;
			// 
			// GraphForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Controls.Add(this.Graph);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "GraphForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "GraphForm";
			this.ResumeLayout(false);

		}

		#endregion

		private ZedGraph.ZedGraphControl Graph;

	}
}