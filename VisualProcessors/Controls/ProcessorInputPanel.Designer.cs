namespace VisualProcessors.Controls
{
	partial class ProcessorInputPanel
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
			this.InputSplitContainer = new System.Windows.Forms.SplitContainer();
			this.InputChannelList = new System.Windows.Forms.ListBox();
			this.OptionalCheckBox = new System.Windows.Forms.CheckBox();
			this.ConstantInput = new System.Windows.Forms.TextBox();
			this.ConstantRadioButton = new System.Windows.Forms.RadioButton();
			this.LinkRadioButton = new System.Windows.Forms.RadioButton();
			this.ConfigurationGroupBox = new System.Windows.Forms.GroupBox();
			this.ConfigurationLabel = new System.Windows.Forms.Label();
			this.UnlinkButton = new System.Windows.Forms.Button();
			this.LinkButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.InputSplitContainer)).BeginInit();
			this.InputSplitContainer.Panel1.SuspendLayout();
			this.InputSplitContainer.Panel2.SuspendLayout();
			this.InputSplitContainer.SuspendLayout();
			this.ConfigurationGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// InputSplitContainer
			// 
			this.InputSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.InputSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.InputSplitContainer.Location = new System.Drawing.Point(0, 0);
			this.InputSplitContainer.Name = "InputSplitContainer";
			// 
			// InputSplitContainer.Panel1
			// 
			this.InputSplitContainer.Panel1.Controls.Add(this.InputChannelList);
			// 
			// InputSplitContainer.Panel2
			// 
			this.InputSplitContainer.Panel2.Controls.Add(this.OptionalCheckBox);
			this.InputSplitContainer.Panel2.Controls.Add(this.ConstantInput);
			this.InputSplitContainer.Panel2.Controls.Add(this.ConstantRadioButton);
			this.InputSplitContainer.Panel2.Controls.Add(this.LinkRadioButton);
			this.InputSplitContainer.Panel2.Controls.Add(this.ConfigurationGroupBox);
			this.InputSplitContainer.Panel2.Controls.Add(this.UnlinkButton);
			this.InputSplitContainer.Panel2.Controls.Add(this.LinkButton);
			this.InputSplitContainer.Panel2MinSize = 200;
			this.InputSplitContainer.Size = new System.Drawing.Size(350, 199);
			this.InputSplitContainer.SplitterDistance = 130;
			this.InputSplitContainer.TabIndex = 2;
			// 
			// InputChannelList
			// 
			this.InputChannelList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.InputChannelList.FormattingEnabled = true;
			this.InputChannelList.IntegralHeight = false;
			this.InputChannelList.Location = new System.Drawing.Point(0, 0);
			this.InputChannelList.Name = "InputChannelList";
			this.InputChannelList.Size = new System.Drawing.Size(130, 199);
			this.InputChannelList.TabIndex = 0;
			this.InputChannelList.SelectedIndexChanged += new System.EventHandler(this.InputChannelList_SelectedIndexChanged);
			// 
			// OptionalCheckBox
			// 
			this.OptionalCheckBox.AutoSize = true;
			this.OptionalCheckBox.Location = new System.Drawing.Point(3, 97);
			this.OptionalCheckBox.Name = "OptionalCheckBox";
			this.OptionalCheckBox.Size = new System.Drawing.Size(65, 17);
			this.OptionalCheckBox.TabIndex = 18;
			this.OptionalCheckBox.Text = "Optional";
			this.OptionalCheckBox.UseVisualStyleBackColor = true;
			this.OptionalCheckBox.CheckedChanged += new System.EventHandler(this.OptionalCheckBox_CheckedChanged);
			// 
			// ConstantInput
			// 
			this.ConstantInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ConstantInput.Enabled = false;
			this.ConstantInput.Location = new System.Drawing.Point(108, 71);
			this.ConstantInput.Name = "ConstantInput";
			this.ConstantInput.Size = new System.Drawing.Size(105, 20);
			this.ConstantInput.TabIndex = 16;
			this.ConstantInput.Text = "0";
			this.ConstantInput.TextChanged += new System.EventHandler(this.ConstantInput_TextChanged);
			// 
			// ConstantRadioButton
			// 
			this.ConstantRadioButton.AutoSize = true;
			this.ConstantRadioButton.Enabled = false;
			this.ConstantRadioButton.Location = new System.Drawing.Point(3, 72);
			this.ConstantRadioButton.Name = "ConstantRadioButton";
			this.ConstantRadioButton.Size = new System.Drawing.Size(67, 17);
			this.ConstantRadioButton.TabIndex = 15;
			this.ConstantRadioButton.Text = "Constant";
			this.ConstantRadioButton.UseVisualStyleBackColor = true;
			this.ConstantRadioButton.CheckedChanged += new System.EventHandler(this.RadioButtonCheckedChanged);
			// 
			// LinkRadioButton
			// 
			this.LinkRadioButton.AutoSize = true;
			this.LinkRadioButton.Checked = true;
			this.LinkRadioButton.Enabled = false;
			this.LinkRadioButton.Location = new System.Drawing.Point(3, 45);
			this.LinkRadioButton.Name = "LinkRadioButton";
			this.LinkRadioButton.Size = new System.Drawing.Size(45, 17);
			this.LinkRadioButton.TabIndex = 14;
			this.LinkRadioButton.TabStop = true;
			this.LinkRadioButton.Text = "Link";
			this.LinkRadioButton.UseVisualStyleBackColor = true;
			this.LinkRadioButton.CheckedChanged += new System.EventHandler(this.RadioButtonCheckedChanged);
			// 
			// ConfigurationGroupBox
			// 
			this.ConfigurationGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ConfigurationGroupBox.Controls.Add(this.ConfigurationLabel);
			this.ConfigurationGroupBox.Location = new System.Drawing.Point(3, 3);
			this.ConfigurationGroupBox.Name = "ConfigurationGroupBox";
			this.ConfigurationGroupBox.Size = new System.Drawing.Size(210, 36);
			this.ConfigurationGroupBox.TabIndex = 13;
			this.ConfigurationGroupBox.TabStop = false;
			this.ConfigurationGroupBox.Text = "Configuration";
			// 
			// ConfigurationLabel
			// 
			this.ConfigurationLabel.AutoSize = true;
			this.ConfigurationLabel.Location = new System.Drawing.Point(6, 16);
			this.ConfigurationLabel.Name = "ConfigurationLabel";
			this.ConfigurationLabel.Size = new System.Drawing.Size(55, 13);
			this.ConfigurationLabel.TabIndex = 0;
			this.ConfigurationLabel.Text = "Not linked";
			// 
			// UnlinkButton
			// 
			this.UnlinkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.UnlinkButton.Enabled = false;
			this.UnlinkButton.Location = new System.Drawing.Point(153, 42);
			this.UnlinkButton.Name = "UnlinkButton";
			this.UnlinkButton.Size = new System.Drawing.Size(60, 23);
			this.UnlinkButton.TabIndex = 17;
			this.UnlinkButton.Text = "Unlink";
			this.UnlinkButton.UseVisualStyleBackColor = true;
			this.UnlinkButton.Click += new System.EventHandler(this.UnlinkButton_Click);
			// 
			// LinkButton
			// 
			this.LinkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.LinkButton.Enabled = false;
			this.LinkButton.Location = new System.Drawing.Point(87, 42);
			this.LinkButton.Name = "LinkButton";
			this.LinkButton.Size = new System.Drawing.Size(60, 23);
			this.LinkButton.TabIndex = 12;
			this.LinkButton.Text = "Link";
			this.LinkButton.UseVisualStyleBackColor = true;
			this.LinkButton.Click += new System.EventHandler(this.LinkButton_Click);
			// 
			// ProcessorInputPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.InputSplitContainer);
			this.Name = "ProcessorInputPanel";
			this.Size = new System.Drawing.Size(350, 199);
			this.InputSplitContainer.Panel1.ResumeLayout(false);
			this.InputSplitContainer.Panel2.ResumeLayout(false);
			this.InputSplitContainer.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.InputSplitContainer)).EndInit();
			this.InputSplitContainer.ResumeLayout(false);
			this.ConfigurationGroupBox.ResumeLayout(false);
			this.ConfigurationGroupBox.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer InputSplitContainer;
		private System.Windows.Forms.ListBox InputChannelList;
		private System.Windows.Forms.TextBox ConstantInput;
		private System.Windows.Forms.RadioButton ConstantRadioButton;
		private System.Windows.Forms.RadioButton LinkRadioButton;
		private System.Windows.Forms.GroupBox ConfigurationGroupBox;
		private System.Windows.Forms.Label ConfigurationLabel;
		private System.Windows.Forms.Button UnlinkButton;
		private System.Windows.Forms.Button LinkButton;
		private System.Windows.Forms.CheckBox OptionalCheckBox;
	}
}
