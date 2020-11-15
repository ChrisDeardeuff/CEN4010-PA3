namespace PA3BackEnd.src.Monopoly
{
    public class Railroads: Property
    {
        int rent;

        public Railroads(int location, Group group, int price):base(location, group, price) {
            
        }
        
        public override bool CanBeMortaged() {
            return false;
        }
        
        public override int GetRent() {
            return rent;
        }
        
        public override int GetAction() {
            return 0;
        }
    }
}