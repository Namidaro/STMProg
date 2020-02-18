using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STMProg.DevicesSpecifications
{
    class DeviceSpecification : IDeviceSpecification
    {
        
        #region Properties
        public string FirmwareFile { get; set; }

        public string ProcType { get; set; }

        #endregion

        #region Ctor
        public DeviceSpecification(string inputFirmwareFile, string inputProcType)
        {
            FirmwareFile = inputFirmwareFile;
            ProcType = inputProcType;
        }
        #endregion
    }
}
