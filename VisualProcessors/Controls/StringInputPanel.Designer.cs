namespace VisualProcessors.Controls
{
	partial class StringInputPanel
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
			this.InputTextBox = new System.Windows.Forms.TextBox();
			this.InputLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// InputTextBox
			// 
			this.InputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.InputTextBox.Location = new System.Drawing.Point(47, 3);
			this.InputTextBox.Name = "InputTextBox";
			this.InputTextBox.Size = new System.Drawing.Size(168, 20);
			this.InputTextBox.TabIndex = 0;
			this.InputTextBox.TextChanged += new System.EventHandler(this.InputTextBox_TextChanged);
			this.InputTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InputTextBox_KeyDown);
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
			// StringInputPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.InputLabel);
			this.Controls.Add(this.InputTextBox);
			this.Name = "StringInputPanel";
			this.Size = new System.Drawing.Size(218, 26);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox InputTextBox;
		private System.Windows.Forms.Label InputLabel;
	}
}
