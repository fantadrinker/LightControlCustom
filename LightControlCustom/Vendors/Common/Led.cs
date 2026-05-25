using System;
using System.Collections.Generic;
using System.Text;

namespace LightControlCustom.Vendors.Common
{
    // represents an led on a device
    public class Led
    {
        public String? DeviceName { get; private set; }

        public String LedName { get; private set; }

        public String[] LedStyles { get; private set; }

        public Led(String deviceName, String ledName, String[] ledStyles)
        {
            this.DeviceName = deviceName;
            this.LedName = ledName;
            this.LedStyles = ledStyles;
        }
    }
}
