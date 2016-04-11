using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Data
{
    /// <summary>
    /// A poker deck has two Joker cards in addition to the standard 52 suit cards.
    /// </summary>
    public class PokerDeck : DeckDecorator
    {
        public override int FullDeckSize
        {
            get { return 54; }
        }

        /// <summary>
        /// This constructor, though public, is not intended to be called by client code.  It is public
        /// to allow implementation of the decorator pattern when constructing a new deck.
        /// </summary>
        /// <param name="deck"></param>
        public PokerDeck(Deck deck)
        {
            _baseDeck = deck;
        }

        public override List<ICard> InitializeCards()
        {
            var cards = _baseDeck.InitializeCards();

            // Add two Jokers to the deck
            cards.Add(new Card() { Name = CardName.Joker, Suit = CardSuit.JokerSuit });
            cards.Add(new Card() { Name = CardName.Joker, Suit = CardSuit.JokerSuit });

            return cards;
        }
    }
}
