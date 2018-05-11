using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Geolocation;
using Windows.UI.Core;
using Windows.UI.Popups;



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
            //hide back button
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;

            AddPins();
        }


        private void AddPins()
        {
            var LandMarks = new List<MapElement>();

            for (int i = 0; i < bsManager.busStops.Count; i++)
            {
                BusStop stop = bsManager.busStops.ElementAt<BusStop>(i);
                var newBusStopPin = new MapIcon
                {
                    Location = new Geopoint(stop.location),
                    NormalizedAnchorPoint = new Point(.5, 1.0),

                    ZIndex = 0,
                    Title = stop.name
                };

                LandMarks.Add(newBusStopPin);
                Map.MapElements.Add(newBusStopPin);
            }

            //Only usable in Fall Creators update:
            //var LandMarksLayer = new MapElementsLayer
            //{
            //    ZIndex = 1,
            //    MapElements = LandMarks
            //};
            //Map.Layers.Add(LandMarksLayer);

        }

        private void List_Click(object sender, RoutedEventArgs e)
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
            CenterMap(bsManager.getBusStop(gp).location, 15);
        }

        public void CenterMap(BasicGeoposition center, int zoom)
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

        private async void MapSearchTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {


            if (e.Key == Windows.System.VirtualKey.Enter /*|| search button is pressed*/)
            {
                // if Enter key is pressed
                // read text in the box and
                string dest = MapSearchTextBox.Text;
                dest = dest.ToLower();
                BusStopManager busStopManager = BusStopManager.getInstance();

                // compare to names of stops
                // by looping through BusStops collection in BusStopManager

                foreach (BusStop stop in busStopManager.busStops)
                {
                    // whichever one is the closest (maybe by string compare functions?)
                    // if doing closest stop name to destination name, consider Fuzzy Search
                    /**************
                     * Option 1:
                     *  Compare destination names to all stop names and assign a value based on how close they are
                     *  Potentially Dice Coefficient
                     *  Choose the one that has the value closest to 1 (which indicates it's the most accurate)
                     *  BingMapsDialog(stop.location, 15); where location is the closest to 1 (in terms of text comparison)
                     *  
                     * Option 2:
                     *  
                     **************/

                    if (stop.name.ToLower().Equals(dest))
                    {
                        CenterMap(stop.location, 15);
                        return;
                    }
            
                }

                // map will center onto that stop (probably by lat/long coordinates)
                var messageDialog = new MessageDialog("No bus stop detected.");
                messageDialog.Commands.Add(new UICommand("Close"));
                messageDialog.CancelCommandIndex = 0;
                await messageDialog.ShowAsync();

                // look at partial search i.e., "Muir" gives you "Muir Pass"
                // could also look at suggestions drop box


            }
        }
    }
}
