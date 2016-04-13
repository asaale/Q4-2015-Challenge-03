using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Data
{
    public static class DeckFactory
    {
        public static Deck CreatePokerDeck()
        {
            Deck deck = new PokerDeck(new BasicDeck());
            deck.Cards = deck.InitializeCards();
            return deck;
        }

        public static Deck CreateBasicDeck()
        {
            Deck deck = new BasicDeck();
            deck.Cards = deck.InitializeCards();
            return deck;
        }
    }
}
