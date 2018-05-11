using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Geolocation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CatCommuter
{


    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditStop : Page
    {
        BusStop busStopToEdit;
        BusStopManager bsManager = BusStopManager.getInstance();
        string name;
        BasicGeoposition position;
        BusLine busLineSelected = Preferences.toEditBusLine;

        public EditStop()
        {
            this.InitializeComponent();
            busStopToEdit = EditLine.toEditBusStop;
            
        }

        private void AddNewStop_Click(object sender, RoutedEventArgs e)
        {
            bsManager.addBusStop(name, position, busLineSelected);
        }
    }
}
