using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Data
{
    /// <summary>
    /// The BasicDeck has only the 52 basic cards in a poker deck.  No Jokers.
    /// </summary>
    public class BasicDeck : Deck
    {
        /// <summary>
        /// This constructor, though public, is not intended to be called by client code.  It is public
        /// to allow implementation of the decorator pattern when constructing a new deck.
        /// </summary>
        public BasicDeck()
        {
        }

        public override int FullDeckSize
        {
            get  { return 52; }
            
        }

        public override List<ICard> InitializeCards()
        {
            var cards = new List<ICard>();

            foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)).Cast<CardSuit>().Where(s => s != CardSuit.JokerSuit))
            {
                foreach (CardName name in Enum.GetValues(typeof(CardName)).Cast<CardName>().Where(v => v >= CardName.Ace && v <= CardName.King))
                {
                    cards.Add(new Card() { Suit = suit, Name = name });
                }
            }

            return cards;
        }
    }
}
