using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
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
        public static IDictionary<string, Tuple<double, double>> BusStopLocationsMap = null;
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
                    //if (BusStopLocationsMap != null) {
                    //  Latitude = 
                    //  Longitude = BusStopLocationsMap
                    //}

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
            Debug.WriteLine("Attempting to open file " + fileName);
            Debug.WriteLine("");
            Debug.WriteLine("");

            // Look for the file by searching up several parent directories
            String filePath = Package.Current.InstalledLocation.Path;
            //Directory filePathDir = Package.Current.InstalledLocation.Di
            await Task.Run(() => {
                for (int i = 0; i < 5; i++)
                {
                    Debug.Write("Checking location " + filePath + "\\" + fileName + " : ");

                    // Sometimes it says the file does not exist when it really does, because the program does not have permission to access the file. I tested this permission with:
                    //Debug.WriteLine("Testing read:");
                    //var worked = File.OpenRead("C:\\Users\\david\\OneDrive\\Documents\\GitHub\\CatCommuter\\CatCommuter\\BusStops_test.json"); // May cause System.UnauthorizedAccessException

                    if (!(File.Exists(filePath + "\\" + fileName))) // If we have access to the higher directory, then we should be able to search for files here
                    {

                        //int lastDirectoryIndex = Math.Max(filePath.LastIndexOf("\\"), Math.Max(filePath.LastIndexOf("/"), 0));  // On different systems, the path might end in \ or /?
                        //filePath = filePath.Substring(0, lastDirectoryIndex); // Doesn't recognize the correct directory

                        filePath = Directory.GetParent(filePath).FullName;
                        //filePath = Path.GetDirectoryName(Path.GetDirectoryName(filePath));  // skips some directories. And still doesn't recognize the correct ones
                        

                        Debug.WriteLine(" Not Found :-(");
                    } else {
                        Debug.WriteLine(" File found in directory! :-)");
                        break;
                    }
                
                }
                if (!File.Exists(fileName))
                {
                    Debug.WriteLine("File was not found in nearest parent directories");
                    filePath = Package.Current.InstalledLocation.Path;
                }
            });
            


            try
			{
                //StorageFile file = await Package.Current.InstalledLocation.GetFileAsync(fileName);
                StorageFile file = await StorageFile.GetFileFromPathAsync(filePath + "\\" + fileName);

                // Read the file for stop data
                BusStopLocationsMap = await DataReader.ReadBusLocations(file);

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
