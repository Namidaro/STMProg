using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.IO;
using STMProg.DevicesSpecifications;

namespace STMProg
{
    class WorkActions
    {

        #region Private members
        private static readonly bool _is64Bit = System.Environment.Is64BitOperatingSystem;
        private static readonly string _initialDirectory = Directory.GetCurrentDirectory();
        private static string _openOCDDirectory;
        private static readonly string _batFileName = "Execute.bat";
        private static string _openOCDExecName;
        public StringBuilder log = new StringBuilder();
        private StringBuilder _commandString = new StringBuilder();
        SynchronizationContext _syncContext;

        #endregion

        #region Properties

        public string CommandString
        {
            get
            {
                return _commandString.ToString();
            }
        }
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

        public bool CreateBatFile(string pathToFile, IDeviceSpecification deviceSpecification)
        {
            try
            {
                using (StreamWriter sw = File.CreateText(pathToFile))
                {
                    sw.WriteLine("@Echo off");
                    sw.Write(OpenOCDExecName);
                    sw.Write(" ");
                    sw.Write(deviceSpecification.KeyFile);
                    sw.Write(" ");
                    foreach (string item in deviceSpecification.CommandQueue)
                    {
                        sw.Write(item);
                        sw.Write(" ");
                    }
                    sw.Write("-c shutdown");
                    sw.WriteLine("");
                    sw.Write("pause");
                }

                //StringBuilder sb = new StringBuilder();
                //{
                //    sb.AppendLine("@Echo off");
                //    sb.Append(OpenOCDExecName);
                //    sb.Append(" ");
                //    sb.Append(_currentDevice.KeyFile);
                //    sb.Append(" ");
                //    foreach (string item in _currentDevice.CommandQueue)
                //    {
                //        sb.Append(item);
                //        sb.Append(" ");
                //    }
                //    sb.AppendLine("");
                //    sb.Append("pause");
                //}

            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        private void ToLog(string _toLog)
        {
            _syncContext.Post(_ => log.AppendLine(_toLog) , null); 

        }

        private void CreateCommandString(IDeviceSpecification _currentDevice)
        {
            {
                //_commandString.Append("@Echo off \n");
                _commandString.Append(OpenOCDExecName);
                _commandString.Append(" ");
                _commandString.Append(_currentDevice.KeyFile);
                _commandString.Append(" ");
                foreach (string item in _currentDevice.CommandQueue)
                {
                    _commandString.Append(item);
                    _commandString.Append(" ");
                }
                _commandString.Append("-c shutdown");
                //_commandString.Append("pause");
            }

        }

        public void ExecuteFile(string _fileName, string _pathString, IDeviceSpecification deviceSpecification)
        {

            using (Process executeProcess = new Process())
            {
                _syncContext = SynchronizationContext.Current;
                CreateCommandString(deviceSpecification);
                //executeProcess.StartInfo.FileName = _fileName;
                //executeProcess.StartInfo.Arguments = _args;
                executeProcess.StartInfo.FileName = "cmd.exe";
                executeProcess.StartInfo.Arguments = CommandString;
                executeProcess.StartInfo.CreateNoWindow = true;
                executeProcess.StartInfo.Verb = "runas";
                executeProcess.StartInfo.UseShellExecute = false;
                executeProcess.StartInfo.RedirectStandardOutput = true;
                executeProcess.StartInfo.RedirectStandardError = true;
                executeProcess.StartInfo.RedirectStandardInput = true;
                executeProcess.StartInfo.StandardErrorEncoding = Encoding.GetEncoding(866);
                executeProcess.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(866);



                //using (AutoResetEvent outputWaitHandle = new AutoResetEvent(false))
                //using (AutoResetEvent errorWaitHandle = new AutoResetEvent(false))
                //{
                //    executeProcess.OutputDataReceived += (sender, e) =>
                //    {
                //        if (e.Data == null)
                //        {
                //            outputWaitHandle.Set();
                //        }
                //        else
                //        {
                //            log.Append(e.Data);
                //        }
                //    };

                //    executeProcess.ErrorDataReceived += (sender, e) =>
                //    {
                //        if (e.Data == null)
                //        {
                //            errorWaitHandle.Set();
                //        }
                //        else
                //        {
                //            log.Append(e.Data);
                //        }
                //    };

                executeProcess.OutputDataReceived += (sender, args) => ToLog(args.Data);
                executeProcess.ErrorDataReceived += (sender, args) => ToLog(args.Data);
                executeProcess.Start();
                //executeProcess.StandardInput.WriteLine("@Echo off");
                //executeProcess.BeginOutputReadLine();
                executeProcess.BeginErrorReadLine();
                executeProcess.StandardInput.WriteLine(_pathString);
                executeProcess.StandardInput.WriteLineAsync(CommandString);

                
                executeProcess.WaitForExit(10);
            }
        }


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


    }
}

