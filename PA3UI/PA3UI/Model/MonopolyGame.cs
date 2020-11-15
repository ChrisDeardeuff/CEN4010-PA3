
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

            NextPlayersTurn();
        }

        private void NextPlayersTurn()
        {

            HideEndTurnButton();
            roles.Clear();
            DiceButton.IsEnabled = true;
            currentsPlayerTurn++;
            currentsPlayerTurn %= players.Length;

            if (currentsPlayerTurn == 0)
            {
                Round++;
            }

            LoadPlayerDataTopBar();

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
            }

            ShowDialogBoxYesNo($"You have landed on a property that can be bought\n\nWould you like to buy it for ${property.price}?", (object sender, RoutedEventArgs args) =>
            {
                if (((Dialog)sender).yes)
                {
                    currentPlayer.subtractBalance(property.BoughtByPlayer(currentsPlayerTurn));
                    currentPlayer.addProperty(property);
                    LoadPlayerDataTopBar();
                    //and property list
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

                if (playerid == currentsPlayerTurn || !currentPlayer.HasEnoughMoney(property.price))
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

        private string GetUserTokenName(int id)
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
