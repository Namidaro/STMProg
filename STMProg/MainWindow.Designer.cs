namespace STMProg
{
    partial class MainWindow
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this._openFirmwareFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this._procTypeLabel = new System.Windows.Forms.Label();
            this._burnDeviceButton = new System.Windows.Forms.Button();
            this._firmwareFileNameLabel = new System.Windows.Forms.Label();
            this._procTypeComboBox = new System.Windows.Forms.ComboBox();
            this._openFirmwareFileButton = new System.Windows.Forms.Button();
            this._outputRichTextBox = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _openFirmwareFileDialog
            // 
            this._openFirmwareFileDialog.FileName = "_openFirmwareFileDialog";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this._procTypeLabel);
            this.panel1.Controls.Add(this._burnDeviceButton);
            this.panel1.Controls.Add(this._firmwareFileNameLabel);
            this.panel1.Controls.Add(this._procTypeComboBox);
            this.panel1.Controls.Add(this._openFirmwareFileButton);
            this.panel1.Controls.Add(this._outputRichTextBox);
            this.panel1.Location = new System.Drawing.Point(23, 73);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(760, 337);
            this.panel1.TabIndex = 0;
            // 
            // _procTypeLabel
            // 
            this._procTypeLabel.AutoSize = true;
            this._procTypeLabel.Location = new System.Drawing.Point(11, 8);
            this._procTypeLabel.Name = "_procTypeLabel";
            this._procTypeLabel.Size = new System.Drawing.Size(92, 13);
            this._procTypeLabel.TabIndex = 5;
            this._procTypeLabel.Text = "Тип процессора:";
            // 
            // _burnDeviceButton
            // 
            this._burnDeviceButton.Location = new System.Drawing.Point(678, 3);
            this._burnDeviceButton.Name = "_burnDeviceButton";
            this._burnDeviceButton.Size = new System.Drawing.Size(75, 23);
            this._burnDeviceButton.TabIndex = 4;
            this._burnDeviceButton.Text = "Прошить";
            this._burnDeviceButton.UseVisualStyleBackColor = true;
            this._burnDeviceButton.Click += new System.EventHandler(this._burnDeviceButton_Click);
            // 
            // _firmwareFileNameLabel
            // 
            this._firmwareFileNameLabel.AutoSize = true;
            this._firmwareFileNameLabel.Location = new System.Drawing.Point(420, 7);
            this._firmwareFileNameLabel.Name = "_firmwareFileNameLabel";
            this._firmwareFileNameLabel.Size = new System.Drawing.Size(119, 13);
            this._firmwareFileNameLabel.TabIndex = 3;
            this._firmwareFileNameLabel.Text = "FirmwareFileNameLabel";
            // 
            // _procTypeComboBox
            // 
            this._procTypeComboBox.FormattingEnabled = true;
            this._procTypeComboBox.Location = new System.Drawing.Point(106, 3);
            this._procTypeComboBox.Name = "_procTypeComboBox";
            this._procTypeComboBox.Size = new System.Drawing.Size(121, 21);
            this._procTypeComboBox.TabIndex = 2;
            this._procTypeComboBox.SelectedIndexChanged += new System.EventHandler(this._procTypeComboBox_SelectedIndexChanged);
            // 
            // _openFirmwareFileButton
            // 
            this._openFirmwareFileButton.Location = new System.Drawing.Point(233, 3);
            this._openFirmwareFileButton.Name = "_openFirmwareFileButton";
            this._openFirmwareFileButton.Size = new System.Drawing.Size(178, 21);
            this._openFirmwareFileButton.TabIndex = 1;
            this._openFirmwareFileButton.Text = "Выбрать файл прошивки";
            this._openFirmwareFileButton.UseVisualStyleBackColor = true;
            this._openFirmwareFileButton.Click += new System.EventHandler(this._openFirmwareFileButton_Click);
            // 
            // _outputRichTextBox
            // 
            this._outputRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._outputRichTextBox.DetectUrls = false;
            this._outputRichTextBox.Location = new System.Drawing.Point(3, 30);
            this._outputRichTextBox.Name = "_outputRichTextBox";
            this._outputRichTextBox.ReadOnly = true;
            this._outputRichTextBox.Size = new System.Drawing.Size(750, 300);
            this._outputRichTextBox.TabIndex = 0;
            this._outputRichTextBox.TabStop = false;
            this._outputRichTextBox.Text = "";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 433);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Resizable = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "STMProg";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog _openFirmwareFileDialog;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label _procTypeLabel;
        private System.Windows.Forms.Button _burnDeviceButton;
        private System.Windows.Forms.Label _firmwareFileNameLabel;
        private System.Windows.Forms.ComboBox _procTypeComboBox;
        private System.Windows.Forms.Button _openFirmwareFileButton;
        private System.Windows.Forms.RichTextBox _outputRichTextBox;
    }
}

