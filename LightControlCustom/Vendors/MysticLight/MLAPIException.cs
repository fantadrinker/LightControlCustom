using System;
using System.Collections.Generic;
using System.Text;

namespace LightControlCustom.Vendors.MysticLight
{
    internal class MLAPIException: Exception
    {
        private MLAPI_Status ret;

        public MLAPIException(): base() { }

        public MLAPIException(MLAPI_Status ret): base()
        {
            this.ret = ret;
        }

        public MLAPI_Status getStatus()
        {
            return ret;
        }
    }
}
