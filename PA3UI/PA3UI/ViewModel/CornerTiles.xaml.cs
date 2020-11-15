using System.Windows.Controls;

namespace PA3UI.ui
{
    /// <summary>
    /// Interaction logic for CornerTiles.xaml
    /// </summary>
    public partial class CornerTile : Tile
    {
        public CornerTile(int y, int x)
        {
            InitializeComponent();
            SetValue(Grid.RowProperty, x);
            SetValue(Grid.ColumnProperty, y);
        }

        public override void AddPlayerToken(UserControl playerToken, int id)
        {
            this.AddPlayerTokenToGrid(playerToken, MainGrid, id);
        }

        public override void RemovePlayerToken(UserControl playerToken)
        {
            MainGrid.Children.Remove(playerToken);
        }

        public override void SetDevelopmentOfProperty(int level)
        {
            throw new System.NotSupportedException();
        }
    }
}
