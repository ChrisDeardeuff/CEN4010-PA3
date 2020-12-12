using System.Collections.Generic;
using System.Windows;

namespace PA3BackEnd.src.Monopoly
{
    public class MonopolyGame 
    {
        private int currentsPlayerTurn { get; set; }
        public int Round { get; private set; }
        private Player[] players;
        private Fields fields;
        private List<int[]> roles;
        private int[][] outstandingDevelopment;
        private Player currentPlayer { get { return players[currentsPlayerTurn]; } }
        

        public bool currentPlayerInPrison { get { return currentPlayer.inPrison; } }
        public int currentPlayerID { get { return currentsPlayerTurn + 1; } }
        public string currentPlayerTokenName { get { return GetUserTokenName(currentPlayerID); } }
        public int currentsPlayerLocation { get { return currentPlayer.position;  } }
        public int currentPlayerBalance { get { return currentPlayer.balance; } }
        public int amountOfPlayers { get { return players.Length; } }
        public bool CanRoleDice { get; private set; }
        public bool CanEndTurn { get { return !CanRoleDice; } }

        public MonopolyGame(int Amountplayers)
        {
            outstandingDevelopment = null;
            roles = new List<int[]>();
            Round = 0;
            currentsPlayerTurn = -1;

            players = new Player[Amountplayers];

            for (int i = 0; i < Amountplayers; i++)
            {
                players[i] = new Player();
            }

            fields = new Fields();
        }

        /// <summary>
        /// GetDevelopmentValue of property at location
        /// </summary>
        /// <param name="location"></param>
        /// <returns>
        /// n    returns developmentValue if property,
        ///     else returns -2
        /// 
        /// </returns>
        public int GetDevelopmentValue(int location)
        {
            try
            {
                return ((Property)fields.GetFieldAt(location)).developmentValue;
            }
            catch 
            {
                return -2;
            }
        }

        /// <summary>
        /// Starts the next Players turn
        /// </summary>
        public void NextPlayersTurn()
        {
            ResetDevelopValues();

            roles.Clear();
            CanRoleDice = true;
            currentsPlayerTurn++;
            currentsPlayerTurn %= players.Length;

            if (currentsPlayerTurn == 0)
            {
                Round++;
            }
        }

        /// <summary>
        /// Returns the user token name
        /// id:
        ///     1 = Shoe
        ///     2 = Thimble
        ///     3 = Car
        ///     4 = TopHat
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetUserTokenName(int id)
        {
            switch (id-1)
            {
                case 0:
                    return "Shoe";
                case 1:
                    return "Thimble";
                case 2:
                    return "Car";
                case 3:
                    return "TopHat";
            }
            return "";
        }

        /// <summary>
        /// Develops the properties according to outstanding Development
        /// </summary>
        /// <returns>
        ///  0 for success
        /// -1 if no outstanding Developments are available
        /// -2 player does not enough money
        /// -3 not enough houses or hotels left
        /// </returns>
        public int ApplyDevelopProperty()
        {
            if (outstandingDevelopment == null)
            {
                return -1;
            }

            int moneyNeeded;
            int HousesNeeded;
            int HotelsNeeded;
            CalculateMoneyAndHousesNeeded(out moneyNeeded, out HousesNeeded, out HotelsNeeded);

            if (!currentPlayer.HasEnoughMoney(moneyNeeded))
            {
                return -2;
            }

            if (!Street.EnoughHousesAndHotelsAvailable(HousesNeeded, HotelsNeeded))
            {
                return -3;
            }

            currentPlayer.subtractBalance(moneyNeeded);

            for (int i = 0; i < outstandingDevelopment.Length; i++)
            {
                DevelopProperty(outstandingDevelopment[i][0], outstandingDevelopment[i][1]);
            }
            ResetDevelopValues();
            return 0;
        }

        private void DevelopProperty(int location, int level)
        {
            var prop = (Property)fields.GetFieldAt(location);
            prop.DevelopProperty(level);
        }

