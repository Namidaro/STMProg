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
        private static readonly string _batFileName = "Execute.bat";
        private static string _openOCDExecName;
        private IDeviceSpecification _currentDevice;
        public StringBuilder log = new StringBuilder();
        WorkActions workActions = new WorkActions();

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

        public string BatFileName
        {
            get
            {
                return _batFileName;
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
        #endregion


        public MainWindow()
        {
            InitializeComponent();
            _outputRichTextBox.Clear();
            _firmwareFileName.ResetText();
            

            //проверка системы на разрядность, в зависимости от нее выбирается исполняемый файл
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
            _openFirmwareFileDialog.Filter = "config files (*.elf)|*.elf|All files (*.*)|*.*";
        }

        private void OnOpenFirmwareFileCommand()
        {
            if (_openFirmwareFileDialog.ShowDialog() == DialogResult.OK)
            {
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
            _outputRichTextBox.AppendText(workActions.log.ToString());
            OnOpenFirmwareFileCommand();
        }

        private void _burnFirmwareButton_Click(object sender, EventArgs e)
        {
            OnBurnFirmwareCommand();

        }

        private void OnBurnFirmwareCommand()
        {
            
            workActions.OpenOCDDirectory = OpenOCDDirectory;
            workActions.OpenOCDExecName = OpenOCDExecName;
            //надо проводить инициализацию устройства, спросить какие есть зависимости и т.п. для создания спецификаций
            _currentDevice = new MR902DeviceSpecification();
            string path = OpenOCDDirectory + BatFileName;
            //удаляется старый фалй, если есть( в принципе не нужно, но на случай непредвиденных ситуаций)
            try
            {
                if (File.Exists(path)) File.Delete(path);

            }
            catch (Exception CannotDeleteFileException)
            {
                MessageBox.Show(CannotDeleteFileException.Message.ToString());
            }
            //пробуем создать bat-файл
            try
            {
                if (workActions.CreateBatFile(path,_currentDevice))
                {
                    try
                    {
                        workActions.ExecuteFile("cmd.exe", @"cd " + OpenOCDDirectory, _currentDevice);
                    }
                    catch (Exception ThreadException)
                    {
                        MessageBox.Show(ThreadException.Message.ToString());
                    }
                }
            }
            catch (Exception CannotCreateFileException)
            {
                MessageBox.Show(CannotCreateFileException.Message.ToString());
            }
            //удаляется bat-файл после отработки
            try
            {
                File.Delete(path);
            }
            catch
            {

            }

        }


    }
}
