namespace VisualProcessors.Forms
{
	partial class ProcessorInterfaceForm
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
			this.UIPanel = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// UIPanel
			// 
			this.UIPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.UIPanel.Location = new System.Drawing.Point(0, 0);
			this.UIPanel.Name = "UIPanel";
			this.UIPanel.Size = new System.Drawing.Size(719, 363);
			this.UIPanel.TabIndex = 1;
			// 
			// ProcessorInterfaceForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(719, 363);
			this.Controls.Add(this.UIPanel);
			this.Name = "ProcessorInterfaceForm";
			this.Text = "GraphForm";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel UIPanel;
	}
}