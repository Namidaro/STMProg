﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STMProg.DevicesSpecifications
{
    interface IDeviceSpecification
    {
        string DeviceName { get; }
        string KeyFile { get; }
        List<string> CommandQueue { get; }
    }
}