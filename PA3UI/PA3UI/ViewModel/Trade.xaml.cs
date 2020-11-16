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
        private Player[] players;
        private int playerId;
        private int index;
        private RoutedEventHandler routedEvent;
        private RoutedEventHandler routedEventHandler;
        private List<Property> properties0;
        private List<Property> properties1;
        private Fields fields;
        private Rectangle cover;

        public Trade(Player[] players, int playerId, Fields fields ,RoutedEventHandler routedEvent)
        {
            InitializeComponent();
            properties0 = new List<Property>();
            properties1 = new List<Property>();

            this.routedEvent = routedEvent;
            this.players = players;
            this.fields = fields;
            this.playerId = playerId;

            cover = new Rectangle();
            cover.SetValue(Grid.RowSpanProperty, 4);
            cover.SetValue(Grid.ColumnSpanProperty, 4);
            cover.Fill = Brushes.White;
            cover.Opacity = 0.5;

            CardHolderPlayer0.Add_EventHandler(PlayerOneGives);
            CardHolderPlayer1.Add_EventHandler(PlayerTwoGives);

            for (int i = 0; i < players.Length; i++)
            {
                if (i == this.playerId)
                {
                    continue;
                }

                ComboBoxSelectPlayer.Items.Add($"Player {i+1} ({MonopolyGame.GetUserTokenName(i)})");
            }

            CardHolderPlayer0.ClearDeeds();
            foreach (var prop in (List<Property>) players[playerId].getPropertiesOwned())
            {
                if (prop.group.HasAnyBuildings())
                {
                    continue;
                }
                CardHolderPlayer0.Add_Deed(prop.GetLocation());
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
            if (index >= playerId)
            {
                index++;
            }

            foreach (var prop in (List<Property>)players[index].getPropertiesOwned())
            {
                if (prop.group.HasAnyBuildings())
                {
                    continue;
                }

                CardHolderPlayer1.Add_Deed(prop.GetLocation());
            }

            MoneySlider.Minimum = -1 * players[index].balance;
            MoneySlider.Maximum = players[playerId].balance;
        }

        private void PlayerOneGives(object sender, RoutedEventArgs args)
        {
            var prop = (Property)fields.GetFieldAt(((Deed)sender).id);

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
            var prop = (Property)fields.GetFieldAt(((Deed)sender).id);

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
                textBlock.Text = prop.name;
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
                textBlock.Text = prop.name;
                textBlock.FontSize = 10;

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
            ShowDialogBoxYesNo($"Player {index + 1} ({MonopolyGame.GetUserTokenName(index)}) do you accept the proposed Trade", (object s, RoutedEventArgs args) =>
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
            foreach (var prop in properties0) 
            {
                players[playerId].removeProperty(prop);
                players[index].addProperty(prop);
            }

            foreach (var prop in properties1)
            {
                players[index].removeProperty(prop);
                players[playerId].addProperty(prop);
            }

            players[playerId].subtractBalance((int)MoneySlider.Value);
            players[index].addBalance((int)MoneySlider.Value);
        }
    }
}
