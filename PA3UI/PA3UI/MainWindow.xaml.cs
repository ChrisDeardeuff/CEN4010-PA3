using PA3UI.ui;
using System.Windows;

namespace PA3UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Window window;

        public MainWindow()
        {
            InitializeComponent();
            window = this;
            ChangeUserControl(new Startup(ChangeUserControl));
        }
        private void ChangeUserControl(System.Windows.Controls.UserControl newPage)
        {
            mainGrid.Children.Clear();
            mainGrid.Children.Add(newPage);
        }

        public static void MaximizeWindow()
        {
            window.WindowState = WindowState.Maximized;
        }

        public static void CloseWindow() 
        {
            window.Close();
        }
    }
}
