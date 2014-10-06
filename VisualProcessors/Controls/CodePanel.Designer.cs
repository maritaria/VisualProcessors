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
			this.SaveButton = new System.Windows.Forms.Button();
			this.LoadButton = new System.Windows.Forms.Button();
			this.CompileButton = new System.Windows.Forms.Button();
			this.ApplyButton = new System.Windows.Forms.Button();
			this.LoadCodeDialog = new System.Windows.Forms.OpenFileDialog();
			this.SaveCodeDialog = new System.Windows.Forms.SaveFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).BeginInit();
			this.MainSplitContainer.Panel1.SuspendLayout();
			this.MainSplitContainer.Panel2.SuspendLayout();
			this.MainSplitContainer.SuspendLayout();
			this.ButtonPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// CodeBox
			// 
			this.CodeBox.AcceptsReturn = true;
			this.CodeBox.AcceptsTab = true;
			this.CodeBox.AllowDrop = true;
			this.CodeBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CodeBox.Location = new System.Drawing.Point(0, 0);
			this.CodeBox.Multiline = true;
			this.CodeBox.Name = "CodeBox";
			this.CodeBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.CodeBox.Size = new System.Drawing.Size(554, 217);
			this.CodeBox.TabIndex = 0;
			this.CodeBox.WordWrap = false;
			this.CodeBox.TextChanged += new System.EventHandler(this.CodeBox_TextChanged);
			this.CodeBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.CodeBox_DragDrop);
			this.CodeBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.CodeBox_DragEnter);
			// 
			// ErrorList
			// 
			this.ErrorList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ErrorList.FormattingEnabled = true;
			this.ErrorList.IntegralHeight = false;
			this.ErrorList.Location = new System.Drawing.Point(0, 0);
			this.ErrorList.Name = "ErrorList";
			this.ErrorList.Size = new System.Drawing.Size(439, 91);
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
			this.MainSplitContainer.Panel1MinSize = 0;
			// 
			// MainSplitContainer.Panel2
			// 
			this.MainSplitContainer.Panel2.Controls.Add(this.ErrorList);
			this.MainSplitContainer.Panel2.Controls.Add(this.ButtonPanel);
			this.MainSplitContainer.Panel2MinSize = 91;
			this.MainSplitContainer.Size = new System.Drawing.Size(554, 312);
			this.MainSplitContainer.SplitterDistance = 217;
			this.MainSplitContainer.TabIndex = 2;
			// 
			// ButtonPanel
			// 
			this.ButtonPanel.Controls.Add(this.SaveButton);
			this.ButtonPanel.Controls.Add(this.LoadButton);
			this.ButtonPanel.Controls.Add(this.CompileButton);
			this.ButtonPanel.Controls.Add(this.ApplyButton);
			this.ButtonPanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.ButtonPanel.Location = new System.Drawing.Point(439, 0);
			this.ButtonPanel.Name = "ButtonPanel";
			this.ButtonPanel.Size = new System.Drawing.Size(115, 91);
			this.ButtonPanel.TabIndex = 2;
			// 
			// SaveButton
			// 
			this.SaveButton.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.SaveButton.Location = new System.Drawing.Point(0, -1);
			this.SaveButton.Name = "SaveButton";
			this.SaveButton.Size = new System.Drawing.Size(115, 23);
			this.SaveButton.TabIndex = 2;
			this.SaveButton.Text = "Save Code";
			this.SaveButton.UseVisualStyleBackColor = true;
			this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
			// 
			// LoadButton
			// 
			this.LoadButton.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.LoadButton.Location = new System.Drawing.Point(0, 22);
			this.LoadButton.Name = "LoadButton";
			this.LoadButton.Size = new System.Drawing.Size(115, 23);
			this.LoadButton.TabIndex = 1;
			this.LoadButton.Text = "Load Code";
			this.LoadButton.UseVisualStyleBackColor = true;
			this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
			// 
			// CompileButton
			// 
			this.CompileButton.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.CompileButton.Location = new System.Drawing.Point(0, 45);
			this.CompileButton.Name = "CompileButton";
			this.CompileButton.Size = new System.Drawing.Size(115, 23);
			this.CompileButton.TabIndex = 0;
			this.CompileButton.Text = "Compile";
			this.CompileButton.UseVisualStyleBackColor = true;
			this.CompileButton.Click += new System.EventHandler(this.CompileButton_Click);
			// 
			// ApplyButton
			// 
			this.ApplyButton.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ApplyButton.Enabled = false;
			this.ApplyButton.Location = new System.Drawing.Point(0, 68);
			this.ApplyButton.Name = "ApplyButton";
			this.ApplyButton.Size = new System.Drawing.Size(115, 23);
			this.ApplyButton.TabIndex = 3;
			this.ApplyButton.Text = "Apply";
			this.ApplyButton.UseVisualStyleBackColor = true;
			this.ApplyButton.Click += new System.EventHandler(this.ApplyButton_Click);
			// 
			// LoadCodeDialog
			// 
			this.LoadCodeDialog.DefaultExt = "cs";
			this.LoadCodeDialog.Filter = "Code file|*.cs|All files|*.*";
			this.LoadCodeDialog.Title = "Load Code";
			this.LoadCodeDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.LoadCodeDialog_FileOk);
			// 
			// SaveCodeDialog
			// 
			this.SaveCodeDialog.DefaultExt = "cs";
			this.SaveCodeDialog.Filter = "Code file|*.cs|All files|*.*";
			this.SaveCodeDialog.Title = "Save Code";
			this.SaveCodeDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.SaveCodeDialog_FileOk);
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
		private System.Windows.Forms.Button ApplyButton;
		private System.Windows.Forms.OpenFileDialog LoadCodeDialog;
		private System.Windows.Forms.SaveFileDialog SaveCodeDialog;
	}
}
