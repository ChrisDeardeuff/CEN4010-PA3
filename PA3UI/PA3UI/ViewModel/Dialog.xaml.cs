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
        public int amount { get { return (int)moneySlider.Value; } }


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
            moneySlider.Minimum = min;
            moneySlider.Maximum = max;
            moneySlider.ValueChanged += MoneySlider_ValueChanged;
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

        public static Dialog ShowBidingDialog(string msg, int min, int max, RoutedEventHandler onClick)
        {
            var dialog = new Dialog(msg, min, max);
            dialog.okButton.Click += dialog.OnButtonClickYes;
            dialog.okButton.Content = "Yes";
            dialog.routedEvent = onClick;
            dialog.noButton.Click += dialog.OnButtonClickNo;
            dialog.moneySlider.Visibility = Visibility.Visible;
            return dialog;

        }

        private void MoneySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetTextOfTextBlockAmount($"${(int)e.NewValue}");
        }

        private void SetTextOfTextBlockAmount(string text) 
        {
            textBlockAmount.Text = text;
        }
    }
}
