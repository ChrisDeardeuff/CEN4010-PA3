using System.Windows.Controls;

namespace PA3UI.ui
{
    public partial class CardViewer : UserControl
    {
        public Deed deed { private set; get; }
        public CardViewer()
        {
            InitializeComponent();
        }

        public void SetDeed(Deed deed)
        {
            CLear();
            this.deed = deed;

            mainGrid.Children.Add(deed);
        }

        public void CLear() 
        {
            if (this.deed != null)
            {
                mainGrid.Children.Remove(this.deed);
                this.deed = null;
            }
        }
    }
}