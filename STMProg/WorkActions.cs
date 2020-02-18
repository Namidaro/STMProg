using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.IO;
using STMProg.DevicesSpecifications;
using STMProg;


namespace STMProg
{
    class WorkActions
    {
        #region Private members
        private static readonly string _initialDirectory = Directory.GetCurrentDirectory();
        private static string _openOCDDirectory;
        private static string _openOCDExecName;
        public StringBuilder log = new StringBuilder();
        private readonly StringBuilder _commandString = new StringBuilder();
        public SynchronizationContext _syncContext;
        public delegate void Completed();
        #endregion

        #region Properties

        public string CommandString
        {
            get
            {
                return _commandString.ToString();
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
        #endregion

        #region Ctor
        public WorkActions()
        {
        }
        #endregion

        private void ToLog(string _toLog)
        {
            if (_toLog != null)
            {
                if (_toLog.Contains("shutdown command invoked"))
                {
                    _syncContext.Post(_ => log.AppendLine(_toLog + Environment.NewLine + "Прошивка завершена"), null);
                }
                else
                if (!_toLog.Contains("Error: couldn't bind to socket: No error")&&!_toLog.Contains("Попытка записи"))
                {
                    _syncContext.Post(_ => log.AppendLine(_toLog), null);
                }
                OnLog();
            }
        }

        private void CreateCommandString(IDeviceSpecification _currentDevice)
        {
            _commandString.Clear();
            _commandString.Append(OpenOCDExecName);
            _commandString.Append(" -f ");
            _commandString.Append(_currentDevice.ProcType);
            _commandString.Append(@" -c init -c ""reset halt"" -c ""flash write_image erase " + _currentDevice.FirmwareFile);
            _commandString.Append(@""" -c ""reset run"" -c shutdown");
            log.AppendLine("Сформирована командная строка: " + CommandString);
            log.AppendLine("");
            log.AppendLine("Выполнение команды: ");
        }

        public void ExecuteFile(string _fileName, string _pathString, IDeviceSpecification deviceSpecification)
        {
            using (Process executeProcess = new Process())
            {
                log.Clear();
                _syncContext = SynchronizationContext.Current;
                CreateCommandString(deviceSpecification);
                executeProcess.StartInfo.FileName = _fileName;
                executeProcess.StartInfo.Arguments = CommandString;
                executeProcess.StartInfo.CreateNoWindow = true;
                //executeProcess.StartInfo.Verb = "runas";
                executeProcess.StartInfo.UseShellExecute = false;
                executeProcess.StartInfo.RedirectStandardOutput = true;
                executeProcess.StartInfo.RedirectStandardError = true;
                executeProcess.StartInfo.RedirectStandardInput = true;
                executeProcess.StartInfo.StandardErrorEncoding = Encoding.GetEncoding(866);
                executeProcess.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(866);
                executeProcess.OutputDataReceived += (sender, args) => ToLog(args.Data);
                executeProcess.ErrorDataReceived += (sender, args) => ToLog(args.Data);

                executeProcess.Start();
                //executeProcess.BeginOutputReadLine();
                executeProcess.BeginErrorReadLine();
                executeProcess.StandardInput.WriteLine(_pathString);
                executeProcess.StandardInput.WriteLine(CommandString);
                //executeProcess.StandardInput.Flush();
                executeProcess.StandardInput.Close();
                executeProcess.StandardInput.Dispose();
                executeProcess.WaitForExit(0);
            }
        }


        public event Completed OnLog;
    }
}

