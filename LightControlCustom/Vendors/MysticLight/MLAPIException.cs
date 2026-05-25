using System;
using System.Collections.Generic;
using System.Text;

namespace LightControlCustom.Vendors.MysticLight
{
    internal class MLAPIException: Exception
    {
        private MysticLightProxy.MLAPI_Status ret;

        public MLAPIException(): base() { }

        public MLAPIException(MysticLightProxy.MLAPI_Status ret): base()
        {
            this.ret = ret;
        }

        public MysticLightProxy.MLAPI_Status getStatus()
        {
            return ret;
        }
    }
}
