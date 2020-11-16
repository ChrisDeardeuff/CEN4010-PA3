
using PA3BackEnd.src.Monopoly;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PA3UI.ui
{
    public partial class MonopolyGame : UserControl
    {
        private int currentsPlayerTurn;
        private DispatcherTimer timer;
        private int timerTime;
        private int timeElapsed;
        private int Round;
        private Player[] players;
        private Fields fields;
        private List<int[]> roles;
        private int[][] outstandingDevelopment;
        private Random rnd;
        private Player currentPlayer { get { return players[currentsPlayerTurn]; } }

        private void Timer_Elapsed(object sender, EventArgs e)
        {
            timeElapsed++;
            int timeLeft = timerTime - timeElapsed;
            SetRemainingTime(timeLeft);
            if (timeLeft <= 0)
            {
                timer.Stop();
                EndGame();
            }
        }

        private void InitilizeData(int amountOfPlayers)
        {
            roles = new List<int[]>();
            Round = 0;
            currentsPlayerTurn = -1;

            players = new Player[amountOfPlayers];

            for (int i = 0; i < amountOfPlayers; i++)
            {
                players[i] = new Player();
            }

            fields = new Fields();

            //players[0].addProperty((Property) fields.GetFieldAt(1));
            //((Property)fields.GetFieldAt(1)).BoughtByPlayer(0);
            //players[0].addProperty((Property) fields.GetFieldAt(3));
            //((Property)fields.GetFieldAt(3)).BoughtByPlayer(0);
            //players[0].addProperty((Property)fields.GetFieldAt(5));
            //((Property)fields.GetFieldAt(5)).BoughtByPlayer(0);
            //players[0].addProperty((Property)fields.GetFieldAt(6));
            //((Property)fields.GetFieldAt(6)).BoughtByPlayer(0);
            //players[0].addProperty((Property)fields.GetFieldAt(8));
            //((Property)fields.GetFieldAt(8)).BoughtByPlayer(0);
            //players[0].addProperty((Property)fields.GetFieldAt(9));
            //((Property)fields.GetFieldAt(9)).BoughtByPlayer(0);

            NextPlayersTurn();
        }

        private void NextPlayersTurn()
        {

            HideEndTurnButton();
            roles.Clear();
            cardViewer.CLear();
            DiceButton.IsEnabled = true;
            currentsPlayerTurn++;
            currentsPlayerTurn %= players.Length;

            if (currentsPlayerTurn == 0)
            {
                Round++;
            }

            LoadPlayerDataTopBar();
            LoadPlayerDataProperties();
            LoadDevelopmentValues();

            if (currentPlayer.inPrison)
            {
                ShowDialogBoxOK($"Player {currentsPlayerTurn + 1} ({GetUserTokenName(currentsPlayerTurn)}) it is your turn now !\n\t but you are in Prison!", null);
                return;
            }

            ShowDialogBoxOK($"Player {currentsPlayerTurn + 1} ({GetUserTokenName(currentsPlayerTurn)}) it is your turn now !\n Roll the dice!", null);
        }

        private void DiceRole(int x, int y)
        {
            roles.Add(new int[] { x, y });

            if (currentPlayer.inPrison)
            {
                currentPlayer.UpdateInPrisonCounter();
                if (x == y)
                {
                    ShowDialogBoxOK($"You rolled doubles and got out of Prison!\n\tHowever, this ends your turn!", (object sender, RoutedEventArgs args) =>
                    {
                        currentPlayer.GetOutOfJail();
                        NextPlayersTurn();
                    });
                }
                else
                {

                    if (currentPlayer.inPrisonCounter != 3)
                    {
                        ShowDialogBoxYesNo($"You did not role doubles,\n but you can pay a fine of $50 to get out of prison.\n Would you like to pay the fine?", (object sender, RoutedEventArgs args) =>
                        {
                            if (((Dialog)sender).yes)
                            {
                                currentPlayer.GetOutOfJail();
                                currentPlayer.subtractBalance(50);
                                NextPlayersTurn();
                            }
                            else
                            {
                                NextPlayersTurn();
                            }
                        });
                    }
                    else
                    {
                        ShowDialogBoxOK($"You now have to pay the fine of $50", (object sender, RoutedEventArgs args) =>
                        {
                            currentPlayer.subtractBalance(50);
                            currentPlayer.GetOutOfJail();
                            NextPlayersTurn();
                        });
                    }
                }
                return;
            }

            if (roles.Count == 3 && x == y)
            {
                GoToPrison();
                return;
            }
            else if (x != y)
            {
                DiceButton.IsEnabled = false;
                ShowEndTurnButton();
            }

            int oldPosition = currentPlayer.position;
            currentPlayer.movePlayerForward(x + y);
            board.SetPositionOfPlayer(oldPosition, currentPlayer.position, currentsPlayerTurn);
            LoadPlayerDataTopBar();

            switch (fields.GetFieldAt(currentPlayer.position).GetAction())
            {
                case Actions.canBuy:
                    CanBuy();
                    break;
                case Actions.payRent:
                    PayRent();
                    break;
                case Actions.goToPrison:
                    GoToPrison();
                    break;
                case Actions.payTax100:
                    PayTax(100);
                    break;
                case Actions.payTax200:
                    PayTax(200);
                    break;
                default:
                    break;
            }
        }

        private void PayTax(int amount)
        {
            ShowDialogBoxOK($"You have to pay ${amount} in taxes", (object sender, RoutedEventArgs args) =>
            {
                currentPlayer.subtractBalance(amount);
                LoadPlayerDataTopBar();
            });
        }

        private void PayRent()
        {
            var property = (Property)fields.GetFieldAt(currentPlayer.position);
            if (property.owner == currentsPlayerTurn)
            {
                return;
            }
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

            ShowDialogBoxOK($"You need To pay ${rent} rent to player {property.owner + 1} ({GetUserTokenName(property.owner)})\n on {property.name}", (object sender, RoutedEventArgs args) =>
            {
                currentPlayer.subtractBalance(rent);
                players[property.owner].addBalance(rent);
                LoadPlayerDataTopBar();
            });
        }

        private void CanBuy()
        {
            var property = (Property)fields.GetFieldAt(currentPlayer.position);

            if (!currentPlayer.HasEnoughMoney(property.price))
            {
                BidForProperty(property, 0, -1, -1);
                return;
            }

            ShowDialogBoxYesNo($"{property.name} is not owned by anyone.\n\nWould you like to buy it for ${property.price}?", (object sender, RoutedEventArgs args) =>
            {
                if (((Dialog)sender).yes)
                {
                    currentPlayer.subtractBalance(property.BoughtByPlayer(currentsPlayerTurn));
                    currentPlayer.addProperty(property);
                    LoadPlayerDataTopBar();
                    LoadPlayerDataProperties();

                }
                else
                {
                    BidForProperty(property, 0, -1, -1);
                }
            });
        }

        private void BidForProperty(Property property, int highestBid, int highestBider, int playerid)
        {
            playerid++;

            if (playerid < players.Length)
            {

                if (playerid == currentsPlayerTurn || !players[playerid].HasEnoughMoney(property.price))
                {
                    BidForProperty(property, highestBid, highestBider, playerid);
                    return;
                }

                ShowDialogBoxBidding($"Player {playerid + 1} ({GetUserTokenName(playerid)}) you can bid for a Property!\n Please Enter your bid!", property.price, players[playerid].balance, (object sender, RoutedEventArgs args) =>
                {
                    var dialog = (Dialog)sender;
                    if (dialog.yes)
                    {
                        if (dialog.amount > highestBid)
                        {
                            highestBid = dialog.amount;
                            highestBider = playerid;
                        }
                    }
                    BidForProperty(property, highestBid, highestBider, playerid);
                });
                return;
            }

            if (highestBider == -1)
            {
                ShowDialogBoxOK("Nobody bid for the property !", null);
            }
            else 
            {
                players[highestBider].subtractBalance(highestBid);
                players[highestBider].addProperty(property);
                property.BoughtByPlayer(highestBider);
                ShowDialogBoxOK($"Player {highestBider +1} ({GetUserTokenName(highestBider)}) won the biding with a bid of ${highestBid}!", null);
            }
        }

        private void GoToPrison()
        {
            int oldPosition = currentPlayer.position;
            currentPlayer.goToJail();
            board.SetPositionOfPlayer(oldPosition, currentPlayer.position, currentsPlayerTurn);
            ShowDialogBoxOK("You are now in prison, and you turn is over!", (object sender, RoutedEventArgs args) => { NextPlayersTurn(); });
        }

        private void ResetDevelopValues()
        {
            outstandingDevelopment = null;
            SetTextBlockMoneyNeeded("$0");
        }

        private void DevelopProperty()
        {
            if (outstandingDevelopment == null)
            {
                ShowDialogBoxOK($"No OutStanding Development!\n Use + and - button above", null);
                return;
            }

            int moneyNeeded;
            int HousesNeeded;
            int HotelsNeeded;
            CalculateMoneyAndHousesNeeded(out moneyNeeded, out HousesNeeded, out HotelsNeeded);

            if (!currentPlayer.HasEnoughMoney(moneyNeeded))
            {
                ShowDialogBoxOK($"You do not Have Enough Money!", null);
                return;
            }

            if (!Street.EnoughHousesAndHotelsAvailable(HousesNeeded, HotelsNeeded))
            {
                ShowDialogBoxOK($"You developed your property!", null);
            }
            else
            {
                ShowDialogBoxOK($"There are not enough houses or hotels left!", null);
            }

            currentPlayer.subtractBalance(moneyNeeded);

            for (int i = 0; i < outstandingDevelopment.Length; i++)
            {
                DevelopProperty(outstandingDevelopment[i][0], outstandingDevelopment[i][1]);
            }
            ResetDevelopValues();
            LoadDevelopmentValues();
            LoadPlayerDataTopBar();
        }

        private void DevelopProperty(int location, int level) 
        {
            var prop = (Property)fields.GetFieldAt(location);
            prop.DevelopProperty(level);
        }

        private void PlaningDevelopment() 
        {
            int moneyNeeded;
            int HousesNeeded;
            int HotelsNeeded;
            CalculateMoneyAndHousesNeeded(out moneyNeeded, out HousesNeeded, out HotelsNeeded);
            SetTextBlockMoneyNeeded($"${moneyNeeded}");
        }

        private void CalculateMoneyAndHousesNeeded(out int moneyNeeded, out int HousesNeeded, out int HotelsNeeded) 
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
                    moneyNeeded -= (prop.developmentValue ) * prop.group.priceToBuild;
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
                    else if(newLevel - outstandingDevelopment[i][1] > 1)
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
    }
}
