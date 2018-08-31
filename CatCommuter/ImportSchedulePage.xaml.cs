using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using Windows.ApplicationModel;
using Windows.Devices.Geolocation;
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
        Random random = new Random();
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
            ConsoleTextBlock.Text = "Imported the following stops: ";
            BusLine busLine = new BusLine(file.Name, new TimeSpan(), new DateTime(2018, 5, 11, 6, 11, 0));
            ISet<DateTime> times = new HashSet<DateTime>();

            foreach (string stop in rawData.Keys)
            {
                ConsoleTextBlock.Text += stop;
                BasicGeoposition position = new BasicGeoposition
                {
                    Latitude = GetRandomNumber(37.304328, 37.350193),
                    Longitude = GetRandomNumber(-120.494791, -120.444666)
                };
                BusStop busStop = new BusStop(stop, null, position);
                BusStopManager.getInstance().busStops.Add(busStop);
                busLine.addStop(busStop);
            }
            BusStopManager.getInstance().busLines[busLine] = new HashSet<DateTime>();
        }

        public double GetRandomNumber(double minimum, double maximum)
        {
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        private async void InitializeButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            StorageFile file = await Package.Current.InstalledLocation.GetFileAsync("C2_test.csv");
            IDictionary<string, IList<string>> rawData = await DataReader.ReadScheduleCSVAsync(file);
            ConsoleTextBlock.Text = "Imported the following stops: ";

            BusLine busLine = new BusLine(file.Name, new TimeSpan(), new DateTime(2018, 5, 11, 6, 11, 0));
            foreach (string stop in rawData.Keys)
            {
                ConsoleTextBlock.Text += stop;
                BasicGeoposition position = new BasicGeoposition
                {
                    Latitude = GetRandomNumber(37.304328, 37.350193),
                    Longitude = GetRandomNumber(-120.494791, -120.444666)
                };
                //TODO: times
                ISet<DateTime> times = new HashSet<DateTime>();
                foreach (string time in rawData[stop])
                {
                    times.Add(new DateTime(2018, 4, 11, random.Next(24), random.Next(60), random.Next(60)));
                }
                IDictionary<BusLine, ISet<DateTime>> buslinestuff = new Dictionary<BusLine, ISet<DateTime>>();
                buslinestuff[busLine] = times;
                BusStop busStop = new BusStop(stop, buslinestuff, position);
                BusStopManager.getInstance().busStops.Add(busStop);
                busLine.addStop(busStop);
            }
            BusStopManager.getInstance().busLines[busLine] = new HashSet<DateTime>();            
        }

		private async void ImportStopLocations_Click(object sender, RoutedEventArgs e)
		{
			String fileName = "BusStops_test.json";
			Debug.WriteLine("***** ImportStopLocations_Click CALLED *****");
			try
			{
				StorageFile file = await Package.Current.InstalledLocation.GetFileAsync(fileName);
			}
			catch (System.IO.FileNotFoundException ex)
			{
				//Debug.WriteLine("Error: " + fileName + " not found");
				Debug.WriteLine(ex.ToString());
				//MessageBox.Show("Error: " + fileName + " not found");
				//DisplayAlert	// DisplayAlery works in Xamrin

				ContentDialog LocationFileNotFound = new ContentDialog
				{
					Title = "Oops! ",
					Content = "Location file \"" + fileName + "\" not found!",
					CloseButtonText = "Ok"
				};
				ContentDialogResult result = await LocationFileNotFound.ShowAsync();

				return;
			}
			
		}
	}
}
