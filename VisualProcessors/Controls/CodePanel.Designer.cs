namespace VisualProcessors.Controls
{
	partial class CodePanel
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
			this.CodeBox = new System.Windows.Forms.TextBox();
			this.ErrorList = new System.Windows.Forms.ListBox();
			this.MainSplitContainer = new System.Windows.Forms.SplitContainer();
			this.ButtonPanel = new System.Windows.Forms.Panel();
			this.CompileButton = new System.Windows.Forms.Button();
			this.LoadButton = new System.Windows.Forms.Button();
			this.SaveButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).BeginInit();
			this.MainSplitContainer.Panel1.SuspendLayout();
			this.MainSplitContainer.Panel2.SuspendLayout();
			this.MainSplitContainer.SuspendLayout();
			this.ButtonPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// CodeBox
			// 
			this.CodeBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CodeBox.Location = new System.Drawing.Point(0, 0);
			this.CodeBox.Multiline = true;
			this.CodeBox.Name = "CodeBox";
			this.CodeBox.Size = new System.Drawing.Size(554, 239);
			this.CodeBox.TabIndex = 0;
			// 
			// ErrorList
			// 
			this.ErrorList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ErrorList.FormattingEnabled = true;
			this.ErrorList.IntegralHeight = false;
			this.ErrorList.Location = new System.Drawing.Point(0, 0);
			this.ErrorList.Name = "ErrorList";
			this.ErrorList.Size = new System.Drawing.Size(416, 69);
			this.ErrorList.TabIndex = 1;
			// 
			// MainSplitContainer
			// 
			this.MainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.MainSplitContainer.Location = new System.Drawing.Point(0, 0);
			this.MainSplitContainer.Name = "MainSplitContainer";
			this.MainSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// MainSplitContainer.Panel1
			// 
			this.MainSplitContainer.Panel1.Controls.Add(this.CodeBox);
			this.MainSplitContainer.Panel1MinSize = 65;
			// 
			// MainSplitContainer.Panel2
			// 
			this.MainSplitContainer.Panel2.Controls.Add(this.ErrorList);
			this.MainSplitContainer.Panel2.Controls.Add(this.ButtonPanel);
			this.MainSplitContainer.Panel2MinSize = 68;
			this.MainSplitContainer.Size = new System.Drawing.Size(554, 312);
			this.MainSplitContainer.SplitterDistance = 239;
			this.MainSplitContainer.TabIndex = 2;
			// 
			// ButtonPanel
			// 
			this.ButtonPanel.Controls.Add(this.SaveButton);
			this.ButtonPanel.Controls.Add(this.LoadButton);
			this.ButtonPanel.Controls.Add(this.CompileButton);
			this.ButtonPanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.ButtonPanel.Location = new System.Drawing.Point(416, 0);
			this.ButtonPanel.Name = "ButtonPanel";
			this.ButtonPanel.Size = new System.Drawing.Size(138, 69);
			this.ButtonPanel.TabIndex = 2;
			// 
			// CompileButton
			// 
			this.CompileButton.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.CompileButton.Location = new System.Drawing.Point(0, 46);
			this.CompileButton.Name = "CompileButton";
			this.CompileButton.Size = new System.Drawing.Size(138, 23);
			this.CompileButton.TabIndex = 0;
			this.CompileButton.Text = "Compile";
			this.CompileButton.UseVisualStyleBackColor = true;
			// 
			// LoadButton
			// 
			this.LoadButton.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.LoadButton.Location = new System.Drawing.Point(0, 23);
			this.LoadButton.Name = "LoadButton";
			this.LoadButton.Size = new System.Drawing.Size(138, 23);
			this.LoadButton.TabIndex = 1;
			this.LoadButton.Text = "Load File";
			this.LoadButton.UseVisualStyleBackColor = true;
			// 
			// SaveButton
			// 
			this.SaveButton.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.SaveButton.Location = new System.Drawing.Point(0, 0);
			this.SaveButton.Name = "SaveButton";
			this.SaveButton.Size = new System.Drawing.Size(138, 23);
			this.SaveButton.TabIndex = 2;
			this.SaveButton.Text = "Save File";
			this.SaveButton.UseVisualStyleBackColor = true;
			// 
			// CodePanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.MainSplitContainer);
			this.Name = "CodePanel";
			this.Size = new System.Drawing.Size(554, 312);
			this.MainSplitContainer.Panel1.ResumeLayout(false);
			this.MainSplitContainer.Panel1.PerformLayout();
			this.MainSplitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).EndInit();
			this.MainSplitContainer.ResumeLayout(false);
			this.ButtonPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TextBox CodeBox;
		private System.Windows.Forms.ListBox ErrorList;
		private System.Windows.Forms.SplitContainer MainSplitContainer;
		private System.Windows.Forms.Panel ButtonPanel;
		private System.Windows.Forms.Button SaveButton;
		private System.Windows.Forms.Button LoadButton;
		private System.Windows.Forms.Button CompileButton;
	}
}
