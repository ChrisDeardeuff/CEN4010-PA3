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
        private UserToken[] userTokens;

        public Board(int numberOfPlayers)
        {
            InitializeComponent();

            userTokens = new UserToken[4];
            userTokens[0] = new UserToken("/Assets/Tokens/Shoe.png");
            userTokens[1] = new UserToken("/Assets/Tokens/Thimble.png");
            userTokens[2] = new UserToken("/Assets/Tokens/Car.png");
            userTokens[3] = new UserToken("/Assets/Tokens/TopHat.png");

            tiles = new Tile[40];
            tiles[0] = new CornerTile(10, 10);
            tiles[10] = new CornerTile(0, 10);
            tiles[20] = new CornerTile(0, 0);
            tiles[30] = new CornerTile(10, 0);

            for (int i = 1; i < 10; i++) 
            {
                tiles[i] = new PropertyBottom(10 -i, 10);
                tiles[i+10] = new PropertyLeft(0, 10-i);
                tiles[i+20] = new PropertyTop(i, 0);
                tiles[i+30] = new PropertyRight(10,i);

            }

            for (int i = 0; i < 40; i++)
            {
                mainGrid.Children.Add(tiles[i]);
            }

            for (int i = 0; i < numberOfPlayers; i++)
            {
                tiles[0].AddPlayerToken(userTokens[i], i+1);
            }
        }

        private void Board_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            rowDefinition1.Height = new GridLength(e.NewSize.Height);
            columDefinition1.Width = new GridLength(e.NewSize.Width);
        }

        public void SetDevelopmentLevelOfTile(int id, int level)
        {
            tiles[id].SetDevelopmentOfProperty(level);
        }

        public void SetPositionOfPlayer(int oldLocation, int newLocation, int id) 
        {
            tiles[oldLocation].RemovePlayerToken(null);
            tiles[newLocation].AddPlayerToken(null, id);
        }
    }
}
