using System;
using System.Windows.Controls;

namespace PA3UI.ui
{
    /// <summary>
    /// Interaction logic for Startup.xaml
    /// </summary>
    public partial class Startup : UserControl
    {
        private Action<UserControl> changeUserControl;

        public Startup(Action<UserControl> changePage)
        {
            this.changeUserControl = changePage;
            InitializeComponent();
        }

        private void Button_Click_TwoPlayers(object sender, System.Windows.RoutedEventArgs e)
        {
            StartGame(2);
        }

        private void Button_Click_ThreePlayers(object sender, System.Windows.RoutedEventArgs e)
        {
            StartGame(3);
        }

        private void Button_Click_FourPlayers(object sender, System.Windows.RoutedEventArgs e)
        {
            StartGame(4);
        }

        private void StartGame(int players) 
        {
            changeUserControl(new MonopolyGame(players, (int)sliderTimer.Value,changeUserControl));
        }

        private void sliderTimer_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            if (e.NewValue > 60)
            {
                textBlockTimerDisplay.Text = $"{(int)(e.NewValue / 60)}h {(int)e.NewValue %60}min";
            }
            else 
            {
                textBlockTimerDisplay.Text = $"{(int)e.NewValue}min";
            }
        }
    }
}
