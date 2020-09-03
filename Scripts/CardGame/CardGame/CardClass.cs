using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    class CardClass
    {
        public int cardVal;
        public string cardType;
        public string cardName;
        public int cardCount;
        public string playerOwner;

        public CardClass(int cardValClass, string cardTypeClass, string cardNameClass, int cardCountClass, string playerOwnerClass)
        {
            cardVal = cardValClass;
            cardType = cardTypeClass;
            cardName = cardNameClass;
            cardCount = cardCountClass;
            playerOwner = playerOwnerClass;
        }
    }
}
