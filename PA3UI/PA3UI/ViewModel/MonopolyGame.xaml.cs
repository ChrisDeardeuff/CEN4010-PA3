using System;
using System.Windows.Controls;

namespace PA3UI.ui
{
    /// <summary>
    /// Interaction logic for MonopolyGame.xaml
    /// </summary>
    public partial class MonopolyGame : UserControl
    {
        private Action<UserControl> changeUserControl;

        public MonopolyGame(int players, int Timer, Action<UserControl> changePage)
        {
            this.changeUserControl = changePage;
            InitializeComponent();
        }
    }
}
