using System.Windows;
using System.Windows.Controls;

namespace PA3UI.ui
{
    /// <summary>
    /// Interaction logic for Board.xaml
    /// </summary>
    public partial class Board : UserControl
    {
        private Tile[] tiles;

        public Board()
        {
            InitializeComponent();

            tiles = new Tile[40]; 
        }

        private void Board_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            rowDefinition1.Height = new GridLength(e.NewSize.Height);
            columDefinition1.Width = new GridLength(e.NewSize.Width);
        }
    }
}
