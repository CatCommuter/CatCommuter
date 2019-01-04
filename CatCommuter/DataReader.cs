using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Windows.Devices.Geolocation;
using Windows.Storage;
using System.Runtime.Serialization.Json;
//using Newtonsoft.Json.Linq; // has jobject
//using System.Xml.Linq;

//static int /// <summary>
///   The main entry point for the application
/// </summary>
//[STAThread]
namespace CatCommuter
{
    public class DataReader
    {


        // Takes in the .csv file with the bus schedule data of this line.
        // Returns a BusLine object with that line's data
        // Returns null if error
        public static async System.Threading.Tasks.Task<IDictionary<string, IList<string>>> ReadScheduleCSVAsync(StorageFile file)
        {
            try
            {
                IDictionary<string, IList<string>> busStopTimes = new Dictionary<string, IList<string>>();
                Debug.WriteLine("Reading Bus Data From File \"" + file.Path + "\"");

                //Read bus schedule data in from the .csv file
                var stream = await file.OpenStreamForReadAsync();
                StreamReader scheduleReader = new StreamReader(stream);
                
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

                    busStopTimes[stopName] = times;
                }
                return busStopTimes;
            }
            catch (Exception e)
            {
                Debug.WriteLine("In ReadScheduleCSV, an error \"" + e + "\" occured!");
            }
            return null;
        }

        

        // Takes in with the .csv file with the bus schedule data of this line.
        // Returns a dictionary mapping from stop names to bus latitude and longitude coordinates
        // Returns null if error
        public static async System.Threading.Tasks.Task<IDictionary<string, Tuple<double,double>>> ReadBusLocations(StorageFile file) {
            Debug.WriteLine("Reading bus stop locations from file " + Path.GetFileName(file.Name) + "\nAt location " + file.Path + ".\n");
            Debug.WriteLine(file.OpenReadAsync().ToString());



            // Attempt 1:
            /*
            // TODO: Read the bus coordinates into ImportSchedulePage.xaml.cs to plot the bus locations at the correct coordinates

            //Get storage file as stream
            Stream fileStream = (await file.OpenReadAsync()).AsStreamForRead();

            //Get deserialize the json stream into the equivalent c# struct
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(busStopJsonStruct));
            busStopJsonStruct busStopsStruct = (busStopJsonStruct)ser.ReadObject(fileStream);

            IDictionary<string, Tuple<double, double>> busStopsDict = busStopsStruct.toBusStopsDict();    //new Dictionary<string, Tuple<double, double>>;


            // TODO: Read the data from the busStopsStruct into the dictionary
            */






            // Attempt 2:
            // Copied from : https://www.codeproject.com/Questions/1223036/How-to-read-JSON-in-Csharp
            /*
            using (Stream jsonStream = (await file.OpenReadAsync()).AsStreamForRead())
            {
                if (jsonStream != null)
                {
                    using (StreamReader responseReader = new StreamReader(jsonStream))
                    {
                        string response = responseReader.ReadToEnd();
                        JObject jal = response as JObject;  // Requires using Newtonsoft.Json.Linq; // has jobject
                        busStopJsonStruct Obj = jal.ToObject<busStopJsonStruct>();
                        // Here You can handle json object values
                    }
                }
            }
            */

            // Attempt 3: Using CSV file instead of json, to read the locations and coordinates
            Stream fileStream = (await file.OpenReadAsync()).AsStreamForRead();
            
            // Read bus location data in from the .csv file
            StreamReader stopReader = new StreamReader(fileStream);

            // Declare the dictionary
            IDictionary<string, Tuple<double, double>> busStopsDict = new Dictionary<string, Tuple<double, double>>();
            // Each line in file should have times for a new stop
            while (!stopReader.EndOfStream)
            {
                string lineStr = stopReader.ReadLine();
                string[] lineVals = lineStr.Split(','); //.ToList<string>();
                Debug.Write("\t" + lineVals[0] + ",\t\t" + Double.Parse(lineVals[1]) + ",\t\t" + Double.Parse(lineVals[2]));

                if (!busStopsDict.ContainsKey(lineVals[0])) { // If the key has not been seen before, then add it. Otherwise just use the first instance of the key, and the initial coordinates.
                    busStopsDict.Add(lineVals[0], new Tuple<double, double>(Double.Parse(lineVals[1]), Double.Parse(lineVals[2])));
                    Debug.WriteLine("");
                } else
                {
                    Debug.WriteLine("\t\tDUPLICATE OF PREVIOUS LOCATION NAME: Location not added into dictionary.");

                }
                
                
                
            }


            return busStopsDict;
        }





        //static void Main(string[] args)
        //{
        //	//ReadScheduleCSV("../CatCommuter/assets/C2_test.csv");
        //	ReadScheduleCSV("C2_test.csv");
        //}
    }
}