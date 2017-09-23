package BusLookup;

public class Bus {
	private String name = "unnamed bus";
	
	public Bus () {
		name = "This bus was not given a name";
	}
	
	public Bus (String name) {
		this.name = name;
	}
	
	public String getName () {
		return name;
	}
	
	public void setName (String name) {
		this.name = name;
	}
	
	
	
}
