using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Data
{
    public class Card : ICard
    {
        private readonly List<CardName> _variableValueCards = new List<CardName>();

        public CardName Name { get; set; }

        public CardSuit Suit { get; set; }

        private int _value = 0;

        /// <summary>
        /// The integer value of the card.  Some cards have fixed values, others have variable values.
        /// </summary>
        public int Value
        {
            get
            {
                if (_variableValueCards.Contains(Name))
                {
                    return _value;
                }
                else
                {
                    // Face cards
                    if ("JackQueenKing".Contains(Name.ToString()))
                    {
                        return 10;
                    }

                    // all other cards
                    return Convert.ToInt32(Name);
                }
            }

            set
            {
                if (_variableValueCards.Contains(Name))
                {
                    _value = value;
                }
            }
        }

        public Card()
        {
            _variableValueCards.AddRange(new CardName[] {
                    CardName.NotSet,
                    CardName.Ace,
                    CardName.Joker
                });
        }
    }
}
