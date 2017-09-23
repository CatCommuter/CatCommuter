package BusLookup;

import java.util.ArrayList;

public class Station {
	// Data stored in station object:
	private String name = "NoName";
	ArrayList<Bus> Busses = new ArrayList<Bus>();
	
	
	// Station Constructors:
	public Station () {
		this.name = "This station was not named";
	}
	
	public Station(String name) {
		this.name = name;
	}
	
	
	// Station accessors:
	public String getName() {
		return this.name;
	}
	
	
	// Station mutators:
	public void setName(String name) {
		this.name = name;
	}
	
}
