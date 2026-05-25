using LightControlCustom.Vendors.Common;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Xml.Linq;
using static LightControlCustom.MainWindow;

namespace LightControlCustom.Vendors.MysticLight
{
    internal class MysticLightProxy
    {
        public enum MLAPI_Status : int
        {
            MLAPI_OK = 0,                   //!< Request is completed.
            MLAPI_ERROR = -1,               //!< Generic error.
            MLAPI_TIMEOUT = -2,             //!< Function is timeout.
            MLAPI_NO_IMPLEMENTED = -3,      //!< MSI Application not found or installed version not supported.
            MLAPI_NOT_INITIALIZED = -4,     //!< MLAPI_Initialize has not been called successful.	
            MLAPI_INVALID_ARGUMENT = -101,  //!< The parameter value is not valid.
            MLAPI_DEVICE_NOT_FOUND = -102,  //!< The device is not found.
            MLAPI_NOT_SUPPORTED = -103      //!< Requested feature is not supported in the selected LED.
        }

        [DllImport("MysticLight_SDK.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl, EntryPoint = "MLAPI_Initialize")]
        extern static MLAPI_Status MLAPI_Initialize();

        [DllImport("MysticLight_SDK.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl, EntryPoint = "MLAPI_Release")]
        extern static MLAPI_Status MLAPI_Release();

        [DllImport("MysticLight_SDK.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl, EntryPoint = "MLAPI_GetErrorMessage")]
        extern static MLAPI_Status MLAPI_GetErrorMessage(int ErrorCode, [MarshalAs(UnmanagedType.BStr)] out string Desc);

        [DllImport("MysticLight_SDK.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl, EntryPoint = "MLAPI_GetDeviceInfo")]
        extern static MLAPI_Status MLAPI_GetDeviceInfo([MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] out string[] pDevType, [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] out string[] pLedCount);

        [DllImport("MysticLight_SDK.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl, EntryPoint = "MLAPI_GetLedInfo")]
        extern static MLAPI_Status MLAPI_GetLedInfo([MarshalAs(UnmanagedType.BStr)] string type, int index, [MarshalAs(UnmanagedType.BStr)] out string pName, [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] out string[] pLedStyles);

        [DllImport("MysticLight_SDK.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl, EntryPoint = "MLAPI_GetLedColor")]
        extern static MLAPI_Status MLAPI_GetLedColor([MarshalAs(UnmanagedType.BStr)] string type, int index, out int R, out int G, out int B);

        [DllImport("MysticLight_SDK.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl, EntryPoint = "MLAPI_GetLedStyle")]
        // extern static MLAPI_Status MLAPI_GetLedStyle([MarshalAs(UnmanagedType.BStr)] string type, int index, [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] out string[] style);
        extern static MLAPI_Status MLAPI_GetLedStyle([MarshalAs(UnmanagedType.BStr)] string type, int index, [MarshalAs(UnmanagedType.BStr)] out string style);

        [DllImport("MysticLight_SDK.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl, EntryPoint = "MLAPI_SetLedColor")]
        extern static MLAPI_Status MLAPI_SetLedColor([MarshalAs(UnmanagedType.BStr)] string type, int index, int R, int G, int B);

        [DllImport("MysticLight_SDK.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl, EntryPoint = "MLAPI_SetLedStyle")]
        extern static MLAPI_Status MLAPI_SetLedStyle([MarshalAs(UnmanagedType.BStr)] string type, int index, [MarshalAs(UnmanagedType.BStr)] string style);

        [DllImport("MysticLight_SDK.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl, EntryPoint = "MLAPI_GetDeviceName")]
        extern static MLAPI_Status MLAPI_GetDeviceName([MarshalAs(UnmanagedType.BStr)] string type, [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] out string[] DevName);

        [DllImport("MysticLight_SDK.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl, EntryPoint = "MLAPI_GetDeviceNameEx")]
        extern static MLAPI_Status MLAPI_GetDeviceNameEx([MarshalAs(UnmanagedType.BStr)] string type, int index, [MarshalAs(UnmanagedType.BStr)] out string DevName);

        [DllImport("MysticLight_SDK.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl, EntryPoint = "MLAPI_GetLedName")]
        extern static MLAPI_Status MLAPI_GetLedName([MarshalAs(UnmanagedType.BStr)] string type, [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] out string[] LedName);
        [DllImport("MysticLight_SDK.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl, EntryPoint = "MLAPI_SetLedColors")]
        extern static MLAPI_Status MLAPI_SetLedColors([MarshalAs(UnmanagedType.BStr)] string type, int AreaIndex, [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] ref string[] LedName, int[] R, int[] G, int[] B);
        [DllImport("MysticLight_SDK.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl, EntryPoint = "MLAPI_SetLedColorEx")]
        extern static MLAPI_Status MLAPI_SetLedColorEx([MarshalAs(UnmanagedType.BStr)] string type, int AreaIndex, [MarshalAs(UnmanagedType.BStr)] string LedName, int R, int G, int B, int Update);
        [DllImport("MysticLight_SDK.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl, EntryPoint = "MLAPI_SetLedColorSync")]
        extern static MLAPI_Status MLAPI_SetLedColorSync([MarshalAs(UnmanagedType.BStr)] string type, int AreaIndex, [MarshalAs(UnmanagedType.BStr)] string LedName, int R, int G, int B, int Update);
        [DllImport("MysticLight_SDK.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl, EntryPoint = "MLAPI_MysticLightControlNotify")]
        extern static MLAPI_Status MLAPI_MysticLightControlNotify(CallbackDelegate funcPointer);
        [DllImport("MysticLight_SDK.dll", SetLastError = true, CallingConvention = CallingConvention.Cdecl, EntryPoint = "MLAPI_SetLedColorsSync")]
        extern static MLAPI_Status MLAPI_SetLedColorsSync([MarshalAs(UnmanagedType.BStr)] string type, [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] ref string[] LedName, int[] R, int[] G, int[] B);


        public String errorMessage { get; private set; }

        public Boolean hasError { get; private set; }

        public Boolean initialized { get; private set; }

        public void callback()
        {
            //This is the function which will be called by the DLL

            MessageBox.Show("Called from the DLL..");
        }

        public MysticLightProxy()
        {
            Console.WriteLine("Initializing MLAPI");

            MLAPI_Status ret;

            ret = MLAPI_Initialize();
            if (ret != MLAPI_Status.MLAPI_OK)
            {
                this.errorMessage = constructErrorMessage(ret);
                this.hasError = true;
                return;
            }
            this.hasError = false;

            // Register Mystic Light Control Notify event
            CallbackDelegate CallbackDelegateInstance = new CallbackDelegate(callback);
            ret = MLAPI_MysticLightControlNotify(CallbackDelegateInstance);

            if (ret != MLAPI_Status.MLAPI_OK)
            {
                this.errorMessage = constructErrorMessage(ret);
            }

            this.initialized = true;
        }

        public MLDevice[]? getDeviceList()
        {
            // TODO: what to do with LedCount?
            String[] ledCount = null;
            String[] deviceList = null;
            MLAPI_Status ret = MLAPI_GetDeviceInfo(out deviceList, out ledCount);
            if (ret != MLAPI_Status.MLAPI_OK)
            {
                this.errorMessage = constructErrorMessage(ret);
                return null;
            }

            // TODO: get proper device name, currently my 5060 ti shows as MSI_VGA
            return deviceList.Select((dev, idx) => new MLDevice(dev, idx)).ToArray();
        }

        public String? getDeviceName(MLDevice device)
        {
            MLAPI_Status ret;

            String sName;

            // first try getDeviceNameEx
            ret = MLAPI_GetDeviceNameEx(device.name, 0, out sName);
            if (ret != MLAPI_Status.MLAPI_OK)
            {
                this.errorMessage = constructErrorMessage(ret);
                return null;
            }

            return sName;
        }

        public MLLed? getDeviceLed(MLDevice dev)
        {
            MLAPI_Status ret;

            String ledName;
            String[] ledStyles;

            ret = MLAPI_GetLedInfo(dev.name, dev.idx, out ledName, out ledStyles);
            if (ret != MLAPI_Status.MLAPI_OK)
            {
                this.errorMessage = constructErrorMessage(ret);
                return null;
            }

            return new MLLed(dev.name, ledName, ledStyles);
        }


        public void setLedStyle(MLDevice device, String style)
        {
            MLAPI_Status ret = MLAPI_SetLedStyle(device.name, device.idx, style);
            if (ret != MLAPI_Status.MLAPI_OK)
            {
                this.errorMessage = constructErrorMessage(ret);
                throw new MLAPIException(ret);
            }
            return;
        }

        public String? getCurrentLedStyle(MLDevice device)
        {
            string style;
            MLAPI_Status ret = MLAPI_GetLedStyle(device.name, device.idx, out style);
            if (ret != MLAPI_Status.MLAPI_OK)
            {
                this.errorMessage = constructErrorMessage(ret);
                return null;
            }
            return style;
        }


        private String constructErrorMessage(MLAPI_Status stat)
        {
            String error;
            MLAPI_Status ret = MLAPI_GetErrorMessage((int) stat, out error);
            return error;
        }

        public void release()
        {
            MLAPI_Release();
        }

        ~MysticLightProxy()
        {
            MLAPI_Release();
        }

    }
}
