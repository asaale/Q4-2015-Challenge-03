using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Data
{
    class NullCard : ICard
    {
        public CardSuit Suit { get; set; }

        public CardName Name { get; set; }

        public int Value
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
