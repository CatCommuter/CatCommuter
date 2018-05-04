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
    public sealed partial class Preferences : Page
    {
        
        BusStopManager bsManager = BusStopManager.getInstance();
        ISet<BusLine> busLinesSet;
       
        public Preferences()
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

            DataContext = busLinesSet;

            route_ListView.ItemsSource = busLinesSet;
            ReloadBusLines();
        }
  

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            BusLine sampleLine = new BusLine("I'm A New Line", new TimeSpan(20), new DateTime());
            ISet<DateTime> times = new HashSet<DateTime>();
            times.Add(new DateTime());
            bsManager.busLines.Add(sampleLine, times);
            BasicGeoposition position = new BasicGeoposition
            {
                Latitude = 37.365269,
                Longitude = -120.426608
            };
            bsManager.busStops.Add(new BusStop("Some Pass", bsManager.busLines, position));
            ReloadBusLines();
        }

        private void ReloadBusLines()
        {
            ISet<BusLine> busLinesSet = new HashSet< BusLine > ();
            ICollection<BusLine> co = bsManager.busLines.Keys;

            List<BusLine> bs = bsManager.busLines.Keys.ToList();

            for(int i = 0; i < bs.Count; i++)
            {
                busLinesSet.Add(bs[i]);
            }

            route_ListView.ItemsSource = null;
            route_ListView.ItemsSource = busLinesSet;
        }



        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggleSwitch = sender as ToggleSwitch;
            if (toggleSwitch != null)
            {
                /*
                if (toggleSwitch.IsOn == true)
                {
                    progress1.IsActive = true;
                    progress1.Visibility = Visibility.Visible;
                }
                else
                {
                    progress1.IsActive = false;
                    progress1.Visibility = Visibility.Collapsed;
                }
                */
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
