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
        private static readonly bool is64Bit = System.Environment.Is64BitOperatingSystem;
        private static readonly string _initialDirectory = Directory.GetCurrentDirectory();

        public MainWindow()
        {
            InitializeComponent();
            _outputRichTextBox.Clear();
            _firmwareFileName.ResetText();
            if (is64Bit)
            {
                _openFirmwareFileDialog.InitialDirectory = _initialDirectory + "\\OpenOCD\\binx64\\";
            }
            else
            {
                _openFirmwareFileDialog.InitialDirectory = _initialDirectory + "\\OpenOCD\\bin\\";
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
                    Console.WriteLine(exception);
                    throw;
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
