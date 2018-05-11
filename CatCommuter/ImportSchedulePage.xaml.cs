using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CatCommuter
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ImportSchedulePage : Page
    {
        public ImportSchedulePage()
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

        
        private async void ImportButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            //picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.ComputerFolder;
            picker.FileTypeFilter.Add(".csv");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();

            if (file == null)
            {
                ConsoleTextBlock.Text = "No file selected";
                return;
            }

            IDictionary<string, IList<string>> rawData = await DataReader.ReadScheduleCSVAsync(file);
            ConsoleTextBlock.Text = "";
            foreach (string stop in rawData.Keys)
            {
                ConsoleTextBlock.Text += stop;
            }
        }

        private async void InitializeButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            StorageFile file = await Package.Current.InstalledLocation.GetFileAsync("C2_test.csv");
            IDictionary<string, IList<string>> rawData = await DataReader.ReadScheduleCSVAsync(file);
            ConsoleTextBlock.Text = "";
            foreach (string stop in rawData.Keys)
            {
                ConsoleTextBlock.Text += stop;
            }
        }
    }
}
