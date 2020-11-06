package Monopoly;

public class Railroads extends Property{

    int rent;

    public Railroads(int location, Group group, int price) {
        super(location, group, price);
    }

    @Override
    public boolean CanBeMortaged() {
        return false;
    }

    @Override
    public int GetRent() {
        return rent;
    }

    @Override
    public int GetAction() {
        return 0;
    }
}
