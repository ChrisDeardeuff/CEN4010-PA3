namespace PA3BackEnd.src.Monopoly
{
    public abstract class Property : Field
    {
        Group group;
	    int owner;
	    int price;
        bool isMortaged;



    public Property(int location, Group group, int price) : base(location)
    {
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
        if (!CanBeMortaged())
        {
            return 0;
        }

        isMortaged = true;

        return price;
    }

    public int UnMortageProperty()
    {
        if (!isMortaged)
        {
            return 0;
        }

        isMortaged = false;
        return price;
    }

    public abstract bool CanBeMortaged();

    public abstract int GetRent();
}
}
