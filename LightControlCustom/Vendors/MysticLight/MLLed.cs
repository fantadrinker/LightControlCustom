using LightControlCustom.Vendors.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace LightControlCustom.Vendors.MysticLight
{
    // represents an led on a device
    internal class MLLed: Led
    {
        public MLLed(String deviceName, String ledName, String[] ledStyles): base(deviceName, ledName, ledStyles) { }

    }
}
