using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string currentPlayerTokenName { get { return GetUserTokenName(currentsPlayerTurn); } }

        public MonopolyGame(int Amountplayers)
        {
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
        /// Starts the next Players turn
        /// </summary>
        public void NextPlayersTurn()
        {

            roles.Clear();
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
        ///     0 = Shoe
        ///     1 = Thimble
        ///     2 = Car
        ///     3 = TopHat
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetUserTokenName(int id)
        {
            switch (id)
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
        /// -2 player has not enough money
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

            if (Street.EnoughHousesAndHotelsAvailable(HousesNeeded, HotelsNeeded))
            {
                return -3;
            }

            currentPlayer.subtractBalance(moneyNeeded);

            for (int i = 0; i < outstandingDevelopment.Length; i++)
            {
                DevelopProperty(outstandingDevelopment[i][0], outstandingDevelopment[i][1]);
            }
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

        private void DevelopProperty(int property)
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

        private void UnDevelopProperty(int property)
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
    }
}
