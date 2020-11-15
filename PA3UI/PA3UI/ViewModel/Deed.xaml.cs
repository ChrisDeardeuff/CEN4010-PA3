using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PA3UI.ui
{
    public partial class Deed : UserControl
    {
        public Deed(int id)
        {
            InitializeComponent();
            DeedImage.Source = new BitmapImage(new Uri($"/Assets/Deeds/{id}.png", UriKind.RelativeOrAbsolute));
            
        }

        public Deed()
        {
            InitializeComponent();
        }

        public void AddEvent_Handler(RoutedEventHandler eHandler)
        {
            DeedButton.Click += eHandler;
        }

        private void viewDeed_OnClick(object sender, RoutedEventArgs e)
        {
        }
    }
}