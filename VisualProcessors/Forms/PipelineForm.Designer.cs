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
			this.MainToolStrip = new System.Windows.Forms.ToolStrip();
			this.SimulationComboBox = new System.Windows.Forms.ToolStripComboBox();
			this.MainToolStrip.SuspendLayout();
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
			// PipelineForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(693, 434);
			this.Controls.Add(this.MainToolStrip);
			this.IsMdiContainer = true;
			this.Name = "PipelineForm";
			this.Text = "Form1";
			this.Shown += new System.EventHandler(this.PipelineFormShown);
			this.MainToolStrip.ResumeLayout(false);
			this.MainToolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip MainToolStrip;
		private System.Windows.Forms.ToolStripComboBox SimulationComboBox;
		

	}
}

