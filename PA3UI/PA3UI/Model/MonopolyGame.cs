
using PA3BackEnd.src.Monopoly;
using System;
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
            var userControl = new UserControl();
            var rectanngle = new Rectangle();
            //rectanngle.Opacity = 128;
            rectanngle.Fill = Brushes.Black;
            userControl.Content = rectanngle;

            MainWindow.AddUserControl(userControl);
            //userControl.

            //lock develop trade and buy property

            currentsPlayerTurn++;
            currentsPlayerTurn %= players.Length;

            if (currentsPlayerTurn == 0)
            {
                Round++;
            }

            // load Data

            // bring up window that it is player x turn
        }


    }
}
