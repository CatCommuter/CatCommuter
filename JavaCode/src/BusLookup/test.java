package BusLookup;

import java.io.FileNotFoundException;
import java.io.FileReader;
import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.Scanner;
import java.util.ArrayList;

public class test {

	public static void main(String[] args) {
		
		// Get the start location
		System.out.println("Please pick a start and end destination:");
		String filename = "C:/Users/David/Documents/Cattracks App/C1 Line Fall 2017.csv";
		
		try {
			Scanner scan = new Scanner(new FileReader(filename));
			ArrayList<String> stations = new ArrayList<String>();
			
			int i = 0;
			while (scan.hasNextLine()) {
				stations.add(scan.next());
				scan.nextLine();
				System.out.println(stations.get(i));
				i++;
			}
			
			scan.close();
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		// Get the end location
		
		
		
		// Assume current time, but use other time of needed
		
		
		String timeStamp = new SimpleDateFormat("HH:mm:ss mm/dd/yyyy").format(Calendar.getInstance().getTime());
		
		DateFormat df = new SimpleDateFormat("HH:mm:ss mm/dd/yyyy");
		Date now = null;
		try {
			now = df.parse(timeStamp);
		} catch (ParseException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		System.out.print("time is " + timeStamp);
		Calendar c = Calendar.getInstance();
		c.setTime(now);
		//Which lines run on this time
		
	}
	
	//public static String pickBus (Stop A, Stop B) {
		
	//}

}
