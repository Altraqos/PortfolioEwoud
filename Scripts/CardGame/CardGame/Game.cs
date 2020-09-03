using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    class Game
    {
        /*
        public static Dictionary<int, string> cardNames;
        public static Dictionary<int, string> cardTypes;
        public static Dictionary<int, string> cardSpecial;
        public static Dictionary<int, string> players;

        public static List<int> player1Cards;
        public static List<int> player2Cards;
        public static List<int> player3Cards;
        public static List<int> player4Cards;
        public static List<CardClass> cardClasses;


    */

        public static bool isPlaying = true;

        public static void StartGame()
        {
            foreach (int x in Program.players.Keys)
            {
                ShowCards(x);
            }
        }

        public static void ShowCards(int playerID)
        {
            Console.ForegroundColor = Program.playerColors[playerID];
            Console.WriteLine(Program.players[playerID] + "'s turn!");
            Console.ForegroundColor = Program.playerColors[0];
            /*
            switch (playerID)
            {
                case 1:
                    foreach (int x in Program.players[1])
                        //Initialize_Game.SlowlyWrite(Program.players[playerID] + " has the: " + Program.cardClasses[x].cardName + " of " + Program.cardClasses[x].cardType + "s in their hand.");
                    break;
                case 2:
                    foreach (int x in Program.players[2])
                        //Initialize_Game.SlowlyWrite(Program.players[playerID] + " has the: " + Program.cardClasses[x].cardName + " of " + Program.cardClasses[x].cardType + "s in their hand.");
                    break;
                case 3:
                    foreach (int x in Program.players[3])
                        //Initialize_Game.SlowlyWrite(Program.players[playerID] + " has the: " + Program.cardClasses[x].cardName + " of " + Program.cardClasses[x].cardType + "s in their hand.");
                    break;
                case 4:
                    foreach (int x in Program.players[4])
                        //Initialize_Game.SlowlyWrite(Program.players[playerID] + " has the: " + Program.cardClasses[x].cardName + " of " + Program.cardClasses[x].cardType + "s in their hand.");
                    break;
                default:
                    return;
                    
            }
            */
        }

        public static void playCards(int playerID)
        {

        }
    }
}
