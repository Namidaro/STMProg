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
using STMProg.DevicesSpecifications;
using System.Threading;


namespace STMProg
{
    public partial class MainWindow : MetroForm
    {
        #region Private members
        private static readonly bool _is64Bit = System.Environment.Is64BitOperatingSystem;
        private static readonly string _initialDirectory = Directory.GetCurrentDirectory();
        private static string _openOCDDirectory;
        private string _procType;
        private string _firmwareFile;
        private static string _openOCDExecName;
        private IDeviceSpecification _currentDevice;
        #endregion
        SynchronizationContext _syncContext;
        public ProcessorType processorType = new ProcessorType();
        WorkActions workActions = new WorkActions();
        public byte time;

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
        {
            get
            {
                return _openOCDDirectory;
            }
            set
            {
                _openOCDDirectory = value;
            }
        }

        public string OpenOCDExecName
        {
            get
            {
                return _openOCDExecName;
            }
            set
            {
                _openOCDExecName = value;
            }
        }

        public string ProcType
        {
            get
            {
                return _procType;
            }
            set
            {
                _procType = value;
            }
        }
        public string FirmwareFile
        {
            get
            {
                return _firmwareFile;
            }
            set
            {
                _firmwareFile = value;
            }
        }
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            _outputRichTextBox.Clear();
            _firmwareFileNameLabel.ResetText();
            //проверка системы на разрядность, в зависимости от нее выбирается исполняемый файл
            try
            {
                if (Is64Bit)
                {
                    _openFirmwareFileDialog.InitialDirectory = InitialDirectory + @"\OpenOCD\bin-x64\";
                    OpenOCDDirectory = _openFirmwareFileDialog.InitialDirectory;
                    OpenOCDExecName = "openocd-x64-0.7.0.exe";
                }
                else
                {
                    _openFirmwareFileDialog.InitialDirectory = InitialDirectory + @"\OpenOCD\bin-x86\";
                    OpenOCDDirectory = _openFirmwareFileDialog.InitialDirectory;
                    OpenOCDExecName = "openocd-0.7.0.exe";
                }
                _openFirmwareFileDialog.FileName = null;
                _openFirmwareFileDialog.Filter = "Файлы прошивки (*.elf)|*.elf|Все файлы (*.*)|*.*";

                _procTypeComboBox.Items.AddRange(processorType.ProcessorList.Keys.ToArray());
                _procTypeComboBox.SelectedItem = _procTypeComboBox.Items[0];
                workActions.OnLog += this.WriteLog;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public void WriteLog()
        {
            _syncContext = workActions._syncContext;

            try
            {
                _syncContext.Post(_ => _outputRichTextBox.Clear(), null);
                _syncContext.Post(_ => _outputRichTextBox.AppendText(workActions.log.ToString()), null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void OnOpenFirmwareFileCommand()
        {
            if (_openFirmwareFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _firmwareFileNameLabel.Text = "Файл прошивки: " + _openFirmwareFileDialog.SafeFileName;
                    FirmwareFile = _openFirmwareFileDialog.SafeFileName;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message.ToString());
                }
            }
            else
            {
                try
                {
                    _firmwareFileNameLabel.Text = "Файл прошивки: не выбран";
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message.ToString());
                }
            }
        }

        private void _openFirmwareFileButton_Click(object sender, EventArgs e)
        {
            //_outputRichTextBox.AppendText(workActions.log.ToString());
            OnOpenFirmwareFileCommand();
        }

        private void OnBurnFirmwareCommand()
        {
            try
            {
                _burnDeviceButton.Enabled = false;
                _outputRichTextBox.Clear();
                workActions.OpenOCDDirectory = OpenOCDDirectory;
                workActions.OpenOCDExecName = OpenOCDExecName;
                _currentDevice = new DeviceSpecification(FirmwareFile, ProcType);
                try
                {
                    workActions.ExecuteFile("cmd.exe", @"cd " + OpenOCDDirectory, _currentDevice);
                }
                catch (Exception ThreadException)
                {
                    MessageBox.Show(ThreadException.Message.ToString());
                }
                _burnDeviceButton.Enabled = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
                _burnDeviceButton.Enabled = true;
            }

        }

        private void _burnDeviceButton_Click(object sender, EventArgs e)
        {
            time = 0;
            OnBurnFirmwareCommand();
        }

        private void _procTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnProcTypeChanged();
        }

        private void OnProcTypeChanged()
        {
            if (processorType.ProcessorList.ContainsKey(_procTypeComboBox.SelectedItem.ToString()))
            {
                ProcType = processorType.ProcessorList.ElementAt(processorType.ProcessorList.IndexOfKey(_procTypeComboBox.SelectedItem.ToString())).Value;
            }
        }

    }
}
