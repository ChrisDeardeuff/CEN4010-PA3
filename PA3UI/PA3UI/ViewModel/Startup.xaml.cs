using System.Web;
using System.Windows;
using System.Windows.Controls;

namespace PA3UI.ui
{
    /// <summary>
    /// Interaction logic for Startup.xaml
    /// </summary>
    public partial class Startup : Page
    {
        private Window window;

        public Startup(Window window)
        {
            InitializeComponent();

        }

        private void Button_Click_TwoPlayers(object sender, System.Windows.RoutedEventArgs e)
        {
            //var window = HttpContext.Current.Handler as Window;
            window.Content.
            var x = new Startup(window);
        }
    }
}
