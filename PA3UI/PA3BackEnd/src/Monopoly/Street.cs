namespace PA3BackEnd.src.Monopoly
{
    public class Street: Property
    {
        private static int houses;
        private static int hotels;
        private bool status;
        private int[] rent;

        static Street()
        {
            InitializeHousesAndHotels();
        }

        public static void InitializeHousesAndHotels()
        {
            houses = 32;
            hotels = 12;
        }

        public static bool EnoughHousesAndHotelsAvailable(int houses, int hotels)
        {
            if (houses >= Street.houses && hotels >= Street.hotels)
            {
                Street.houses -= houses;
                Street.hotels -= hotels;
                return true;
            }
            return false;
        }

        public Street(int location, Group group, int price, int[] rent, string name):base(location, group, price, name) {
            this.rent = rent;
        }
        int HousesAvailable(){
            return houses;
        }
        int HotelsAvailable(){
            return hotels;
        }
        int DevelopmentStatus(){
            return developmentValue;
        }
        
        public override bool CanBeMortaged() {

            if (isMortaged)
            {
                return false;
            }

            if (developmentValue != 0)
            {
                return false;
            }

            if (group.GetAmountPlayerOwns(owner) == 1)
            {
                return true;
            }

            if (group.HasAnyBuildings())
            {
                return false;
            }
            return true;
        }
        
        public override int GetRent() {
            if (isMortaged)
            {
                return 0;
            }
            return rent[developmentValue];
        }

        public override bool CanPlayerBuild(int playerId)
        {
            if (owner != playerId)
            {
                return false;
            }

            if (group.GetAmountPlayerOwns(playerId) != group.Count)
            {
                return false;
            }

            return true;
        }
    }
}