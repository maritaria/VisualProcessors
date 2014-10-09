namespace VisualProcessors.Controls
{
	partial class NumericInputPanel
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
			this.InputLabel = new System.Windows.Forms.Label();
			this.InputNumeric = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.InputNumeric)).BeginInit();
			this.SuspendLayout();
			// 
			// InputLabel
			// 
			this.InputLabel.AutoSize = true;
			this.InputLabel.Location = new System.Drawing.Point(3, 6);
			this.InputLabel.Name = "InputLabel";
			this.InputLabel.Size = new System.Drawing.Size(38, 13);
			this.InputLabel.TabIndex = 1;
			this.InputLabel.Text = "Name:";
			// 
			// InputNumeric
			// 
			this.InputNumeric.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.InputNumeric.Location = new System.Drawing.Point(47, 3);
			this.InputNumeric.Name = "InputNumeric";
			this.InputNumeric.ReadOnly = true;
			this.InputNumeric.Size = new System.Drawing.Size(168, 20);
			this.InputNumeric.TabIndex = 2;
			this.InputNumeric.ValueChanged += new System.EventHandler(this.InputNumeric_ValueChanged);
			// 
			// NumericInputPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.InputNumeric);
			this.Controls.Add(this.InputLabel);
			this.Name = "NumericInputPanel";
			this.Size = new System.Drawing.Size(218, 26);
			((System.ComponentModel.ISupportInitialize)(this.InputNumeric)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label InputLabel;
		private System.Windows.Forms.NumericUpDown InputNumeric;
	}
}
