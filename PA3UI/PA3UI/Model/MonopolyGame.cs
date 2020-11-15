
using PA3BackEnd.src.Monopoly;
using System;
using System.Windows.Controls;
using System.Windows.Threading;

namespace PA3UI.ui
{
    public partial class MonopolyGame : UserControl
    {
        private DispatcherTimer timer;
        private int timerTime;
        private int timeElapsed;
        private Player[] players;

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
            players = new Player[amountOfPlayers];

            for (int i = 0; i < amountOfPlayers; i++)
            {
                players[i] = new Player();
            }
        }
    }
}
