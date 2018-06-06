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

            //for debugging
            string path = OpenOCDDirectory + BatFileName;
            ExecuteFile(path);
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

        private bool CreateBatFile(string pathToFile)
        {
            try
            {
                using (StreamWriter sw = File.CreateText(pathToFile))
                {
                    sw.WriteLine("@Echo off");
                    sw.Write(OpenOCDExecName);
                    sw.Write(" ");
                    sw.Write(_currentDevice.KeyFile);
                    sw.Write(" ");
                    foreach (string item in _currentDevice.CommandQueue)
                    {
                        sw.Write(item);
                        sw.Write(" ");
                    }
                    sw.WriteLine("");
                    sw.Write("pause");

                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        private void ExecuteFile(string pathToFile)
        {

            //string output = "";
            //string errors = "";
            //Process executeProcess = new Process();

            ////myProcess.StartInfo.FileName = "cmd.exe";
            ////myProcess.StartInfo.Arguments = @"/C cd " + Application.StartupPath + "/bin & decompile.bat";
            ////myProcess.Start();
            ////myProcess.StartInfo.Arguments = @"/C cd " + Application.StartupPath + "/bin & decompile.bat ПАРАМЕТР";

            //executeProcess.StartInfo.FileName = "cmd.exe";
            //executeProcess.StartInfo.Arguments = @"/C cd " + pathToFile;
            //executeProcess.StartInfo.StandardOutputEncoding = Encoding.UTF8;
            //executeProcess.StartInfo.StandardErrorEncoding = Encoding.UTF8;
            //executeProcess.StartInfo.CreateNoWindow = true;
            //executeProcess.StartInfo.Verb = "runas";
            //executeProcess.StartInfo.UseShellExecute = false;
            ////executeProcess.StartInfo.RedirectStandardInput = true;
            //executeProcess.StartInfo.RedirectStandardOutput = true;
            //executeProcess.StartInfo.RedirectStandardError = true;

            //executeProcess.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
            //{
            //    if (!String.IsNullOrEmpty(e.Data))
            //    {
            //        _outputRichTextBox.AppendText("Output: \n");
            //        _outputRichTextBox.AppendText(e.Data);
            //    }
            //});

            //executeProcess.ErrorDataReceived += new DataReceivedEventHandler((sender, e) =>
            //{
            //    if (!String.IsNullOrEmpty(e.Data))
            //    {
            //        _outputRichTextBox.AppendText("error: \n");
            //        _outputRichTextBox.AppendText(e.Data);
            //    }
            //});


            //executeProcess.Start();
            ////executeProcess.BeginOutputReadLine();
            ////output = executeProcess.StandardOutput.ReadToEnd();
            ////executeProcess.BeginOutputReadLine();
            //executeProcess.BeginErrorReadLine();
            //executeProcess.BeginOutputReadLine();
            ////output = executeProcess.StandardOutput.ReadToEnd(); //Encoding.UTF8.GetBytes(executeProcess.StandardOutput.ReadToEnd());
            ////errors = executeProcess.StandardError.ReadToEnd(); //Encoding.UTF8.GetBytes(executeProcess.StandardError.ReadToEnd());

            ////executeProcess.Close();
            ////executeProcess.Dispose();
            ////_outputRichTextBox.AppendText("Output: \n" + output);
            ////_outputRichTextBox.AppendText(Environment.NewLine);
            ////_outputRichTextBox.AppendText("Errors: \n" + errors);
            //executeProcess.WaitForExit();


            using (Process executeProcess = new Process())
            {
                StringBuilder log = new StringBuilder();


                executeProcess.StartInfo.FileName = "cmd.exe";
                executeProcess.StartInfo.Arguments = @"/C cd " + pathToFile;
                executeProcess.StartInfo.CreateNoWindow = true;
                executeProcess.StartInfo.Verb = "runas";
                executeProcess.StartInfo.UseShellExecute = false;
                executeProcess.StartInfo.RedirectStandardOutput = true;
                executeProcess.StartInfo.RedirectStandardError = true;


                using (AutoResetEvent outputWaitHandle = new AutoResetEvent(false))
                using (AutoResetEvent errorWaitHandle = new AutoResetEvent(false))
                {
                    executeProcess.OutputDataReceived += (sender, e) => {
                        if (e.Data == null)
                        {
                            outputWaitHandle.Set();
                        }
                        else
                        {
                            log.Append(e.Data);
                        }
                    };

                    executeProcess.ErrorDataReceived += (sender, e) => {
                        if (e.Data == null)
                        {
                            errorWaitHandle.Set();
                        }
                        else
                        {
                            log.Append(e.Data);
                        }
                    };

                    executeProcess.Start();

                    ////output = executeProcess.StandardOutput.ReadToEnd(); //Encoding.UTF8.GetBytes(executeProcess.StandardOutput.ReadToEnd());
                    ////errors = executeProcess.StandardError.ReadToEnd(); //Encoding.UTF8.GetBytes(executeProcess.StandardError.ReadToEnd());

                    executeProcess.BeginErrorReadLine();
                    executeProcess.BeginOutputReadLine();

                    executeProcess.WaitForExit(5);
                    //return log.ToString();
                }
            }




        }

        private void OnBurnFirmwareCommand()
        {
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
                if (CreateBatFile(path))
                {
                    ExecuteFile(path);



                    //executeProcess.OutputDataReceived +=

                    //proc.Start();




                    //Process myProcess = new Process();
                    //                    myProcess.StartInfo.FileName = "cmd.exe";
                    //                    myProcess.StartInfo.Arguments = @"/C cd " + Application.StartupPath + "/server/login & start.bat";
                    //                    myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    //                    myProcess.StartInfo.CreateNoWindow = true;
                    //                    myProcess.Start();


                    ////_outputRichTextBox.AppendText("");


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
