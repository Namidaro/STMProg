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
            this._progPanel = new System.Windows.Forms.Panel();
            this._firmwareFileName = new System.Windows.Forms.Label();
            this._deviceList = new System.Windows.Forms.ComboBox();
            this._openFirmwareFile = new System.Windows.Forms.Button();
            this._outputRichTextBox = new System.Windows.Forms.RichTextBox();
            this._progPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _openFirmwareFileDialog
            // 
            this._openFirmwareFileDialog.FileName = "_openFirmwareFileDialog";
            // 
            // _progPanel
            // 
            this._progPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._progPanel.Controls.Add(this._firmwareFileName);
            this._progPanel.Controls.Add(this._deviceList);
            this._progPanel.Controls.Add(this._openFirmwareFile);
            this._progPanel.Controls.Add(this._outputRichTextBox);
            this._progPanel.Location = new System.Drawing.Point(23, 73);
            this._progPanel.Name = "_progPanel";
            this._progPanel.Size = new System.Drawing.Size(760, 337);
            this._progPanel.TabIndex = 0;
            // 
            // _firmwareFileName
            // 
            this._firmwareFileName.AutoSize = true;
            this._firmwareFileName.Location = new System.Drawing.Point(233, 8);
            this._firmwareFileName.Name = "_firmwareFileName";
            this._firmwareFileName.Size = new System.Drawing.Size(93, 13);
            this._firmwareFileName.TabIndex = 3;
            this._firmwareFileName.Text = "FirmwareFileName";
            // 
            // _deviceList
            // 
            this._deviceList.FormattingEnabled = true;
            this._deviceList.Location = new System.Drawing.Point(5, 3);
            this._deviceList.Name = "_deviceList";
            this._deviceList.Size = new System.Drawing.Size(121, 21);
            this._deviceList.TabIndex = 2;
            // 
            // _openFirmwareFile
            // 
            this._openFirmwareFile.Location = new System.Drawing.Point(132, 3);
            this._openFirmwareFile.Name = "_openFirmwareFile";
            this._openFirmwareFile.Size = new System.Drawing.Size(95, 21);
            this._openFirmwareFile.TabIndex = 1;
            this._openFirmwareFile.Text = "Выбрать файл";
            this._openFirmwareFile.UseVisualStyleBackColor = true;
            this._openFirmwareFile.Click += new System.EventHandler(this._openFirmwareFile_Click);
            // 
            // _outputRichTextBox
            // 
            this._outputRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._outputRichTextBox.DetectUrls = false;
            this._outputRichTextBox.Enabled = false;
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
            this.Controls.Add(this._progPanel);
            this.Name = "MainWindow";
            this.Text = "STMProg";
            this._progPanel.ResumeLayout(false);
            this._progPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog _openFirmwareFileDialog;
        private System.Windows.Forms.Panel _progPanel;
        private System.Windows.Forms.RichTextBox _outputRichTextBox;
        private System.Windows.Forms.Button _openFirmwareFile;
        private System.Windows.Forms.ComboBox _deviceList;
        private System.Windows.Forms.Label _firmwareFileName;
    }
}

