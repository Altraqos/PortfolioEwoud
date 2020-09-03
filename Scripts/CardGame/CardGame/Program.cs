using System;
using System.Collections.Generic;

namespace CardGame
{
    class Program
    {
        public static Dictionary<int, string> cardNames;
        public static Dictionary<int, string> cardTypes;
        public static Dictionary<int, string> cardSpecial;
        public static Dictionary<int, string> players;

        /*
        public static List<int> player1Cards;
        public static List<int> player2Cards;
        public static List<int> player3Cards;
        public static List<int> player4Cards;
        */
        public static List<CardClass> cardClasses;
        public static List<Player> playerClasses;

        public static List<ConsoleColor> playerColors;

        static void Main(string[] args)
        {
            Initialize_Game.SetInitialReferences();
            Initialize_Game.InstantiateCards();
        }
    }
}