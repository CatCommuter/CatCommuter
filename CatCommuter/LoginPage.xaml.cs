using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CatCommuter
{
    

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        bool loggedIn = false;
        public LoginPage()
        {
            this.InitializeComponent();
            //Add a back button
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            SystemNavigationManager.GetForCurrentView().BackRequested += (s, a) =>
            {
                if (Frame.CanGoBack)
                {
                    Frame.GoBack();
                    a.Handled = true;
                }
            };
        }

        //Currently unable to navigate to a new page from here
        //Also localSettings persists through app restarts
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (localSettings.Values["loggedin"] != null)
            {
                Frame.Navigate(typeof(RecentRoutesPage));
            }
        }

        string theUsername = "catcommuter";
        string thePassword = "secure";

        private async void AppBarButton_Click_Login(object sender, RoutedEventArgs e)
        {
            if (username.Text.ToLower().Equals(theUsername) && password.Password.ToLower().Equals(thePassword))
            {
                localSettings.Values["loggedin"] = true;
                loggedIn = true;
                Frame.Navigate(typeof(RecentRoutesPage));
            }
            else
            {
                var messageDialog = new MessageDialog("Login credentials are not correct");
                messageDialog.Commands.Add(new UICommand("Close"));
                messageDialog.CancelCommandIndex = 0;
                await messageDialog.ShowAsync();
            }
        }

        private void AppBarButton_Click_Bypass(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RecentRoutesPage));
        }
    }
}
