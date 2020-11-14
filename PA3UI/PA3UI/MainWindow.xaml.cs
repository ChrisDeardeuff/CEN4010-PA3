using PA3UI.ui;
using System.Windows;

namespace PA3UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ChangeUserControl(new Startup(ChangeUserControl));
        }
        private void ChangeUserControl(System.Windows.Controls.UserControl newPage)
        {
            mainGrid.Children.Clear();
            mainGrid.Children.Add(newPage);
        }
    }
}
