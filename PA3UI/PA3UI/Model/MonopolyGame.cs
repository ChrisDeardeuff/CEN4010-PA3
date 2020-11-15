
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
            //check if in prison
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

            ShowDialogBoxOK($"Player {currentsPlayerTurn+1} it is your turn now !", null);
        }

        private void DiceRole(int x, int y)
        {
            roles.Add(new int[] { x, y });
            if (roles.Count == 3 && x == y)
            {
                GoToPrison();
            }
            else if (x != y)
            {
                DiceButton.IsEnabled = false;
                ShowEndTurnButton();
            }

            int oldPosition = players[currentsPlayerTurn].position;
            players[currentsPlayerTurn].movePlayerForward(x + y);
            board.SetPositionOfPlayer(oldPosition, players[currentsPlayerTurn].position, currentsPlayerTurn);
            LoadPlayerDataTopBar();

            //action of field landed on
            switch (fields.GetFieldAt(players[currentsPlayerTurn].position).GetAction())
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
                default:
                    break;
            }
        }

        private void PayRent() 
        {
            var property = (Property)fields.GetFieldAt(players[currentsPlayerTurn].position);
            if (property.owner == currentsPlayerTurn)
            {
                return;
            }

            int rent = property.GetRent();

            ShowDialogBoxOK($"You need To pay ${rent} rent to Player{property.owner}({GetUserTokenName(property.owner)})", (object sender, RoutedEventArgs args) => {
                players[currentsPlayerTurn].subtractBalance(rent);
                players[property.owner].addBalance(rent);
                LoadPlayerDataTopBar();
            });
        }

        private void CanBuy() 
        {
            var property = (Property)fields.GetFieldAt(players[currentsPlayerTurn].position);
            ShowDialogBoxYesNo($"You have landed on a Property that can be bought for ${property.price}\nWould you like to Buy it ?", (object sender, RoutedEventArgs args) => {
                if (((Dialog)sender).yes)
                {
                    players[currentsPlayerTurn].subtractBalance(property.BoughtByPlayer(currentsPlayerTurn));
                    players[currentsPlayerTurn].addProperty(property);
                    LoadPlayerDataTopBar();
                    //and property list
                }
                else 
                {
                    //start biding for property
                }
            });
        }

        private void GoToPrison() 
        {
            int oldPosition = players[currentsPlayerTurn].position;
            players[currentsPlayerTurn].goToJail();
            board.SetPositionOfPlayer(oldPosition, players[currentsPlayerTurn].position, currentsPlayerTurn);
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
