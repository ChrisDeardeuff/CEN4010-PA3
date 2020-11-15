using System.Windows;
using System.Windows.Controls;

namespace PA3UI.ui
{
    /// <summary>
    /// Interaction logic for DialogOK.xaml
    /// </summary>
    public partial class Dialog : UserControl
    {
        public bool yes { private set; get; }
        public int amount { private set; get; }
        private RoutedEventHandler routedEvent;
        private Dialog(string msg)
        {
            InitializeComponent();
            TextBlockMessage.Text = msg;
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            routedEvent.Invoke(this, e);
        }

        public static Dialog ShowOKDialog(string msg, RoutedEventHandler onClick)
        {
            var dialog = new Dialog(msg);
            dialog.okButton.Click += dialog.OnButtonClick;
            dialog.routedEvent = onClick;
            dialog.noButton.IsEnabled = false;
            dialog.noButton.Visibility = Visibility.Hidden;
            return dialog;
        }

        private void OnButtonClickYes(object sender, RoutedEventArgs e)
        {
            yes = true;
            routedEvent.Invoke(this, e);
        }

        private void OnButtonClickNo(object sender, RoutedEventArgs e)
        {
            yes = false;
            routedEvent.Invoke(this, e);
        }

        public static Dialog ShowYesNoDialog(string msg, RoutedEventHandler onClick)
        {
            var dialog = new Dialog(msg);
            dialog.okButton.Click += dialog.OnButtonClickYes;
            dialog.okButton.Content = "Yes";
            dialog.routedEvent = onClick;
            dialog.noButton.Click += dialog.OnButtonClickNo;
            return dialog;
        }
    }
}
