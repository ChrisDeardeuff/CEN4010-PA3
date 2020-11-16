using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PA3UI.ui
{
    public partial class Deed : UserControl
    {
        RoutedEventHandler eHandler;
        public int id { private set; get; }

        public Deed(int id)
        {
            InitializeComponent();
            DeedImage.Source = new BitmapImage(new Uri($"/Assets/Deeds/{id}.png", UriKind.RelativeOrAbsolute));
            this.id = id;

        }

        public Deed()
        {
            InitializeComponent();
        }

        public void AddEvent_Handler(RoutedEventHandler eHandler)
        {
            this.eHandler = eHandler;
        }

        private void viewDeed_OnClick(object sender, RoutedEventArgs e)
        {
            if (eHandler == null)
            {
                return;
            }
            eHandler.Invoke(this, null);
        }
    }
}