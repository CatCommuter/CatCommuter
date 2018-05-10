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
        static BusLine ReadScheduleCSV(string filename)
        {
            try
            {

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
                    //string stopName = lineStr.Substring(0, lineStr.IndexOf())
                    //string[] rowItems = lineStr.Split(',');

                    BasicGeoposition stopPosition = new BasicGeoposition();
                    stopPosition.Latitude = 37.365269;
                    stopPosition.Longitude = -120.426608;

                    foreach (string cellItem in lineStr.Split())
                    {

                    }

                    //busStops.Add(new BusStop(stopName, ));
                    BusStop currentStop = new BusStop(stopName, null, stopPosition);
                    BusStopManager.getInstance().busStops.Add(currentStop);
                    // Create the bus stop
                    //BusStop currentStop = new BusStop();

                    // Read in the times into the stop

                }
                //}

                BusLine sampleLine = new BusLine(filename, timeSpan, busStartTime);
                return sampleLine;
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