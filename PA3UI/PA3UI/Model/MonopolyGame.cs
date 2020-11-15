
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
        private bool playerDidRole;
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
            playerDidRole = false;
            currentsPlayerTurn++;
            currentsPlayerTurn %= players.Length;

            if (currentsPlayerTurn == 0)
            {
                Round++;
            }

            LoadPlayerData();

            ShowDialogBoxOK($"Player {currentsPlayerTurn+1} it is your turn now !", null);
        }




    }
}
