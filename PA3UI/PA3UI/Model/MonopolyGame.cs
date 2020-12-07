
using PA3BackEnd.src.Monopoly;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;


namespace PA3UI.ui
{
    public partial class MonopolyGame : UserControl
    {
        private DispatcherTimer timer;
        private int timerTime;
        private int timeElapsed;
        private Random rnd;
        private PA3BackEnd.src.Monopoly.MonopolyGame monopolyGame;

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
            monopolyGame = new PA3BackEnd.src.Monopoly.MonopolyGame(amountOfPlayers);
            NextPlayersTurn();
        }

        private void NextPlayersTurn()
        {
            monopolyGame.NextPlayersTurn();
            HideEndTurnButton();
            cardViewer.CLear();
            DiceButton.IsEnabled = monopolyGame.CanRoleDice;

            LoadPlayerDataTopBar();
            LoadPlayerDataProperties();
            LoadDevelopmentValues();

            if (monopolyGame.currentPlayerInPrison)
            {
                ShowDialogBoxOK($"Player {monopolyGame.currentPlayerID + 1} ({monopolyGame.currentPlayerTokenName}) it is your turn now !\n\t but you are in Prison!", null);
                return;
            }

            ShowDialogBoxOK($"Player {monopolyGame.currentPlayerID + 1} ({monopolyGame.currentPlayerTokenName}) it is your turn now !\n Roll the dice!", null);
        }

        private void DiceRole(int x, int y)
        {
            RoutedEventHandler action;
            switch (monopolyGame.DiceRole(x, y, out action))
            {
                case 0:
                    break;
                case 1:
                    ShowDialogBoxOK($"You now have to pay the fine of $50", (object sender, RoutedEventArgs args) =>
                    {
                        action.Invoke(sender, args);
                        NextPlayersTurn();
                    });
                    break;
                case 2:
                    ShowDialogBoxOK($"You rolled doubles and got out of Prison!\n\tHowever, this ends your turn!", (object sender, RoutedEventArgs args) =>
                    {
                        action.Invoke(sender, args);
                        NextPlayersTurn();
                    });
                    break;
                case 3:
                    ShowDialogBoxYesNo($"You did not role doubles,\n but you can pay a fine of $50 to get out of prison.\n Would you like to pay the fine?", (object sender, RoutedEventArgs args) =>
                    {
                        action.Invoke(((Dialog)sender).yes, args);
                        NextPlayersTurn();
                    });
                    break;
                case 4:
                    ShowDialogBoxOK($"You rolled 3 doubles and are now in Prison!\n\tThis ends your turn!", (object sender, RoutedEventArgs args) =>
                    {
                        NextPlayersTurn();
                    });
                    break;
                case 5:
                    ShowDialogBoxOK($"You have to pay $100 in taxes", action);
                    break;
                case 6:
                    ShowDialogBoxOK($"You have to pay $200 in taxes", action);
                    break;
                case 7:
                    int rent;
                    int owner;
                    string propertyName;
                    monopolyGame.PayRent(out rent, out owner, out propertyName, out action);
                    if (action != null)
                    {
                        ShowDialogBoxOK($"You need To pay ${rent} rent to player {owner} ({PA3BackEnd.src.Monopoly.MonopolyGame.GetUserTokenName(owner)})\n on {propertyName}", action);
                    }
                    break;
                case 8:

                case 9:
                    ShowDialogBoxOK("You are now in prison, and you turn is over!", (object sender, RoutedEventArgs args) =>
                    {
                        NextPlayersTurn();
                    });
                    break;

            }
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

        private void DevelopProperty()
        {
            switch (monopolyGame.ApplyDevelopProperty())
            {
                case 0:
                    ShowDialogBoxOK($"You developed your property!", null);
                    break;
                case -1:
                    ShowDialogBoxOK($"No OutStanding Development!\n Use + and - button above", null);
                    break;
                case -2:
                    ShowDialogBoxOK($"You do not Have Enough Money!", null);
                    break;
                case -3:
                    ShowDialogBoxOK($"There are not enough houses or hotels left!", null);
                    break;
            }

            LoadDevelopmentValues();
            LoadPlayerDataTopBar();
        }

        private void PlaningDevelopment() 
        {
            int moneyNeeded;
            monopolyGame.CalculateMoneyAndHousesNeeded(out moneyNeeded, out _, out _);
            SetTextBlockMoneyNeeded($"${moneyNeeded}");
        }
    }
}
