namespace PA3BackEnd.src.Monopoly
{
    public class Utility: Property
    {
        int rent = 1;

        public Utility(int location, Group group, int price):base(location, group, price){
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