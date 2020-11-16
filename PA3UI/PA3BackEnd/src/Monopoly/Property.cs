namespace PA3BackEnd.src.Monopoly
{
    public abstract class Property : Field
    {
        public Group group { private set; get; }
        public int owner { private set; get; }
        public int price { private set; get; }
        public int developmentValue { private set; get; }
        public bool isMortaged { get { return developmentValue == -1; } }
        public string name { private set; get; }



        public Property(int location, Group group, int price, string name) : base(location)
        {
            this.group = group;
            this.price = price;
            this.name = name;
            developmentValue = 0;
            owner = -1;
            group.AddProperty(this);
        }

        public void DevelopProperty(int level)
        {
            this.developmentValue = level;
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

        public abstract bool CanPlayerBuild(int playerId);
    }
}
