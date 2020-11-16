using System;

namespace PA3BackEnd.src.Monopoly
{
    public class Railroads: Property
    {
        public Railroads(int location, Group group, int price, string name) : base(location, group, price, name) {
            
        }
        
        public override bool CanBeMortaged() {
            return !isMortaged;
        }

        public override bool CanPlayerBuild(int playerId)
        {
            return false;
        }

        public override int GetRent() {
            if (isMortaged)
            {
                return 0;
            }
            int amount = this.group.GetAmountPlayerOwns(owner);
            return ((int)Math.Pow(2, amount-1))*25;
        }
    }
}