using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Devices.Geolocation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CatCommuter
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditLine : Page
    {
        BusLine busLineSelected;
        public EditLine()
        {
            this.InitializeComponent();

            busLineSelected = Preferences.toEditBusLine;
            DataContext = busLineSelected;
            //System.Diagnostics.Debug.WriteLine("Size:" + busLineSelected.busStops.Count);
            stop_ListView.ItemsSource = busLineSelected.busStops;
        }
    }
}
