package Monopoly;

import com.oracle.webservices.internal.api.message.PropertySet.Property;

public class Group {
	private Property[] properties;
	private int priceToBuild;
	private int currentAmount;
	
	public Group(int size) {
		propertise = new Property[size];
	}
	
	public void AddProperty(Property property) 
	{
		properties[currentAmount] = property;
		currentAmount++;
	}
	
	public boolean CanPlayerBuild(int playerid) {
		for(int i = 0; i < properties.length; i++)
		{
			if(properties[i].GetOwner() != playerid)
			{
				return false;
			}
		}
		return true;
	}
}
