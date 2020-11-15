using System;
using System.Diagnostics;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace PA3UI.ui
{
    /// <summary>
    /// Interaction logic for MonopolyGame.xaml
    /// </summary>
    public partial class MonopolyGame : UserControl
    {
        private Action<UserControl> changeUserControl;
        private Board board;

        public MonopolyGame(int players, int timerTime, Action<UserControl> changePage)
        {
            this.changeUserControl = changePage;
            InitializeComponent();
            board = new Board(players);
            board.SetValue(Grid.ColumnProperty, 1);
            board.SetValue(Grid.RowProperty, 2);
            mainGrid.Children.Add(board);

            Ch.Add_EventHandler(viewDeed_OnClick);
            
            
            timeElapsed = 0;
            this.timerTime = timerTime;
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0,1,0);
            timer.Tick += Timer_Elapsed;
            SetRemainingTime(timerTime);
            timer.Start();


        }

        private void SetRemainingTime(int timeLeft) 
        {
            if (timeLeft < 0)
            {
                timeLeft = 0;
            }

            if (timeLeft > 60)
            {
                textBlockTimer.Text = $"Time Left: {timeLeft / 60}h {timeLeft % 60}min";
            }
            else
            {
                textBlockTimer.Text = $"Time Left: {timeLeft}min";
            }
        }

        private void EndGame()
        {
            timer.Stop();
            SetRemainingTime(0);
        }

        private void Button_Resign_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            EndGame();
        }
        private void viewDeed_OnClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
