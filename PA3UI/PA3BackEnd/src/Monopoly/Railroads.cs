namespace PA3BackEnd.src.Monopoly
{
    public class Railroads: Property
    {
        public Railroads(int location, Group group, int price, string name) : base(location, group, price, name) {
            
        }
        
        public override bool CanBeMortaged() {
            return false;
        }
        
        public override int GetRent() {
            int amount = this.group.GetAmountPlayerOwns(owner);
            return amount * 50;
        }
    }
}