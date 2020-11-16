using PA3UI.ui;
using System.Windows;
using System.Windows.Controls;

namespace PA3UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Window window;
        private static Grid grid;

        public MainWindow()
        {
            InitializeComponent();
            window = this;
            grid = mainGrid;
            ChangeUserControl(new Startup());
        }

        public static void ChangeUserControl(UserControl newPage)
        {
            grid.Children.Clear();
            grid.Children.Add(newPage);
        }

        public static void AddUserControl(UserControl newPage) 
        {
            grid.Children.Add(newPage);
        }

        public static void RemoveUserControl(UserControl newPage)
        {
            grid.Children.Remove(newPage);
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
