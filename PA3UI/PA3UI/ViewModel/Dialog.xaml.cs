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
        private int min;
        private int max;


        private RoutedEventHandler routedEvent;
        private Dialog(string msg)
        {
            InitializeComponent();
            TextBlockMessage.Text = msg;
        }

        private Dialog(string msg, int min, int max)
        {
            InitializeComponent();
            TextBlockMessage.Text = msg;
            amount = min;
            this.min = min;
            this.max = max;
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            routedEvent.Invoke(this, e);
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

        public static Dialog ShowOKDialog(string msg, RoutedEventHandler onClick)
        {
            var dialog = new Dialog(msg);
            dialog.okButton.Click += dialog.OnButtonClick;
            dialog.routedEvent = onClick;
            dialog.noButton.IsEnabled = false;
            dialog.noButton.Visibility = Visibility.Hidden;
            return dialog;
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

        private void OnButtonClickPlus(object sender, RoutedEventArgs e)
        {
            yes = true;
            amount += 10;
        }

        private void OnButtonClickMinus(object sender, RoutedEventArgs e)
        {
            yes = false;
            amount -= 10;
        }

        public static Dialog ShowBidingDialog(string msg, int min, int max, RoutedEventHandler onClick)
        {
            var dialog = new Dialog(msg, min, max);
            dialog.okButton.Click += dialog.OnButtonClickPlus;
            dialog.okButton.Content = "+";
            dialog.routedEvent = onClick;
            dialog.noButton.Click += dialog.OnButtonClickMinus;
            dialog.noButton.Content = "-";
            return dialog;

        }
    }
}
