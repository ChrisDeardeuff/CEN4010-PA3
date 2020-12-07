using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA3BackEnd.src.Monopoly
{
    public class MonopolyGame 
    {
        private int currentsPlayerTurn { get; set; }
        public int Round { get; private set; }
        private Player[] players;
        private Fields fields;
        private List<int[]> roles;
        private int[][] outstandingDevelopment;
        private Player currentPlayer { get { return players[currentsPlayerTurn]; } }
        

        public bool currentPlayerInPrison { get { return currentPlayer.inPrison; } }
        public int currentPlayerID { get { return currentsPlayerTurn + 1; } }
        public string currentPlayerTokenName { get { return GetUserTokenName(currentsPlayerTurn); } }

        public MonopolyGame(int Amountplayers)
        {
            roles = new List<int[]>();
            Round = 0;
            currentsPlayerTurn = -1;

            players = new Player[Amountplayers];

            for (int i = 0; i < Amountplayers; i++)
            {
                players[i] = new Player();
            }

            fields = new Fields();
        }

        public void NextPlayersTurn()
        {

            roles.Clear();
            currentsPlayerTurn++;
            currentsPlayerTurn %= players.Length;

            if (currentsPlayerTurn == 0)
            {
                Round++;
            }
        }

        public static string GetUserTokenName(int id)
        {
            switch (id)
            {
                case 0:
                    return "Shoe";
                case 1:
                    return "Thimble";
                case 2:
                    return "Car";
                case 3:
                    return "TopHat";
            }
            return "";
        }
    }
}
