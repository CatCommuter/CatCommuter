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

using Windows.UI.Core;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CatCommuter
{


    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditStop : Page
    {
        BusStop busStopToEdit { get; set; }
        BusStopManager bsManager = BusStopManager.getInstance();
        BusLine busLineSelected = Preferences.toEditBusLine;


        public EditStop()
        {
            this.InitializeComponent();
            //Add a back button

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            SystemNavigationManager.GetForCurrentView().BackRequested += (s, a) =>
            {
                //Debug.WriteLine("BackRequested");
                if (Frame.CanGoBack)
                {
                    Frame.Navigate(typeof(EditLine));
                    a.Handled = true;
                }
            };

            busStopToEdit = EditLine.toEditBusStop;
            stop_ListView.DataContext = null;
            stop_ListView.DataContext = busStopToEdit;

        }

        private void AddNewStop_Click(object sender, RoutedEventArgs e)
        {
            //bsManager.addBusStop(name, busStopToEdit.position, busLineSelected);
            busStopToEdit.updatePosition();
            Frame.Navigate(typeof(EditLine));
        }
    }
}
