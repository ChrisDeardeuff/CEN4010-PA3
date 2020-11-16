﻿using System.Windows.Controls;
using PA3BackEnd.src.Monopoly;

namespace PA3UI.ViewModel
{
    public partial class EndGame : UserControl
    {
        public EndGame(string playerName)
        {
            InitializeComponent();
            Winner.Text = $"The winner is {playerName}!";
        }

    }
}