using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Windows.Devices.Geolocation;

//static int /// <summary>
///   The main entry point for the application
/// </summary>
//[STAThread]
namespace CatCommuter
{
    public class DataReader
    {


        // Takes in file name of the .csv with the bus schedule data of this line.
        // Returns a BusLine object with that line's data
        // Returns null if error
        static IDictionary<String, IList<String>> ReadScheduleCSV(string filename)
        {
            try
            {
                IDictionary<String, IList<String>> busStopTimes = new Dictionary<String, IList<String>>();

                Debug.WriteLine("Reading Bus Data From File \"" + filename + "\"");

                // Parameters to create BusLine object

                string busName = filename;
                DateTime busStartTime = new DateTime();
                TimeSpan timeSpan = new TimeSpan();


                //List<BusStop> busStops = new List<BusStop>();


                //Read bus schedule data in from the .csv file
                StreamReader scheduleReader = new StreamReader(File.OpenRead(filename));
                //using (StreamReader scheduleReader = new StreamReader(filename))
                //{



                // Each line in file should have times for a new stop
                while (!scheduleReader.EndOfStream)
                {
                    string lineStr = scheduleReader.ReadLine();

                    // Skip over titles/empty lines at top of file that don't contain bus times
                    Regex timeFormat = new Regex("[0-9]?[0-9]:[0-9][0-9]");
                    if (!timeFormat.IsMatch(lineStr))
                    {
                        continue;   // skip this line
                    }

                    Debug.WriteLine("Is a time line!!!: " + lineStr);

                    // Get the name of the stop
                    string stopName = Regex.Match(lineStr, @"^.*?(?=[0-9]?[0-9]:[0-9][0-9])").Value;

                    IList<String> times = new List<String>();

                    foreach (string cellItem in lineStr.Split())
                    {
                        times.Add(cellItem);
                    }

                    busStopTimes.Add(stopName, times);
                }
                return busStopTimes;
            }
            catch (Exception e)
            {
                Debug.WriteLine("In ReadScheduleCSV, an error \"" + e + "\" occured!");
            }
            return null;
        }

        //static void Main(string[] args)
        //{
        //	//ReadScheduleCSV("../CatCommuter/assets/C2_test.csv");
        //	ReadScheduleCSV("C2_test.csv");
        //}
    }
}