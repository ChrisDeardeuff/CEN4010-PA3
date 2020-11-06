package Monopoly;

public abstract class Property extends Field{
	Group group;
	int owner;
	int price;
	boolean isMortaged;
	
	public Property(int location, Group group, int price)
	{
		super(location);
		this.group = group;
		this.price = price;
		isMortaged = false;
		owner = -1;
	}
	
	public int GetPrice()
	{
		return price;
	}
	
	public int GetOwner()
	{
		return owner;
	}
	
	public Group GetGroup()
	{
		return group;
	}
	
	public int MortageProperty() 
	{
		if(!CanBeMortaged())
		{
			return 0;
		}
		
		isMortaged = true;
		
		return price;
	}
	
	public int UnMortageProperty()
	{
		if(!isMortaged)
		{
			return 0;
		}
		
		isMortaged = false;
		return price;
	}
	
	public abstract boolean CanBeMortaged();
	
	public abstract int GetRent();
}	
