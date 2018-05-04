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
		

		// Takes in file name of the .csv with the bus schedule data of this line.
		// Returns a BusLine object with that line's data
		// Returns null if error
		static BusLine ReadScheduleCSV(string filename)
		{
			try
			{

				Debug.WriteLine("Reading Bus Data From File \"" + filename + "\"");

				//Read bus schedule data in from the .csv file
				BusLine sampleLine = new BusLine("C2");

				StreamReader scheduleReader = new StreamReader(File.OpenRead(filename));
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

							Debug.WriteLine("Is a time line!!!: " + lineStr);




					}
				//}
				return sampleLine;
			}
			catch (Exception e)
			{
				Debug.WriteLine("In ReadScheduleCSV, an error \"" + e + "\" occured!");
			}
			return null;
		}

		static void Main(string[] args)
		{
			ReadScheduleCSV("../CatCommuter/assets/C2_test.csv");
		}
	}
}