        /// <summary>
        /// Calculates the ressources neccessary for the planed development
        /// </summary>
        /// <param name="moneyNeeded"></param>
        /// <param name="HousesNeeded"></param>
        /// <param name="HotelsNeeded"></param>
        public void CalculateMoneyAndHousesNeeded(out int moneyNeeded, out int HousesNeeded, out int HotelsNeeded)
        {
            moneyNeeded = 0;
            HousesNeeded = 0;
            HotelsNeeded = 0;

            if (outstandingDevelopment == null)
            {
                return;
            }

            for (int i = 0; i < outstandingDevelopment.Length; i++)
            {
                var prop = (Property)fields.GetFieldAt(outstandingDevelopment[i][0]);
                if (outstandingDevelopment[i][1] == prop.developmentValue)
                {
                    continue;
                }
                else if (outstandingDevelopment[i][1] == 5)
                {
                    HotelsNeeded++;
                    if (prop.developmentValue == -1)
                    {
                        moneyNeeded += prop.price / 2;
                        moneyNeeded += 5 * prop.group.priceToBuild;
                        continue;
                    }

                    HousesNeeded -= prop.developmentValue;
                    moneyNeeded += (5 - prop.developmentValue) * prop.group.priceToBuild;
                }
                else if (outstandingDevelopment[i][1] >= 0)
                {
                    if (prop.developmentValue >= 0)
                    {
                        moneyNeeded -= (prop.developmentValue - outstandingDevelopment[i][1]) * prop.group.priceToBuild;

                        if (prop.developmentValue == 5)
                        {
                            HotelsNeeded--;
                            HousesNeeded += outstandingDevelopment[i][1];
                        }
                        else
                        {
                            HousesNeeded += outstandingDevelopment[i][1] - prop.developmentValue;
                        }
                        continue;
                    }
                    else
                    {
                        HousesNeeded += outstandingDevelopment[i][1];
                        moneyNeeded += prop.price / 2;
                        moneyNeeded += outstandingDevelopment[i][1] * prop.group.priceToBuild;
                    }
                }
                else
                {
                    moneyNeeded -= (prop.developmentValue) * prop.group.priceToBuild;
                    moneyNeeded -= prop.price / 2;
                    if (prop.developmentValue == 5)
                    {
                        HotelsNeeded--;
                    }
                    else
                    {
                        HousesNeeded -= prop.developmentValue;
                    }
                }

            }
        }

        public void DevelopProperty(int property)
        {
            var prop = ((Property)fields.GetFieldAt(property));

            var properties = prop.group.properties;

            if (outstandingDevelopment == null)
            {
                int newLevel = prop.developmentValue + 1;

                if (newLevel >= 1)
                {
                    if (!prop.CanPlayerBuild(currentsPlayerTurn))
                    {
                        return;
                    }
                }

                if (newLevel > 5)
                {
                    return;
                }

                outstandingDevelopment = new int[properties.Length][];
                for (int i = 0; i < properties.Length; i++)
                {
                    outstandingDevelopment[i] = new int[] { properties[i].GetLocation(), properties[i].developmentValue };
                    if (outstandingDevelopment[i][0] == property)
                    {
                        outstandingDevelopment[i][1]++;
                    }
                    else if (newLevel - outstandingDevelopment[i][1] > 1)
                    {
                        outstandingDevelopment[i][1]++;
                    }
                }
                return;
            }

            int newLevel1 = 0;
            for (int i = 0; i < properties.Length; i++)
            {
                if (outstandingDevelopment[i][0] == property)
                {
                    newLevel1 = outstandingDevelopment[i][1] + 1;
                }
            }

            if (newLevel1 > 5)
            {
                return;
            }

            for (int i = 0; i < properties.Length; i++)
            {
                if (outstandingDevelopment[i][0] == property)
                {
                    outstandingDevelopment[i][1]++;
                }
                else if (newLevel1 - outstandingDevelopment[i][1] > 1)
                {
                    outstandingDevelopment[i][1]++;
                }
            }

        }

