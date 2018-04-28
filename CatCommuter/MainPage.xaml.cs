using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Geolocation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CatCommuter
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        BusStopManager bsManager;

        public MainPage()
        {
            this.InitializeComponent();

            //hide back button
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            bsManager = BusStopManager.getInstance();
        }

        int times = 0;
        private void randomButton_Click(object sender, RoutedEventArgs e)
        {
            outputTextBlock.Text = "Oh no that button you pressed " + ++times + " times t r i g g e r e d an event!";
        }

        private void goSomewhereButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(BlankPage1));
        }

        private void goPreferencesButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Preferences));
        }

        private void MapPage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MapPage));
        }
    }

}
