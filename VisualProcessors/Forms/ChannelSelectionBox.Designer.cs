namespace VisualProcessors.Forms
{
	partial class ChannelSelectionBox
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
			this.ConfirmButton = new System.Windows.Forms.Button();
			this.ChannelComboBox = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// ConfirmButton
			// 
			this.ConfirmButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ConfirmButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.ConfirmButton.Location = new System.Drawing.Point(217, 12);
			this.ConfirmButton.Name = "ConfirmButton";
			this.ConfirmButton.Size = new System.Drawing.Size(55, 21);
			this.ConfirmButton.TabIndex = 0;
			this.ConfirmButton.Text = "Confirm";
			this.ConfirmButton.UseVisualStyleBackColor = true;
			// 
			// ChannelComboBox
			// 
			this.ChannelComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ChannelComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ChannelComboBox.FormattingEnabled = true;
			this.ChannelComboBox.Location = new System.Drawing.Point(12, 12);
			this.ChannelComboBox.Name = "ChannelComboBox";
			this.ChannelComboBox.Size = new System.Drawing.Size(199, 21);
			this.ChannelComboBox.TabIndex = 1;
			// 
			// ChannelSelectionBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 45);
			this.Controls.Add(this.ChannelComboBox);
			this.Controls.Add(this.ConfirmButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ChannelSelectionBox";
			this.ShowIcon = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "ChannelSelectionBox";
			this.Load += new System.EventHandler(this.ChannelSelectionBox_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button ConfirmButton;
		private System.Windows.Forms.ComboBox ChannelComboBox;
	}
}