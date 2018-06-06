using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STMProg.DevicesSpecifications
{
    class MR902DeviceSpecification : IDeviceSpecification
    {
        public string DeviceName
        {
            get { return "MR902"; }
        }
        public string KeyFile
        {
            get { return "stm32f417vgt6.cfg"; }
        }
        public List<string> CommandQueue
        {
            get
            {
                return new List<string>
                {
                    "init",
                    "reset halt",
                    "flash write_image erase MR902.elf",
                    "reset run"
                };
            }
        }
    }
}