        public void UnDevelopProperty(int property)
        {
            var prop = ((Property)fields.GetFieldAt(property));

            var properties = prop.group.properties;

            if (outstandingDevelopment == null)
            {
                int newLevel = prop.developmentValue - 1;

                if (newLevel < -1)
                {
                    return;
                }

                outstandingDevelopment = new int[properties.Length][];
                for (int i = 0; i < properties.Length; i++)
                {
                    outstandingDevelopment[i] = new int[] { properties[i].GetLocation(), properties[i].developmentValue };
                    if (outstandingDevelopment[i][0] == property)
                    {
                        outstandingDevelopment[i][1]--;
                    }
                    else if (outstandingDevelopment[i][1] - newLevel > 1)
                    {
                        outstandingDevelopment[i][1]--;
                    }
                }
                return;
            }

            int newLevel1 = 0;
            for (int i = 0; i < properties.Length; i++)
            {
                if (outstandingDevelopment[i][0] == property)
                {
                    newLevel1 = outstandingDevelopment[i][1] - 1;
                }
            }

            if (newLevel1 < -1)
            {
                return;
            }

            for (int i = 0; i < properties.Length; i++)
            {
                if (outstandingDevelopment[i][0] == property)
                {
                    outstandingDevelopment[i][1]--;
                }
                else if (outstandingDevelopment[i][1] - newLevel1 > 1)
                {
                    outstandingDevelopment[i][1]--;
                }
            }
        }

        public void CalculateHighestPlayerScore(out int playerid, out int highestScore)
        {
            highestScore = players[0].CalculateScore();
            playerid = 0;

            for (int i = 1; i < players.Length; i++)
            {
                int score = players[i].CalculateScore();
                if (score > highestScore)
                {
                    highestScore = score;
                    playerid = i;
                }
            }
        }

        /// <summary>
        /// Resets Develop Values
        /// </summary>
        public void ResetDevelopValues()
        {
            outstandingDevelopment = null;
        }

        /// <summary>
        /// Dice role method, should be called for dice role
        /// </summary>
        /// <param name="x">role of dice 1</param>
        /// <param name="y">role of dice 2</param>
        /// <param name="action"> output action that needs to be executed after this method has finiished execution</param>
        /// <returns>
        ///     0 - No Action
        ///     1 - Player needs to pay a fine of 50 to get out of prison   (Should End Turn)
        ///     2 - Player gets out of prison, roled doubles, but end turn (Should End Turn)
        ///     3 - Player is still in prison (no doubles), but could pay a fine of 50 to get out (Should End Turn)
        ///     4 - roled 3 doubles in a row player needs to go to prison
        ///     5 - pay taxes 100
        ///     6 - pay taxes 200
        ///     7 - pay Rent (Should Call PayRent)
        ///     8 - Can Buy
        ///     9 - landed on go to prison
        /// </returns>
        public int DiceRoll(int x, int y, out RoutedEventHandler action)
        {
            roles.Add(new int[] { x, y });

            if (currentPlayer.inPrison)
            {
                CanRoleDice = false;
                currentPlayer.UpdateInPrisonCounter();
                if (x == y)
                {
                    action = (object sender, RoutedEventArgs args) =>
                    {
                        currentPlayer.GetOutOfJail();
                    };
                    return 2;
                }
                else
                {

                    if (currentPlayer.inPrisonCounter != 3)
                    {
                        action = (object sender, RoutedEventArgs args) =>
                        {
                            if ((bool)sender)
                            {
                                currentPlayer.GetOutOfJail();
                                currentPlayer.subtractBalance(50);
                            }
                        };
                        return 3;
                    }
                    else
                    {
                        action = (object sender, RoutedEventArgs args) =>
                        {
                            currentPlayer.subtractBalance(50);
                            currentPlayer.GetOutOfJail();
                        };
                        return 1;
                    }
                }
            }

            if (roles.Count == 3 && x == y)
            {
                currentPlayer.goToJail();
                action = null;
                CanRoleDice = false;
                return 4;
            }

            else if (x != y)
            {
                CanRoleDice = false;
            }

            int oldPosition = currentPlayer.position;
            currentPlayer.movePlayerForward(x + y);

            var prop = fields.GetFieldAt(currentPlayer.position);

            switch (prop.GetAction())
            {
                case Actions.canBuy:
                    action = null;
                    return 8;
                case Actions.payRent:
                    action = null;
                    if (((Property)prop).owner == currentsPlayerTurn)
                    {
                        return 0;
                    }
                    return 7;
                case Actions.goToPrison:
                    action = null;
                    currentPlayer.goToJail();
                    CanRoleDice = false;
                    return 9;
                case Actions.payTax100:
                    PayTax(100, out action);
                    return 5;
                case Actions.payTax200:
                    PayTax(200, out action);
                    return 6;
                default:
                    action = null;
                    return 0;
            }
        }

