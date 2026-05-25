using LightControlCustom.Vendors.Common;
using LightControlCustom.Vendors.MysticLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LightControlCustom
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate void CallbackDelegate();

        public DateTime dt, dt2;
        public int frame = 0;

        struct LED_Info
        {
            public int AreaIndex;
            public string FullName;
        }

        public void callback()
        {
            //This is the function which will be called by the DLL

            MessageBox.Show("Called from the DLL..");
        }

        string[] LedNames, LedName;

        MysticLightProxy mlClient;


        public ObservableCollection<String> Leds { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            mlClient = new MysticLightProxy();

            if (mlClient.hasError)
            {
                Console.WriteLine("Encountered error when setting up MysticLightClient");
                displayErrorMessage(mlClient.errorMessage);
                return;
            }

            // get device info
            // TODO: have this run in the background and not block UI initialization
            // create loading state
            MLDevice[]? mlDevices = mlClient.getDeviceList();
            if (mlDevices == null)
            {
                displayErrorMessage(mlClient.errorMessage);
                return;
            }

            // TODO: if lenght is 0, disable the select and show error message


            // found device list, populate dropdown 1
            Device1.ItemsSource = new ObservableCollection<Device>(mlDevices);

            // TODO: list devices at left panel
        }

        private void populateLeds(MLDevice device)
        {
            String deviceName = device.DisplayName;
            List<LED_Info> list_LED_Info = new List<LED_Info>();
            MLLed? mlLed = mlClient.getDeviceLed(device);
            if (mlLed == null)
            {
                displayErrorMessage("No Led found for device");
                return;
            }


            textBox_Result.AppendText($"LED[{device.idx}]: {mlLed.LedName}\n");
            textBox_Result.AppendText($"  Styles: {string.Join(", ", mlLed.LedStyles)}\n");

            Effect1.ItemsSource = new ObservableCollection<String>(mlLed.LedStyles);
            Effect1.IsEnabled = true;

            String? style = mlClient.getCurrentLedStyle(device);
            if (style != null && mlLed.LedStyles.Contains(style))
            {
                Effect1.SelectedItem = style;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (mlClient.initialized)
            {
                mlClient.release();
            }
        }

        private void Device1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                // TODO: support more device types
                var newValue = e.AddedItems[0] as MLDevice; // Cast to your object type
                if (newValue != null)
                {
                    populateLeds(newValue);
                }
            }
        }

        private void btn_Set_Effect(object sender, RoutedEventArgs e)
        {
            MLDevice? selectedDevice = Device1.SelectedItem as MLDevice;
            if (selectedDevice == null || Effect1.SelectedValue.Equals(null))
            {
                return;
            }

            mlClient.setLedStyle(selectedDevice, Effect1.SelectedValue.ToString());
        }

        // more like color off
        private void ColorShift()
        {
            byte R = 0;
            byte G = 0;
            byte B = 0;
            int step = 20;

            while (true)
            {
                // G up
                for (int i = 0; i <= step; i++)
                {
                    byte ii = (byte)((250 / step) * i); //G 0~255 brightness
                    G = ii;
                    SetLedColorSync_AllMB(LedNames, R, G, B);
                    frame++;
                }
                //R down
                for (int i = step; i >= 0; i--)
                {
                    byte ii = (byte)((250 / step) * i); //R 0~255 brightness
                    R = ii;
                    SetLedColorSync_AllMB(LedNames, R, G, B);
                    frame++;
                }
                //B up
                for (int i = 0; i <= step; i++)
                {
                    byte ii = (byte)((250 / step) * i); //B 0~255 brightness
                    B = ii;
                    SetLedColorSync_AllMB(LedNames, R, G, B);
                    frame++;
                }
                //G down
                for (int i = step; i >= 0; i--)
                {
                    byte ii = (byte)((250 / step) * i); //G 0~255 brightness
                    G = ii;
                    SetLedColorSync_AllMB(LedNames, R, G, B);
                    frame++;
                }
                //R up
                for (int i = 0; i <= step; i++)
                {
                    byte ii = (byte)((250 / step) * i); //R 0~255 brightness
                    R = ii;
                    SetLedColorSync_AllMB(LedNames, R, G, B);
                    frame++;
                }
                //B down
                for (int i = step; i >= 0; i--)
                {
                    byte ii = (byte)((250 / step) * i); //B 0~255 brightness
                    B = ii;
                    SetLedColorSync_AllMB(LedNames, R, G, B);
                    frame++;
                }
            }
        }

        private void SetLedColorSync_AllMB(string[] LedNames, int R, int G, int B)
        {
            int[] R0, G0, B0;

            R0 = new int[LedNames.Length];
            G0 = new int[LedNames.Length];
            B0 = new int[LedNames.Length];
            for (int i = 0; i < LedNames.Length; i++)
            {
                R0[i] = R;
                G0[i] = G;
                B0[i] = B;
            }

            // MLAPI_SetLedColorsSync("test", ref LedNames, R0, G0, B0);
        }

        private void displayErrorMessage(String message)
        {
            textBox_Diagnostics.AppendText(message);
        }
    }
}