using PA3BackEnd.src.Monopoly;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PA3UI.ui
{
    /// <summary>
    /// Interaction logic for Trade.xaml
    /// </summary>
    public partial class Trade : UserControl
    {
        private int index;
        private RoutedEventHandler routedEvent;
        private RoutedEventHandler routedEventHandler;
        private PA3BackEnd.src.Monopoly.MonopolyGame monopolyGame;       
 
        private List<int> properties0;
        private List<int> properties1;
        private Rectangle cover;

        public Trade(PA3BackEnd.src.Monopoly.MonopolyGame monopolyGame ,RoutedEventHandler routedEvent)
        {
            InitializeComponent();
            this.monopolyGame = monopolyGame;


            properties0 = new List<int>();
            properties1 = new List<int>();

            this.routedEvent = routedEvent;

            cover = new Rectangle();
            cover.SetValue(Grid.RowSpanProperty, 4);
            cover.SetValue(Grid.ColumnSpanProperty, 4);
            cover.Fill = Brushes.White;
            cover.Opacity = 0.5;

            CardHolderPlayer0.Add_EventHandler(PlayerOneGives);
            CardHolderPlayer1.Add_EventHandler(PlayerTwoGives);

            for (int i = 0; i < monopolyGame.amountOfPlayers; i++)
            {
                if (i == monopolyGame.currentPlayerID -1)
                {
                    continue;
                }

                ComboBoxSelectPlayer.Items.Add($"Player {i+1} ({PA3BackEnd.src.Monopoly.MonopolyGame.GetUserTokenName(i+1)})");
            }

            CardHolderPlayer0.ClearDeeds();
            foreach (var prop in monopolyGame.GetPropertiesOwnedByPlayer())
            {
                if (monopolyGame.HasAnyBuildingsOnIt(prop))
                {
                    continue;
                }
                CardHolderPlayer0.Add_Deed(prop);
            }

            CardHolderPlayer1.ClearDeeds();

            ComboBoxSelectPlayer.SelectedIndex = 0;
            textblockmoney.Text = $"${0}";
        }

        private void Reset() 
        {
            stackPanel0.Children.Clear();
            stackPanel1.Children.Clear();

            properties0.Clear();
            properties1.Clear();

            MoneySlider.Value = 0;

        }

        private void ComboBoxSelectPlayer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Reset();
            CardHolderPlayer1.ClearDeeds();
            index = ComboBoxSelectPlayer.SelectedIndex;
            if (index >= monopolyGame.currentPlayerID - 1)
            {
                index++;
            }

            foreach (var prop in monopolyGame.GetPropertiesOwnedByPlayer(index))
            {
                if (monopolyGame.HasAnyBuildingsOnIt(prop))
                {
                    continue;
                }

                CardHolderPlayer1.Add_Deed(prop);
            }

            if (monopolyGame.GetBalanceOfPlayer(index) < 0)
            {
                MoneySlider.Minimum = 0;
            }
            else 
            {
                MoneySlider.Minimum = -1 * monopolyGame.GetBalanceOfPlayer(index);
            }

            if (monopolyGame.currentPlayerBalance < 0)
            {
                MoneySlider.Maximum = 0;
            }
            else 
            {
                MoneySlider.Maximum = monopolyGame.currentPlayerBalance;
            }

            MoneySlider.Value = 0;
        }

        private void PlayerOneGives(object sender, RoutedEventArgs args)
        {
            var prop = ((Deed)sender).id;

            if (properties0.Contains(prop))
            {
                properties0.Remove(prop);
            }
            else 
            {
                properties0.Add(prop);
            }
            RefreshStackPanel0();
        }

        private void PlayerTwoGives(object sender, RoutedEventArgs args)
        {
            var prop = ((Deed)sender).id;

            if (properties1.Contains(prop))
            {
                properties1.Remove(prop);
            }
            else
            {
                properties1.Add(prop);
            }
            RefreshStackPanel1();
        }

        private void RefreshStackPanel0()
        {
            stackPanel0.Children.Clear();
            foreach (var prop in properties0)
            {
                var textBlock = new TextBlock();
                textBlock.Text = monopolyGame.GetNameOfProperty(prop);
                textBlock.FontSize= 20;
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;

                stackPanel0.Children.Add(textBlock);
            }
        }

        private void RefreshStackPanel1()
        {
            stackPanel1.Children.Clear();
            foreach (var prop in properties1)
            {
                var textBlock = new TextBlock();
                textBlock.Text = monopolyGame.GetNameOfProperty(prop);
                textBlock.FontSize = 20;
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;

                stackPanel1.Children.Add(textBlock);
            }
        }

        private void MoneySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            textblockmoney.Text = $"${(int)e.NewValue}";
        }

        private void Button_CLick_Cancel(object sender, RoutedEventArgs args)
        {
            routedEvent.Invoke(this, null);
        }

        private void Button_Click_Propose(object sender, RoutedEventArgs e)
        {
            ShowDialogBoxYesNo($"Player {index + 1} ({PA3BackEnd.src.Monopoly.MonopolyGame.GetUserTokenName(index)}) do you accept the proposed Trade", (object s, RoutedEventArgs args) =>
            {
                var dialog = (Dialog)s;
                if (dialog.yes)
                {
                    CompleteTrade();
                    Button_CLick_Cancel(null, null);
                }
                else 
                {
                    ShowDialogBoxOK("The proposed Trade was rejected", null);
                }
            });
        }

        private void ShowDialogBox()
        {
            try
            {
                mainGrid.Children.Add(cover);
            }
            catch
            {
                Debug.Fail("Adding the cover failed!");
            }
        }

        private void ShowDialogBoxYesNo(string msg, RoutedEventHandler routedEvent)
        {
            ShowDialogBox();
            routedEventHandler = routedEvent;
            var dialogBox = Dialog.ShowYesNoDialog(msg, RemoveDialogBox);
            dialogBox.SetValue(Grid.ColumnSpanProperty, 4);
            dialogBox.SetValue(Grid.RowSpanProperty, 4);
            mainGrid.Children.Add(dialogBox);
        }

        private void RemoveDialogBox(object sender, RoutedEventArgs args)
        {
            mainGrid.Children.Remove(cover);
            mainGrid.Children.Remove((UIElement)sender);
            if (routedEventHandler != null)
            {
                var tempEvent = routedEventHandler;
                routedEventHandler = null;
                tempEvent.Invoke(sender, args);
            }
        }

        private void ShowDialogBoxOK(string msg, RoutedEventHandler routedEvent)
        {
            ShowDialogBox();
            routedEventHandler = routedEvent;
            var dialogBox = Dialog.ShowOKDialog(msg, RemoveDialogBox);
            dialogBox.SetValue(Grid.ColumnSpanProperty, 4);
            dialogBox.SetValue(Grid.RowSpanProperty, 4);
            mainGrid.Children.Add(dialogBox);
        }

        private void CompleteTrade() 
        {
            monopolyGame.CompleteTrade(properties0, properties1, (int)MoneySlider.Value, monopolyGame.currentPlayerID-1, index);
        }
    }
}
