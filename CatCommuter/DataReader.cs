using System;
using System.Diagnostics;
using System.IO;
//using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
//static int /// <summary>
///   The main entry point for the application
/// </summary>
//[STAThread]
namespace CatCommuter {
	public class DataReader {
		//static void Main(string[] args)
		static bool ReadScheduleCSV(string filename)
		{
			Debug.WriteLine ("Reading Bus Data From File \"" + filename + "\"");

			//Read bus schedule data in from the .csv file
			BusLine sampleLine = new BusLine("C2", null, null);
			using (StreamReader scheduleReader = new StreamReader (filename))
			{
				
				while (!scheduleReader.EndOfStream) {
					string lineStr = scheduleReader.ReadLine();
                     
                    // Skip over titles/empty lines at top of file that don't contain bus times
                    Regex timeFormat = new Regex("[0-9]?[0-9]:[0-9][0-9]");
					if (!timeFormat.IsMatch(lineStr)) {
						continue;   // skip this line
					}

                    Debug.WriteLine("Is a time line!!!: " + lineStr);


                   

				}
			}
			return true;
		}
	}
}
