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
        private Board board;

        public MonopolyGame(int players, int Timer, Action<UserControl> changePage)
        {
            this.changeUserControl = changePage;
            InitializeComponent();
            board = new Board(players);
            board.SetValue(Grid.ColumnProperty, 1);
            board.SetValue(Grid.RowProperty, 2);
            mainGrid.Children.Add(board);
        }
    }
}
