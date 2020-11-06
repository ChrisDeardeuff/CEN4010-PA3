package Monopoly;

public class Street extends Property{

    private int houses;
    private int hotels;
    private int developments;
    private boolean status;

    public Street(int location, Group group, int price) {
        super(location, group, price);
    }
    int HousesAvailable(){
        return houses;
    }
    int HotelsAvailable(){
        return hotels;
    }
    int DevelopmentStatus(){
        return developments;
    }
    boolean CanPlayerBuild(){
        return status;
    }

    @Override
    public boolean CanBeMortaged() {
        return false;
    }

    @Override
    public int GetRent() {
        return 0;
    }

    @Override
    public int GetAction() {
        return 0;
    }
}
