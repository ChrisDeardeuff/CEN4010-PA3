package Monopoly;

public abstract class Field {
	int location;
	
	public Field(int location)
	{
		this.location = location; 
	}
	
	public int GetLocation()
	{
		return location;
	}
	
	public abstract int GetAction();
}
