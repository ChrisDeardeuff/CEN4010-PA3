using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PA3UI.ui
{
    /// <summary>
    /// Interaction logic for UserToken.xaml
    /// </summary>
    public partial class UserToken : UserControl
    {
        public UserToken(string texture)
        {
            InitializeComponent();
            tokenTexture.Source = new BitmapImage(new Uri(texture, UriKind.RelativeOrAbsolute));
        }

        public void ScaleRelativeToBoardSize(int boardHeight)
        { 
            //not yet implemented
        }
    }
}