        /// <summary>
        /// Method to pay rent to the owner of the property the currentPlayer is standing on
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="toPlayer"></param>
        public void PayRent(out int amount, out int toPlayer,out string propertyName, out RoutedEventHandler action)
        {
            var property = (Property)fields.GetFieldAt(currentPlayer.position);
            
            int rent = 0;

            if (property is Utility)
            {
                rent = roles[roles.Count - 1][0] + roles[roles.Count - 1][1];
                rent = ((Utility)property).GetRent(rent);
            }
            else
            {
                rent = property.GetRent();
            }

            propertyName = property.name;
            amount = rent;
            toPlayer = property.owner + 1;
            action = (object sender, RoutedEventArgs args) =>
            {
                currentPlayer.subtractBalance(rent);
                players[property.owner].addBalance(rent);
            };
        }

        private void PayTax(int amount, out RoutedEventHandler action)
        {
            action = (object sender, RoutedEventArgs args) =>
            {
                currentPlayer.subtractBalance(amount);
            };
        }

        /// <summary>
        /// method that returns wheather or not a player can buy the property on which he is standing
        /// </summary>
        /// <returns>
        ///     if the player can buy the property
        /// </returns>
        public bool CanBuy(out string name, out int price)
        {
            var property = (Property)fields.GetFieldAt(currentPlayer.position);
            name = property.name;
            price = property.price;

            if (!currentPlayer.HasEnoughMoney(property.price))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Buys the propertie on which the currentPlayer is standing on
        /// </summary>
        /// <returns></returns>
        public void BuyProperty() 
        {
            var property = (Property)fields.GetFieldAt(currentPlayer.position);

            currentPlayer.subtractBalance(property.BoughtByPlayer(currentsPlayerTurn));
            currentPlayer.addProperty(property);
        }

        /// <summary>
        /// Method to Complete Bid
        /// </summary>
        /// <param name="highestBider"></param>
        /// <param name="price"></param>
        public void CompleteBid(int highestBider, int highestBid) 
        {
            var property = (Property)fields.GetFieldAt(currentsPlayerLocation);
            players[highestBider].subtractBalance(highestBid);
            players[highestBider].addProperty(property);
            property.BoughtByPlayer(highestBider);
        }

        /// <summary>
        /// Returns list of property owned by currentPlayer
        /// </summary>
        /// <returns></returns>
        public List<int> GetPropertiesOwnedByPlayer(int player = -1)
        {
            if (player == -1)
            {
                player = currentsPlayerTurn;
            }

            var list = new List<int>();
            foreach (var prop in players[player].getPropertiesOwned()) 
            {
                list.Add(prop.GetLocation());
            }

            return list;
        }

        /// <summary>
        /// Returns the name of a Property
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public string GetNameOfProperty(int location)
        {
            var field = fields.GetFieldAt(location);
            if (field is Property)
            {
                return ((Property)field).name;
            }
            return "";
        }

        /// <summary>
        /// Returns price of a Property
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public int GetPriceOfProperty(int location) 
        {
            var field = fields.GetFieldAt(location);
            if (field is Property)
            {
                return ((Property)field).price;
            }
            return -1;
        }

        public bool HasAnyBuildingsOnIt(int location)
        {
            var field = fields.GetFieldAt(location);
            if (field is Property)
            {
                return ((Property) field).group.HasAnyBuildings();
            }
            return false;
        }

        public int GetBalanceOfPlayer(int index)
        {
            return players[index].balance;
        }

        public void CompleteTrade(List<int> properties0, List<int> properties1, int money, int p0, int p1)
        {
            foreach (var prop in properties0)
            {
                players[p0].removeProperty((Property)fields.GetFieldAt(prop));
                players[p1].addProperty((Property)fields.GetFieldAt(prop));
                ((Property)fields.GetFieldAt(prop)).BoughtByPlayer(p1);
            }

            foreach (var prop in properties1)
            {
                players[p1].removeProperty((Property)fields.GetFieldAt(prop));
                players[p0].addProperty((Property)fields.GetFieldAt(prop));
                ((Property)fields.GetFieldAt(prop)).BoughtByPlayer(p0);
            }

            players[p0].subtractBalance(money);
            players[p1].addBalance(money);
        }
    }
}
