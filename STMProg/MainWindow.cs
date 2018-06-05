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

namespace STMProg
{
    public partial class MainWindow : MetroForm
    {
        private List<string> _deviceCollection;
        private static bool is64Bit = System.Environment.Is64BitOperatingSystem;

        public MainWindow()
        {
            InitializeComponent();
            _outputRichTextBox.Clear();
            _firmwareFileName.ResetText();
            InitializeDeviceCollection();
            //_deviceList.Items = DevicesToOperate;
        }

        private void InitializeDeviceCollection()
        {
            
        }

        private void OnOpenFirmwareFileCommand()
        {
            if (_openFirmwareFileDialog.ShowDialog() == DialogResult.OK)
            {
                var devInfo = File.ReadAllText(_openFirmwareFileDialog.FileName);
                try
                {
                    _firmwareFileName.Text = "Файл прошивки: " + _openFirmwareFileDialog.FileName;
                    
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
            }
        }

        private void _openFirmwareFile_Click(object sender, EventArgs e)
        {
            OnOpenFirmwareFileCommand();
        }
    }
}
