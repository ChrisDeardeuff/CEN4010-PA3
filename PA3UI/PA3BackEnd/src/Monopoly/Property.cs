namespace PA3BackEnd.src.Monopoly
{
    public abstract class Property : Field
    {
        public Group group { private set; get; }
        public int owner { private set; get; }
        public int price { private set; get; }
        public bool isMortaged { private set; get; }
        public string name { private set; get; }



        public Property(int location, Group group, int price, string name) : base(location)
        {
            this.group = group;
            this.price = price;
            this.name = name;
            isMortaged = false;
            owner = -1;
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
            return price * -1;
        }

        public abstract bool CanBeMortaged();

        public abstract int GetRent();

        public int BoughtByPlayer(int playerId) 
        {
            owner = playerId;
            return price;
        }

        public override Actions GetAction()
        {
            if (owner == -1)
            {
                return Actions.canBuy;
            }
            else 
            {
                return Actions.payRent;
            }
        }
    }
}
