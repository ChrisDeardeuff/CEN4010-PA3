package Monopoly;

public class Utility extends Property{

    int rent = 1;

    public Utility(int location, Group group, int price) {
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
