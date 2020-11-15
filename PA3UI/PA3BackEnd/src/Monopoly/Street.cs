namespace PA3BackEnd.src.Monopoly
{
    public class Street: Property
    {
        private int houses;
        private int hotels;
        private int developments;
        private bool status;
        private int[] rent;

        public Street(int location, Group group, int price, int[] rent):base(location, group, price) {
            this.rent = rent;
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
        bool CanPlayerBuild(){
            return status;
        }
        
        public override bool CanBeMortaged() {
            return false;
        }
        
        public override int GetRent() {
            return rent[developments];
        }
    }
}