using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    class Player
    {
        public List<int> playerCardsInHand = new List<int>();
        public string Name;

        public void addCards(int cardToAdd)
        {
            playerCardsInHand.Add(cardToAdd);
            Console.WriteLine(cardToAdd);
        }
    }
}
