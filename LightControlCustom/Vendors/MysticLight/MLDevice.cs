using LightControlCustom.Vendors.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace LightControlCustom.Vendors.MysticLight
{
    internal class MLDevice : Device
    {
        public String name { get; private set; }

        public int idx { get; private set; }

        public MLDevice(String name, int index): base(name, String.Format("{0}_{1}", name, index))
        {
            this.name = name;
            this.idx = index;
        }
    }
}
