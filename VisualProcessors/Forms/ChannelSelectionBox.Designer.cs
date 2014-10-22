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
			this.ChannelListView = new System.Windows.Forms.ListView();
			this.SuspendLayout();
			// 
			// ChannelListView
			// 
			this.ChannelListView.Location = new System.Drawing.Point(12, 12);
			this.ChannelListView.MultiSelect = false;
			this.ChannelListView.Name = "ChannelListView";
			this.ChannelListView.Size = new System.Drawing.Size(512, 194);
			this.ChannelListView.TabIndex = 0;
			this.ChannelListView.UseCompatibleStateImageBehavior = false;
			this.ChannelListView.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
			// 
			// ChannelSelectionBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(536, 218);
			this.Controls.Add(this.ChannelListView);
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

		private System.Windows.Forms.ListView ChannelListView;

	}
}