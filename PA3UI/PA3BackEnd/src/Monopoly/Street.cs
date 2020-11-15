namespace PA3BackEnd.src.Monopoly
{
    public class Street: Property
    {
        private int houses;
        private int hotels;
        private int developments;
        private bool status;

        public Street(int location, Group group, int price):base(location, group, price) {
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
            return 0;
        }
    }
}