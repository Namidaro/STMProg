using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using System.IO;
using System.Diagnostics;


namespace STMProg
{
    public partial class MainWindow : MetroForm
    {
        #region Private members
        private static readonly bool _is64Bit = System.Environment.Is64BitOperatingSystem;
        private static readonly string _initialDirectory = Directory.GetCurrentDirectory();
        private static string _openOCDDirectory;
        private static readonly string _batFileName = "Execute.bat";
        #endregion

        #region Properties
        public bool Is64Bit
        {
            get
            {
                return _is64Bit;
            }
        }

        public string InitialDirectory
        {
            get
            {
                return _initialDirectory;
            }
        }

        public string OpenOCDDirectory
        { get
            {
                return _openOCDDirectory;
            }
            set
            {
                _openOCDDirectory = value;
            }
        }

        public string BatFileName
        {
            get
            {
                return _batFileName;
            }
        }
        #endregion


        public MainWindow()
        {
            InitializeComponent();
            _outputRichTextBox.Clear();
            _firmwareFileName.ResetText();
            if (Is64Bit)
            {
                _openFirmwareFileDialog.InitialDirectory = InitialDirectory + @"\OpenOCD\bin-x64\";
                OpenOCDDirectory = _openFirmwareFileDialog.InitialDirectory;
            }
            else
            {
                _openFirmwareFileDialog.InitialDirectory = InitialDirectory + @"\OpenOCD\bin\";
                OpenOCDDirectory = _openFirmwareFileDialog.InitialDirectory;
            }
            _openFirmwareFileDialog.FileName = null;
            _openFirmwareFileDialog.Filter = "config files (*.elf)|*.elf|All files (*.*)|*.*";

        }


        private void OnOpenFirmwareFileCommand()
        {
            _outputRichTextBox.AppendText(_openFirmwareFileDialog.InitialDirectory);
            if (_openFirmwareFileDialog.ShowDialog() == DialogResult.OK)
            {
                var devInfo = File.ReadAllText(_openFirmwareFileDialog.FileName);
                try
                {
                    _firmwareFileName.Text = "Файл прошивки: " + _openFirmwareFileDialog.SafeFileName;

                }
                catch (Exception exception)
                {
                    _outputRichTextBox.AppendText(exception.ToString());
                }
            }
        }


        private void _openFirmwareFileButton_Click(object sender, EventArgs e)
        {
            OnOpenFirmwareFileCommand();
        }

        private void _burnFirmwareButton_Click(object sender, EventArgs e)
        {
            OnBurnFirmwareCommand();
        }

        private void OnBurnFirmwareCommand()
        {
            string path = OpenOCDDirectory + BatFileName;
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.Write("");
                }
            }



            Process proc = new Process();
            proc.StartInfo.FileName = "";
            proc.BeginOutputReadLine();
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.Arguments = "-f stm32f417vgt6.cfg";

            proc.Start();
            proc.BeginOutputReadLine();
            proc.BeginErrorReadLine();



            //_outputRichTextBox.AppendText("");
        }
    }
}
