using System.Windows.Controls;

namespace PA3UI.ui
{
    /// <summary>
    /// Interaction logic for PropertyLeft.xaml
    /// </summary>
    public partial class PropertyLeft : Tile
    {
        public PropertyLeft(int y, int x)
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
            SetDevelopmentOfProperty(level, House1, House2, House3, House4, Hotel);
        }
    }
}
