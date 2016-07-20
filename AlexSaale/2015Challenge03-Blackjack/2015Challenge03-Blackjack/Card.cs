using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2015Challenge03_Blackjack {
    public enum Suit { Hearts, Diamonds, Clubs, Spades }
    public enum Face { Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }

    public class Card {
        public Suit Suit { get; }
        public Face Face { get; }
        public int BlackjackValue { get; }
        public int PokerValue { get; }

        public Card(Face face, Suit suit) {
            Face = face;
            Suit = suit;
            switch (Face) {
                case Face.Two:
                    BlackjackValue = 2;
                    PokerValue = 2;
                    break;
                case Face.Three:
                    BlackjackValue = 3;
                    PokerValue = 3;
                    break;
                case Face.Four:
                    BlackjackValue = 4;
                    PokerValue = 4;
                    break;
                case Face.Five:
                    BlackjackValue = 5;
                    PokerValue = 5;
                    break;
                case Face.Six:
                    BlackjackValue = 6;
                    PokerValue = 6;
                    break;
                case Face.Seven:
                    BlackjackValue = 7;
                    PokerValue = 7;
                    break;
                case Face.Eight:
                    BlackjackValue = 8;
                    PokerValue = 8;
                    break;
                case Face.Nine:
                    BlackjackValue = 9;
                    PokerValue = 9;
                    break;
                case Face.Ten:
                    BlackjackValue = 10;
                    PokerValue = 10;
                    break;
                case Face.Jack:
                    BlackjackValue = 10;
                    PokerValue = 11;
                    break;
                case Face.Queen:
                    BlackjackValue = 10;
                    PokerValue = 12;
                    break;
                case Face.King:
                    BlackjackValue = 10;
                    PokerValue = 13;
                    break;
                case Face.Ace:
                    BlackjackValue = 11;
                    PokerValue = 14;
                    break;
            }
        }

        public override string ToString() {
            return Face + " of " + Suit;
        }
    }
}
