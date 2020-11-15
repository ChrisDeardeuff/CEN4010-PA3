
using PA3BackEnd.src.Monopoly;
using System;
using System.Collections.Generic;
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
            DiceButton.IsEnabled = true;
            currentsPlayerTurn++;
            currentsPlayerTurn %= players.Length;

            if (currentsPlayerTurn == 0)
            {
                Round++;
            }

            LoadPlayerData();

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
            }

            int oldPosition = players[currentsPlayerTurn].getPosition();
            players[currentsPlayerTurn].movePlayerForward(x + y);
            board.SetPositionOfPlayer(oldPosition, players[currentsPlayerTurn].getPosition(), currentsPlayerTurn);

            //action of field landed on
        }

        private void GoToPrison() 
        {
            players[currentsPlayerTurn].goToJail();
            //got to message
        }


    }
}
