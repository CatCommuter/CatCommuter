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
    public sealed partial class MapPage : Page
    {
        String name;
        BusStopManager bsManager;
        public MapPage()
        {
            this.InitializeComponent();
            bsManager = BusStopManager.getInstance();
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void goPreferencesButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Preferences));
        }
        private void ClosestStop_Click(object sender, RoutedEventArgs e)
        {
            BasicGeoposition gp = new BasicGeoposition(); //replace this with the current device location
            gp.Longitude = -120.422507;
            gp.Latitude = 37.367543;
            //bsManager.getBusStop(gp).name;

            name = bsManager.getBusStop(gp).name;
            MapSearchTextBox.Text = name;
            BingMapsDialog(bsManager.getBusStop(gp).location, 15);
        }

        public void BingMapsDialog(BasicGeoposition center, int zoom)
        {
            InitializeComponent();
            Geopoint geopoint = new Geopoint(center);
            Map.Center = geopoint;
            Map.ZoomLevel = zoom;
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ImportSchedulePage));
        }

        private void AppBarButton_Click_2(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginPage));
        }
    }
}
