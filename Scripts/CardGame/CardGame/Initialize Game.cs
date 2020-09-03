using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CardGame
{
    class Initialize_Game
    {
        public static void SetInitialReferences()
        {
            //Program.player1Cards = new List<int>();
            //Program.player2Cards = new List<int>();
            //Program.player3Cards = new List<int>();
            //Program.player4Cards = new List<int>();
            Program.cardClasses = new List<CardClass>();
            Program.playerClasses = new List<Player>();
            Program.cardNames = new Dictionary<int, string>();
            Program.cardNames.Add(1, "Ace");
            Program.cardNames.Add(2, "Two");
            Program.cardNames.Add(3, "Three");
            Program.cardNames.Add(4, "Four");
            Program.cardNames.Add(5, "Five");
            Program.cardNames.Add(6, "Six");
            Program.cardNames.Add(7, "Seven");
            Program.cardNames.Add(8, "Eight");
            Program.cardNames.Add(9, "Nine");
            Program.cardNames.Add(10, "Ten");
            Program.cardNames.Add(11, "Jack");
            Program.cardNames.Add(12, "Lady");
            Program.cardNames.Add(13, "Lord");
            Program.cardTypes = new Dictionary<int, string>();
            Program.cardTypes.Add(1, "Spade");
            Program.cardTypes.Add(2, "Heart");
            Program.cardTypes.Add(3, "Diamond");
            Program.cardTypes.Add(4, "Club");
            Program.cardTypes.Add(5, "Special");
            Program.cardSpecial = new Dictionary<int, string>();
            Program.cardSpecial.Add(1, "Joker");
            Program.players = new Dictionary<int, string>();
            Program.players.Add(0, "EDI");
            Program.players.Add(1, "Xivtraqos");
            Program.players.Add(2, "Lithdrae");
            Program.players.Add(3, "Altecarabia");
            Program.players.Add(4, "Mysteriahn");
            Program.playerColors = new List<ConsoleColor>();
            Program.playerColors.Add(ConsoleColor.Gray);
            Program.playerColors.Add(ConsoleColor.Red);
            Program.playerColors.Add(ConsoleColor.Cyan);
            Program.playerColors.Add(ConsoleColor.Yellow);
            Program.playerColors.Add(ConsoleColor.Green);

            Console.ForegroundColor = Program.playerColors[0];
        }

        public static void PlayerInitialize()
        {
            for (int i = 0; i < Program.players.Count; i++)
            {
                Program.playerClasses.Add(new Player());
                Program.playerClasses[i].Name = Program.players[i];
                Console.WriteLine(Program.playerClasses[i].Name);
            }
            DealCards();
        }

        public static void DealCards()
        {
            for (int p = 0; p < Program.players.Count; p++)
            {
                Player player = Program.playerClasses[p];
                for (int i = 0; i < 7; i++)
                {
                top:
                    Random r = new Random();
                    int randomVal = r.Next(1, 54);

                    if (!Program.playerClasses[p].playerCardsInHand.Contains(randomVal))
                    {
                        player.addCards(randomVal);
                    }
                    else
                    goto top;
                }
                for (int k = 0; k < Program.players.Count; k++)
                {
                    player.playerCardsInHand.Sort();
                    Console.WriteLine("----------------------------------");
                    for (int l = 0; l < player.playerCardsInHand.Count; l++)
                    {
                        Console.WriteLine(player.Name + " got the card with ID:" + player.playerCardsInHand[l]);
                    }
                }
            }

            Game.StartGame();
        }


        public static void InstantiateCards()
        {
            // Ace = 1
            // 2 = 2
            // 3 = 3
            // 4 = 4
            // 5 = 5
            // 6 = 6
            // 7 = 7
            // 8 = 9
            // 9 = 9
            // 10 = 10
            // Jack = 11
            // Lady = 12
            // Lord = 13
            // Joker = 14
            int cardCount = 0;
            for (int j = 1; j <= 4; j++)
            {
                for (int i = 1; i <= 13; i++)
                {
                    cardCount++;
                    Program.cardClasses.Add(new CardClass(i, Program.cardTypes[j], Program.cardNames[i], cardCount, null));
                }
            }
            for (int i = 0; i < 2; i++)
            {
                cardCount++;
                Program.cardClasses.Add(new CardClass(13, Program.cardTypes[5], Program.cardSpecial[1], cardCount, null));
            }
            PlayerInitialize();
        }

        public static void SlowlyWrite(string SlowlyType)
        {
            if (string.IsNullOrEmpty(SlowlyType))
                return;
            try
            {
                SlowlyType = char.ToUpper(SlowlyType[0]) + SlowlyType.Substring(1);
            }
            catch
            {
                return;
            }
            char last = SlowlyType[SlowlyType.Length - 1];
            foreach (char c in SlowlyType)
            {
                Console.Write(c);
                Thread.Sleep(7);
            }
            if (last.ToString() != "." && last.ToString() != "!" && last.ToString() != "?" && last.ToString() != ":" && last.ToString() != ";" && last.ToString() != "," && last.ToString() != " " && last.ToString() != "~")
                Console.Write(".");
            Console.WriteLine();
        }
    }
}
