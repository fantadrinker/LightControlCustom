using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace LightControlCustom.Vendors.Common
{
    public abstract class Device
    {
        public String DisplayName { get; private set; }

        public String DeviceId { get; private set; }

        public Device(String name, String id)
        {
            DisplayName = name;
            DeviceId = id;
        }
    }
}
