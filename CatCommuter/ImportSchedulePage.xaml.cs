using System;
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

            ReadScheduleCSVAsync(file);
        }

        private async void InitializeButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            StorageFile file = await Package.Current.InstalledLocation.GetFileAsync("C2_test.csv");
            ReadScheduleCSVAsync(file);
        }

        async System.Threading.Tasks.Task<BusLine> ReadScheduleCSVAsync(StorageFile file)
        {
            try
            {

                ConsoleTextBlock.Text = "Reading Bus Data From File \"" + file.Path + "\"";

                //Read bus schedule data in from the .csv file
                BusLine sampleLine = new BusLine("C2", new TimeSpan(), new DateTime());
                var stream = await file.OpenStreamForReadAsync();
                StreamReader scheduleReader = new StreamReader(stream);
                //using (StreamReader scheduleReader = new StreamReader(filename))
                //{

                while (!scheduleReader.EndOfStream)
                {
                    string lineStr = scheduleReader.ReadLine();

                    // Skip over titles/empty lines at top of file that don't contain bus times
                    Regex timeFormat = new Regex("[0-9]?[0-9]:[0-9][0-9]");
                    if (!timeFormat.IsMatch(lineStr))
                    {
                        continue;   // skip this line
                    }

                    ConsoleTextBlock.Text = "Is a time line!!!: " + lineStr;
                }
                //}
                return sampleLine;
            }
            catch (Exception e)
            {
                ConsoleTextBlock.Text = e.Message + "\n" + e.StackTrace;
            }
            return null;
        }
    }
}
