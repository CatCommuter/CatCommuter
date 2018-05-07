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
        BusStop sampleStop;
        public EditLine()
        {
            this.InitializeComponent();

            //Add a back button
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            SystemNavigationManager.GetForCurrentView().BackRequested += (s, a) =>
            {
                Debug.WriteLine("BackRequested");
                if (Frame.CanGoBack)
                {
                    Frame.GoBack();
                    a.Handled = true;
                }
            };

            busLineSelected = Preferences.toEditBusLine;
            DataContext = busLineSelected;
            //System.Diagnostics.Debug.WriteLine("Size:" + busLineSelected.busStops.Count);
            stop_ListView.ItemsSource = busLineSelected.busStops;

            DataContext = busLineSelected.busStops;


            _parentItems = busLineSelected.busStops;
        }

        private IList<BusStop> _parentItems;

        public IList<BusStop> ParentItems
        {
            get { return _parentItems; }
            set { _parentItems = value; }
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            
            BasicGeoposition position = new BasicGeoposition
            {
                Latitude = 37.363930,
                Longitude = -120.430695

            };
            BusStop sampleStop = new BusStop("Emigrant Pass at Scholars Lane ", BusStopManager.getInstance().busLines, position);
            BusStopManager.getInstance().busStops.Add(sampleStop);
            busLineSelected.addStop(sampleStop);

            stop_ListView.ItemsSource = null;
            stop_ListView.ItemsSource = busLineSelected.busStops;
            DataContext = busLineSelected.busStops;
            _parentItems = busLineSelected.busStops;
        }

    }
}
