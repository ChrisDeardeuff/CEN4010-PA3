using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PA3UI.ui
{
    /// <summary>
    /// Interaction logic for PropertyLeft.xaml
    /// </summary>
    public partial class PropertyRight : Tile
    {
        public PropertyRight(int y, int x)
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
