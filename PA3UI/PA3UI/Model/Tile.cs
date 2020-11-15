using System.Windows.Controls;
using System.Windows;
using System.Windows.Shapes;

namespace PA3UI.ui
{
    public abstract class Tile : UserControl
    {
        public abstract void AddPlayerToken(UserControl playerToken, int id);
        public abstract void RemovePlayerToken(UserControl playerToken);

        /// <summary>
        /// Updates the visual part of properties on the board
        /// </summary>
        /// <param name="level">
        /// Valid values for:
        ///     Streets: -1 to 5
        ///     RailRoad -1 to 0
        /// </param>
        public abstract void SetDevelopmentOfProperty(int level);
        public void AddPlayerTokenToGrid(UserControl playerToken, Grid grid, int id)
        {
            int x = 0;
            int y = 0;

            if (id > 2)
            {
                y = 1;
            }

            if (id % 2 == 1)
            {
                x = 1;
            }


            playerToken.SetValue(Grid.RowProperty, x);
            playerToken.SetValue(Grid.ColumnProperty, y);
            grid.Children.Add(playerToken);
        }

        public void SetDevelopmentOfProperty(int level, Rectangle House1, Rectangle House2, Rectangle House3, Rectangle House4, Rectangle Hotel)
        {
            if (level == 5)
            {
                House1.Visibility = Visibility.Hidden;
                House2.Visibility = Visibility.Hidden;
                House3.Visibility = Visibility.Hidden;
                House4.Visibility = Visibility.Hidden;
                Hotel.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                Hotel.Visibility = Visibility.Hidden;
            }

            if (level == 4)
            {
                House4.Visibility = Visibility.Visible;
            }
            else
            {
                House4.Visibility = Visibility.Hidden;
            }

            if (level >= 3)
            {
                House3.Visibility = Visibility.Visible;
            }
            else
            {
                House3.Visibility = Visibility.Hidden;
            }

            if (level >= 2)
            {
                House2.Visibility = Visibility.Visible;
            }
            else
            {
                House2.Visibility = Visibility.Hidden;
            }

            if (level >= 1)
            {
                House1.Visibility = Visibility.Visible;
            }
            else
            {
                House1.Visibility = Visibility.Hidden;
            }
        }
    }
}
