using System.Windows;
using System.Windows.Controls;

namespace PA3UI.ui
{
    /// <summary>
    /// Interaction logic for Board.xaml
    /// </summary>
    public partial class Board : UserControl
    {
        public Board()
        {
            InitializeComponent();

            //this += Board_SizeChanged;
        }

        private void Board_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            rowDefinition1.Height = new GridLength(e.NewSize.Height);
            columDefinition1.Width = new GridLength(e.NewSize.Width);
        }
    }
}
