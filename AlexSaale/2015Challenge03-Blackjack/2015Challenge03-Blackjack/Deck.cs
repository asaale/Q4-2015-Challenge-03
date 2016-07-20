using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2015Challenge03_Blackjack {
    public class Deck {
        private List<Card> Cards { get; set; }

        public Deck() {
            Cards = CreateNewDeck();
        }

        public void Shuffle() {
            var rng = new Random();
            int current = Cards.Count;

            while (current > 1) {
                current--;
                int randomLoc = rng.Next(current + 1);
                Card randomCard = Cards[randomLoc];
                Cards[randomLoc] = Cards[current];
                Cards[current] = randomCard;
            }
        }

        public List<Card> CreateNewDeck() {
            var cards = new List<Card>() {
                new Card(Face.Two, Suit.Clubs),
                new Card(Face.Two, Suit.Hearts),
                new Card(Face.Two, Suit.Spades),
                new Card(Face.Two, Suit.Diamonds),
                new Card(Face.Three, Suit.Clubs),
                new Card(Face.Three, Suit.Hearts),
                new Card(Face.Three, Suit.Spades),
                new Card(Face.Three, Suit.Diamonds),
                new Card(Face.Four, Suit.Clubs),
                new Card(Face.Four, Suit.Hearts),
                new Card(Face.Four, Suit.Spades),
                new Card(Face.Four, Suit.Diamonds),
                new Card(Face.Five, Suit.Clubs),
                new Card(Face.Five, Suit.Hearts),
                new Card(Face.Five, Suit.Spades),
                new Card(Face.Five, Suit.Diamonds),
                new Card(Face.Six, Suit.Clubs),
                new Card(Face.Six, Suit.Hearts),
                new Card(Face.Six, Suit.Spades),
                new Card(Face.Six, Suit.Diamonds),
                new Card(Face.Seven, Suit.Clubs),
                new Card(Face.Seven, Suit.Hearts),
                new Card(Face.Seven, Suit.Spades),
                new Card(Face.Seven, Suit.Diamonds),
                new Card(Face.Eight, Suit.Clubs),
                new Card(Face.Eight, Suit.Hearts),
                new Card(Face.Eight, Suit.Spades),
                new Card(Face.Eight, Suit.Diamonds),
                new Card(Face.Nine, Suit.Clubs),
                new Card(Face.Nine, Suit.Hearts),
                new Card(Face.Nine, Suit.Spades),
                new Card(Face.Nine, Suit.Diamonds),
                new Card(Face.Ten, Suit.Clubs),
                new Card(Face.Ten, Suit.Hearts),
                new Card(Face.Ten, Suit.Spades),
                new Card(Face.Ten, Suit.Diamonds),
                new Card(Face.Jack, Suit.Clubs),
                new Card(Face.Jack, Suit.Hearts),
                new Card(Face.Jack, Suit.Spades),
                new Card(Face.Jack, Suit.Diamonds),
                new Card(Face.Queen, Suit.Clubs),
                new Card(Face.Queen, Suit.Hearts),
                new Card(Face.Queen, Suit.Spades),
                new Card(Face.Queen, Suit.Diamonds),
                new Card(Face.King, Suit.Clubs),
                new Card(Face.King, Suit.Hearts),
                new Card(Face.King, Suit.Spades),
                new Card(Face.King, Suit.Diamonds),
                new Card(Face.Ace, Suit.Clubs),
                new Card(Face.Ace, Suit.Hearts),
                new Card(Face.Ace, Suit.Spades),
                new Card(Face.Ace, Suit.Diamonds),
            };
            return cards;
        }

        public Card DealCard() {
            var card = Cards.First();
            Cards.Remove(card);
            return card;
        }
    }
}
