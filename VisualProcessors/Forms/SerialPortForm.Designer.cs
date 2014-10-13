namespace VisualProcessors.Forms
{
	partial class SerialPortForm
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
			this.AcceptUserButton = new System.Windows.Forms.Button();
			this.CancelUserButton = new System.Windows.Forms.Button();
			this.PortNameComboBox = new System.Windows.Forms.ComboBox();
			this.RefreshButton = new System.Windows.Forms.Button();
			this.BaudRateComboBox = new System.Windows.Forms.ComboBox();
			this.SettingsGroupBox = new System.Windows.Forms.GroupBox();
			this.DataBitsTextBox = new System.Windows.Forms.TextBox();
			this.StopBitsLabel = new System.Windows.Forms.Label();
			this.StopBitsComboBox = new System.Windows.Forms.ComboBox();
			this.ReceivedBytesThresholdLabel = new System.Windows.Forms.Label();
			this.ReceivedBytesThresholdTextBox = new System.Windows.Forms.TextBox();
			this.WriteTimoutLabel = new System.Windows.Forms.Label();
			this.WriteTimeoutTextBox = new System.Windows.Forms.TextBox();
			this.ReadTimeoutLabel = new System.Windows.Forms.Label();
			this.ReadTimeoutTextBox = new System.Windows.Forms.TextBox();
			this.HandshakeLabel = new System.Windows.Forms.Label();
			this.HandshakeComboBox = new System.Windows.Forms.ComboBox();
			this.RtsEnableCheckBox = new System.Windows.Forms.CheckBox();
			this.DtrEnableCheckBox = new System.Windows.Forms.CheckBox();
			this.ParityLabel = new System.Windows.Forms.Label();
			this.ParityComboBox = new System.Windows.Forms.ComboBox();
			this.DataBitsLabel = new System.Windows.Forms.Label();
			this.BaudRateLabel = new System.Windows.Forms.Label();
			this.SettingsGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// AcceptUserButton
			// 
			this.AcceptUserButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.AcceptUserButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.AcceptUserButton.Location = new System.Drawing.Point(236, 278);
			this.AcceptUserButton.Name = "AcceptUserButton";
			this.AcceptUserButton.Size = new System.Drawing.Size(75, 23);
			this.AcceptUserButton.TabIndex = 0;
			this.AcceptUserButton.Text = "Accept";
			this.AcceptUserButton.UseVisualStyleBackColor = true;
			// 
			// CancelUserButton
			// 
			this.CancelUserButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.CancelUserButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelUserButton.Location = new System.Drawing.Point(317, 278);
			this.CancelUserButton.Name = "CancelUserButton";
			this.CancelUserButton.Size = new System.Drawing.Size(75, 23);
			this.CancelUserButton.TabIndex = 1;
			this.CancelUserButton.Text = "Cancel";
			this.CancelUserButton.UseVisualStyleBackColor = true;
			// 
			// PortNameComboBox
			// 
			this.PortNameComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.PortNameComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
			this.PortNameComboBox.FormattingEnabled = true;
			this.PortNameComboBox.IntegralHeight = false;
			this.PortNameComboBox.Location = new System.Drawing.Point(12, 12);
			this.PortNameComboBox.Name = "PortNameComboBox";
			this.PortNameComboBox.Size = new System.Drawing.Size(121, 260);
			this.PortNameComboBox.TabIndex = 2;
			// 
			// RefreshButton
			// 
			this.RefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.RefreshButton.Location = new System.Drawing.Point(12, 278);
			this.RefreshButton.Name = "RefreshButton";
			this.RefreshButton.Size = new System.Drawing.Size(121, 23);
			this.RefreshButton.TabIndex = 3;
			this.RefreshButton.Text = "Refresh";
			this.RefreshButton.UseVisualStyleBackColor = true;
			this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
			// 
			// BaudRateComboBox
			// 
			this.BaudRateComboBox.FormattingEnabled = true;
			this.BaudRateComboBox.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "28800",
            "38400",
            "56000",
            "57600",
            "115200"});
			this.BaudRateComboBox.Location = new System.Drawing.Point(85, 19);
			this.BaudRateComboBox.Name = "BaudRateComboBox";
			this.BaudRateComboBox.Size = new System.Drawing.Size(157, 21);
			this.BaudRateComboBox.TabIndex = 4;
			this.BaudRateComboBox.Text = "9600";
			this.BaudRateComboBox.TextChanged += new System.EventHandler(this.BaudRateComboBox_TextChanged);
			// 
			// SettingsGroupBox
			// 
			this.SettingsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.SettingsGroupBox.Controls.Add(this.DataBitsTextBox);
			this.SettingsGroupBox.Controls.Add(this.StopBitsLabel);
			this.SettingsGroupBox.Controls.Add(this.StopBitsComboBox);
			this.SettingsGroupBox.Controls.Add(this.ReceivedBytesThresholdLabel);
			this.SettingsGroupBox.Controls.Add(this.ReceivedBytesThresholdTextBox);
			this.SettingsGroupBox.Controls.Add(this.WriteTimoutLabel);
			this.SettingsGroupBox.Controls.Add(this.WriteTimeoutTextBox);
			this.SettingsGroupBox.Controls.Add(this.ReadTimeoutLabel);
			this.SettingsGroupBox.Controls.Add(this.ReadTimeoutTextBox);
			this.SettingsGroupBox.Controls.Add(this.HandshakeLabel);
			this.SettingsGroupBox.Controls.Add(this.HandshakeComboBox);
			this.SettingsGroupBox.Controls.Add(this.RtsEnableCheckBox);
			this.SettingsGroupBox.Controls.Add(this.DtrEnableCheckBox);
			this.SettingsGroupBox.Controls.Add(this.ParityLabel);
			this.SettingsGroupBox.Controls.Add(this.ParityComboBox);
			this.SettingsGroupBox.Controls.Add(this.DataBitsLabel);
			this.SettingsGroupBox.Controls.Add(this.BaudRateLabel);
			this.SettingsGroupBox.Controls.Add(this.BaudRateComboBox);
			this.SettingsGroupBox.Location = new System.Drawing.Point(139, 12);
			this.SettingsGroupBox.Name = "SettingsGroupBox";
			this.SettingsGroupBox.Size = new System.Drawing.Size(253, 260);
			this.SettingsGroupBox.TabIndex = 5;
			this.SettingsGroupBox.TabStop = false;
			this.SettingsGroupBox.Text = "Settings";
			// 
			// DataBitsTextBox
			// 
			this.DataBitsTextBox.Location = new System.Drawing.Point(85, 46);
			this.DataBitsTextBox.Name = "DataBitsTextBox";
			this.DataBitsTextBox.Size = new System.Drawing.Size(157, 20);
			this.DataBitsTextBox.TabIndex = 22;
			this.DataBitsTextBox.Text = "8";
			this.DataBitsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.DataBitsTextBox.TextChanged += new System.EventHandler(this.DataBitsTextBox_TextChanged);
			// 
			// StopBitsLabel
			// 
			this.StopBitsLabel.AutoSize = true;
			this.StopBitsLabel.Location = new System.Drawing.Point(6, 130);
			this.StopBitsLabel.Name = "StopBitsLabel";
			this.StopBitsLabel.Size = new System.Drawing.Size(48, 13);
			this.StopBitsLabel.TabIndex = 21;
			this.StopBitsLabel.Text = "Stopbits:";
			// 
			// StopBitsComboBox
			// 
			this.StopBitsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.StopBitsComboBox.FormattingEnabled = true;
			this.StopBitsComboBox.Location = new System.Drawing.Point(85, 127);
			this.StopBitsComboBox.Name = "StopBitsComboBox";
			this.StopBitsComboBox.Size = new System.Drawing.Size(157, 21);
			this.StopBitsComboBox.TabIndex = 20;
			// 
			// ReceivedBytesThresholdLabel
			// 
			this.ReceivedBytesThresholdLabel.AutoSize = true;
			this.ReceivedBytesThresholdLabel.Location = new System.Drawing.Point(6, 209);
			this.ReceivedBytesThresholdLabel.Name = "ReceivedBytesThresholdLabel";
			this.ReceivedBytesThresholdLabel.Size = new System.Drawing.Size(130, 13);
			this.ReceivedBytesThresholdLabel.TabIndex = 19;
			this.ReceivedBytesThresholdLabel.Text = "Received bytes threshold:";
			// 
			// ReceivedBytesThresholdTextBox
			// 
			this.ReceivedBytesThresholdTextBox.Location = new System.Drawing.Point(142, 206);
			this.ReceivedBytesThresholdTextBox.Name = "ReceivedBytesThresholdTextBox";
			this.ReceivedBytesThresholdTextBox.Size = new System.Drawing.Size(100, 20);
			this.ReceivedBytesThresholdTextBox.TabIndex = 18;
			this.ReceivedBytesThresholdTextBox.Text = "1";
			this.ReceivedBytesThresholdTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.ReceivedBytesThresholdTextBox.TextChanged += new System.EventHandler(this.ReceivedBytesThresholdTextBox_TextChanged);
			// 
			// WriteTimoutLabel
			// 
			this.WriteTimoutLabel.AutoSize = true;
			this.WriteTimoutLabel.Location = new System.Drawing.Point(6, 183);
			this.WriteTimoutLabel.Name = "WriteTimoutLabel";
			this.WriteTimoutLabel.Size = new System.Drawing.Size(72, 13);
			this.WriteTimoutLabel.TabIndex = 17;
			this.WriteTimoutLabel.Text = "Write timeout:";
			// 
			// WriteTimeoutTextBox
			// 
			this.WriteTimeoutTextBox.Location = new System.Drawing.Point(142, 180);
			this.WriteTimeoutTextBox.Name = "WriteTimeoutTextBox";
			this.WriteTimeoutTextBox.Size = new System.Drawing.Size(100, 20);
			this.WriteTimeoutTextBox.TabIndex = 16;
			this.WriteTimeoutTextBox.Text = "-1";
			this.WriteTimeoutTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.WriteTimeoutTextBox.TextChanged += new System.EventHandler(this.WriteTimeoutTextBox_TextChanged);
			// 
			// ReadTimeoutLabel
			// 
			this.ReadTimeoutLabel.AutoSize = true;
			this.ReadTimeoutLabel.Location = new System.Drawing.Point(6, 157);
			this.ReadTimeoutLabel.Name = "ReadTimeoutLabel";
			this.ReadTimeoutLabel.Size = new System.Drawing.Size(73, 13);
			this.ReadTimeoutLabel.TabIndex = 15;
			this.ReadTimeoutLabel.Text = "Read timeout:";
			// 
			// ReadTimeoutTextBox
			// 
			this.ReadTimeoutTextBox.Location = new System.Drawing.Point(142, 154);
			this.ReadTimeoutTextBox.Name = "ReadTimeoutTextBox";
			this.ReadTimeoutTextBox.Size = new System.Drawing.Size(100, 20);
			this.ReadTimeoutTextBox.TabIndex = 14;
			this.ReadTimeoutTextBox.Text = "-1";
			this.ReadTimeoutTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.ReadTimeoutTextBox.TextChanged += new System.EventHandler(this.ReadTimeoutTextBox_TextChanged);
			// 
			// HandshakeLabel
			// 
			this.HandshakeLabel.AutoSize = true;
			this.HandshakeLabel.Location = new System.Drawing.Point(6, 103);
			this.HandshakeLabel.Name = "HandshakeLabel";
			this.HandshakeLabel.Size = new System.Drawing.Size(65, 13);
			this.HandshakeLabel.TabIndex = 13;
			this.HandshakeLabel.Text = "Handshake:";
			// 
			// HandshakeComboBox
			// 
			this.HandshakeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.HandshakeComboBox.FormattingEnabled = true;
			this.HandshakeComboBox.Location = new System.Drawing.Point(85, 100);
			this.HandshakeComboBox.Name = "HandshakeComboBox";
			this.HandshakeComboBox.Size = new System.Drawing.Size(157, 21);
			this.HandshakeComboBox.TabIndex = 12;
			// 
			// RtsEnableCheckBox
			// 
			this.RtsEnableCheckBox.AutoSize = true;
			this.RtsEnableCheckBox.Location = new System.Drawing.Point(106, 232);
			this.RtsEnableCheckBox.Name = "RtsEnableCheckBox";
			this.RtsEnableCheckBox.Size = new System.Drawing.Size(90, 17);
			this.RtsEnableCheckBox.TabIndex = 11;
			this.RtsEnableCheckBox.Text = "RTS Enabled";
			this.RtsEnableCheckBox.UseVisualStyleBackColor = true;
			// 
			// DtrEnableCheckBox
			// 
			this.DtrEnableCheckBox.AutoSize = true;
			this.DtrEnableCheckBox.Location = new System.Drawing.Point(9, 232);
			this.DtrEnableCheckBox.Name = "DtrEnableCheckBox";
			this.DtrEnableCheckBox.Size = new System.Drawing.Size(91, 17);
			this.DtrEnableCheckBox.TabIndex = 10;
			this.DtrEnableCheckBox.Text = "DTR Enabled";
			this.DtrEnableCheckBox.UseVisualStyleBackColor = true;
			// 
			// ParityLabel
			// 
			this.ParityLabel.AutoSize = true;
			this.ParityLabel.Location = new System.Drawing.Point(6, 76);
			this.ParityLabel.Name = "ParityLabel";
			this.ParityLabel.Size = new System.Drawing.Size(36, 13);
			this.ParityLabel.TabIndex = 9;
			this.ParityLabel.Text = "Parity:";
			// 
			// ParityComboBox
			// 
			this.ParityComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ParityComboBox.FormattingEnabled = true;
			this.ParityComboBox.Location = new System.Drawing.Point(85, 73);
			this.ParityComboBox.Name = "ParityComboBox";
			this.ParityComboBox.Size = new System.Drawing.Size(157, 21);
			this.ParityComboBox.TabIndex = 8;
			// 
			// DataBitsLabel
			// 
			this.DataBitsLabel.AutoSize = true;
			this.DataBitsLabel.Location = new System.Drawing.Point(6, 49);
			this.DataBitsLabel.Name = "DataBitsLabel";
			this.DataBitsLabel.Size = new System.Drawing.Size(49, 13);
			this.DataBitsLabel.TabIndex = 7;
			this.DataBitsLabel.Text = "Databits:";
			// 
			// BaudRateLabel
			// 
			this.BaudRateLabel.AutoSize = true;
			this.BaudRateLabel.Location = new System.Drawing.Point(6, 22);
			this.BaudRateLabel.Name = "BaudRateLabel";
			this.BaudRateLabel.Size = new System.Drawing.Size(53, 13);
			this.BaudRateLabel.TabIndex = 5;
			this.BaudRateLabel.Text = "Baudrate:";
			// 
			// SerialPortForm
			// 
			this.AcceptButton = this.AcceptUserButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.CancelUserButton;
			this.ClientSize = new System.Drawing.Size(404, 313);
			this.ControlBox = false;
			this.Controls.Add(this.SettingsGroupBox);
			this.Controls.Add(this.RefreshButton);
			this.Controls.Add(this.PortNameComboBox);
			this.Controls.Add(this.CancelUserButton);
			this.Controls.Add(this.AcceptUserButton);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(420, 350);
			this.Name = "SerialPortForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "SerialPortForm";
			this.SettingsGroupBox.ResumeLayout(false);
			this.SettingsGroupBox.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button AcceptUserButton;
		private System.Windows.Forms.Button CancelUserButton;
		private System.Windows.Forms.ComboBox PortNameComboBox;
		private System.Windows.Forms.Button RefreshButton;
		private System.Windows.Forms.ComboBox BaudRateComboBox;
		private System.Windows.Forms.GroupBox SettingsGroupBox;
		private System.Windows.Forms.Label BaudRateLabel;
		private System.Windows.Forms.Label DataBitsLabel;
		private System.Windows.Forms.Label ParityLabel;
		private System.Windows.Forms.ComboBox ParityComboBox;
		private System.Windows.Forms.CheckBox DtrEnableCheckBox;
		private System.Windows.Forms.CheckBox RtsEnableCheckBox;
		private System.Windows.Forms.Label HandshakeLabel;
		private System.Windows.Forms.ComboBox HandshakeComboBox;
		private System.Windows.Forms.Label ReadTimeoutLabel;
		private System.Windows.Forms.TextBox ReadTimeoutTextBox;
		private System.Windows.Forms.Label WriteTimoutLabel;
		private System.Windows.Forms.TextBox WriteTimeoutTextBox;
		private System.Windows.Forms.Label ReceivedBytesThresholdLabel;
		private System.Windows.Forms.TextBox ReceivedBytesThresholdTextBox;
		private System.Windows.Forms.Label StopBitsLabel;
		private System.Windows.Forms.ComboBox StopBitsComboBox;
		private System.Windows.Forms.TextBox DataBitsTextBox;
	}
}