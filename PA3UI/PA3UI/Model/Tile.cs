using System.Windows.Controls;

namespace PA3UI.ui
{
    public abstract class Tile : UserControl
    {
        public abstract void AddPlayerToken(UserControl playerToken);
        public abstract void RemovePlayerToken(UserControl playerToken);
    }
}
