using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STMProg.DevicesSpecifications
{
    class DeviceSpecification : IDeviceSpecification
    {
        #region Private members
        private string _firmwareFile;
        private string _procType;
        #endregion

        #region Properties
        public string FirmwareFile
        {
            get { return _firmwareFile; }
        }
        public string ProcType
        {
            get { return _procType; }
        }
        #endregion

        #region Ctor
        public DeviceSpecification(string _inputFirmwareFile, string _inputProcType)
        {
            _firmwareFile = _inputFirmwareFile;
            _procType = _inputProcType;
        }
        #endregion
    }
}
