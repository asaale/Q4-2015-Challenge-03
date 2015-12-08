using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace BlackJack
{
    public class PokerBase
    {
        public List<Card> deck = new List<Card>();
        public Random random = new Random();
        public List<Card> hand = new List<Card>();
        public List<Card> dealerHand = new List<Card>();
        public int playerTotal = 0;
        public int dealerTotal = 0;

        public void ModifyDeckForPoker()
        {
            Card cardAH = deck.Where(t => t.Abbreviation == "AH").FirstOrDefault();
            cardAH.Value = 1;
            Card cardAS = deck.Where(t => t.Abbreviation == "AS").FirstOrDefault();
            cardAS.Value = 1;
            Card cardAD = deck.Where(t => t.Abbreviation == "AD").FirstOrDefault();
            cardAD.Value = 1;
            Card cardAC = deck.Where(t => t.Abbreviation == "AC").FirstOrDefault();
            cardAC.Value = 1;

            Card cardKH = deck.Where(t => t.Abbreviation == "KH").FirstOrDefault();
            cardKH.Value = 13;
            Card cardKS = deck.Where(t => t.Abbreviation == "KS").FirstOrDefault();
            cardKS.Value = 13;
            Card cardKD = deck.Where(t => t.Abbreviation == "KD").FirstOrDefault();
            cardKD.Value = 13;
            Card cardKC = deck.Where(t => t.Abbreviation == "KC").FirstOrDefault();
            cardKC.Value = 13;

            Card cardQH = deck.Where(t => t.Abbreviation == "QH").FirstOrDefault();
            cardQH.Value = 12;
            Card cardQS = deck.Where(t => t.Abbreviation == "QS").FirstOrDefault();
            cardQS.Value = 12;
            Card cardQD = deck.Where(t => t.Abbreviation == "QD").FirstOrDefault();
            cardQD.Value = 12;
            Card cardQC = deck.Where(t => t.Abbreviation == "QC").FirstOrDefault();
            cardQC.Value = 12;

            Card cardJH = deck.Where(t => t.Abbreviation == "JH").FirstOrDefault();
            cardJH.Value = 11;
            Card cardJS = deck.Where(t => t.Abbreviation == "JS").FirstOrDefault();
            cardJS.Value = 11;
            Card cardJD = deck.Where(t => t.Abbreviation == "JD").FirstOrDefault();
            cardJD.Value = 11;
            Card cardJC = deck.Where(t => t.Abbreviation == "JC").FirstOrDefault();
            cardJC.Value = 11;
        }

        public void CreateDeck()
        {
            deck.Clear();

            deck.Add(new Card { CardName = "Ace of Hearts", Abbreviation = "AH", Value = 11, Suit = "Hearts" });
            deck.Add(new Card { CardName = "King of Hearts", Abbreviation = "KH", Value = 10, Suit = "Hearts" });
            deck.Add(new Card { CardName = "Queen of Hearts", Abbreviation = "QH", Value = 10, Suit = "Hearts" });
            deck.Add(new Card { CardName = "Jack of Hearts", Abbreviation = "JH", Value = 10, Suit = "Hearts" });
            deck.Add(new Card { CardName = "Ten of Hearts", Abbreviation = "10H", Value = 10, Suit = "Hearts" });
            deck.Add(new Card { CardName = "Nine of Hearts", Abbreviation = "9H", Value = 9, Suit = "Hearts" });
            deck.Add(new Card { CardName = "Eight of Hearts", Abbreviation = "8H", Value = 8, Suit = "Hearts" });
            deck.Add(new Card { CardName = "Seven of Hearts", Abbreviation = "7H", Value = 7, Suit = "Hearts" });
            deck.Add(new Card { CardName = "Six of Hearts", Abbreviation = "6H", Value = 6, Suit = "Hearts" });
            deck.Add(new Card { CardName = "Five of Hearts", Abbreviation = "5H", Value = 5, Suit = "Hearts" });
            deck.Add(new Card { CardName = "Four of Hearts", Abbreviation = "4H", Value = 4, Suit = "Hearts" });
            deck.Add(new Card { CardName = "Three of Hearts", Abbreviation = "3H", Value = 3, Suit = "Hearts" });
            deck.Add(new Card { CardName = "Two of Hearts", Abbreviation = "2H", Value = 2, Suit = "Hearts" });

            deck.Add(new Card { CardName = "Ace of Clubs", Abbreviation = "AC", Value = 11, Suit = "Clubs" });
            deck.Add(new Card { CardName = "King of Clubs", Abbreviation = "KC", Value = 10, Suit = "Clubs" });
            deck.Add(new Card { CardName = "Queen of Clubs", Abbreviation = "QC", Value = 10, Suit = "Clubs" });
            deck.Add(new Card { CardName = "Jack of Clubs", Abbreviation = "JC", Value = 10, Suit = "Clubs" });
            deck.Add(new Card { CardName = "Ten of Clubs", Abbreviation = "10C", Value = 10, Suit = "Clubs" });
            deck.Add(new Card { CardName = "Nine of Clubs", Abbreviation = "9C", Value = 9, Suit = "Clubs" });
            deck.Add(new Card { CardName = "Eight of Clubs", Abbreviation = "8C", Value = 8, Suit = "Clubs" });
            deck.Add(new Card { CardName = "Seven of Clubs", Abbreviation = "7C", Value = 7, Suit = "Clubs" });
            deck.Add(new Card { CardName = "Six of Clubs", Abbreviation = "6C", Value = 6, Suit = "Clubs" });
            deck.Add(new Card { CardName = "Five of Clubs", Abbreviation = "5C", Value = 5, Suit = "Clubs" });
            deck.Add(new Card { CardName = "Four of Clubs", Abbreviation = "4C", Value = 4, Suit = "Clubs" });
            deck.Add(new Card { CardName = "Three of Clubs", Abbreviation = "3C", Value = 3, Suit = "Clubs" });
            deck.Add(new Card { CardName = "Two of Clubs", Abbreviation = "2C", Value = 2, Suit = "Clubs" });

            deck.Add(new Card { CardName = "Ace of Spades", Abbreviation = "AS", Value = 11, Suit = "Spades" });
            deck.Add(new Card { CardName = "King of Spades", Abbreviation = "KS", Value = 10, Suit = "Spades" });
            deck.Add(new Card { CardName = "Queen of Spades", Abbreviation = "QS", Value = 10, Suit = "Spades" });
            deck.Add(new Card { CardName = "Jack of Spades", Abbreviation = "JS", Value = 10, Suit = "Spades" });
            deck.Add(new Card { CardName = "Ten of Spades", Abbreviation = "10S", Value = 10, Suit = "Spades" });
            deck.Add(new Card { CardName = "Nine of Spades", Abbreviation = "9S", Value = 9, Suit = "Spades" });
            deck.Add(new Card { CardName = "Eight of Spades", Abbreviation = "8S", Value = 8, Suit = "Spades" });
            deck.Add(new Card { CardName = "Seven of Spades", Abbreviation = "7S", Value = 7, Suit = "Spades" });
            deck.Add(new Card { CardName = "Six of Spades", Abbreviation = "6S", Value = 6, Suit = "Spades" });
            deck.Add(new Card { CardName = "Five of Spades", Abbreviation = "5S", Value = 5, Suit = "Spades" });
            deck.Add(new Card { CardName = "Four of Spades", Abbreviation = "4S", Value = 4, Suit = "Spades" });
            deck.Add(new Card { CardName = "Three of Spades", Abbreviation = "3S", Value = 3, Suit = "Spades" });
            deck.Add(new Card { CardName = "Two of Spades", Abbreviation = "2S", Value = 2, Suit = "Spades" });

            deck.Add(new Card { CardName = "Ace of Diamonds", Abbreviation = "AD", Value = 11, Suit = "Diamonds" });
            deck.Add(new Card { CardName = "King of Diamonds", Abbreviation = "KD", Value = 10, Suit = "Diamonds" });
            deck.Add(new Card { CardName = "Queen of Diamonds", Abbreviation = "QD", Value = 10, Suit = "Diamonds" });
            deck.Add(new Card { CardName = "Jack of Diamonds", Abbreviation = "JD", Value = 10, Suit = "Diamonds" });
            deck.Add(new Card { CardName = "Ten of Diamonds", Abbreviation = "10D", Value = 10, Suit = "Diamonds" });
            deck.Add(new Card { CardName = "Nine of Diamonds", Abbreviation = "9D", Value = 9, Suit = "Diamonds" });
            deck.Add(new Card { CardName = "Eight of Diamonds", Abbreviation = "8D", Value = 8, Suit = "Diamonds" });
            deck.Add(new Card { CardName = "Seven of Diamonds", Abbreviation = "7D", Value = 7, Suit = "Diamonds" });
            deck.Add(new Card { CardName = "Six of Diamonds", Abbreviation = "6D", Value = 6, Suit = "Diamonds" });
            deck.Add(new Card { CardName = "Five of Diamonds", Abbreviation = "5D", Value = 5, Suit = "Diamonds" });
            deck.Add(new Card { CardName = "Four of Diamonds", Abbreviation = "4D", Value = 4, Suit = "Diamonds" });
            deck.Add(new Card { CardName = "Three of Diamonds", Abbreviation = "3D", Value = 3, Suit = "Diamonds" });
            deck.Add(new Card { CardName = "Two of Diamonds", Abbreviation = "2D", Value = 2, Suit = "Diamonds" });

        }

        public void Shuffle()
        {
            if (deck.Count > 1)
            {
                for (int i = deck.Count - 1; i >= 0; i--)
                {
                    Card tmp = deck[i];
                    int randomIndex = random.Next(i + 1);

                    //Swap elements
                    deck[i] = deck[randomIndex];
                    deck[randomIndex] = tmp;
                }
            }
        }

        public void DrawCard(bool player)
        {
            var firstCard = deck[0];

            if (player)
            {
                hand.Add(firstCard);
                playerTotal = playerTotal + firstCard.Value;
                deck.Remove(firstCard);
            }
            else
            {
                dealerHand.Add(firstCard);
                dealerTotal = dealerTotal + firstCard.Value;
                deck.Remove(firstCard);
            }
        }
        
        public class Card
        {
            public string CardName { get; set; }
            public string Abbreviation { get; set; }
            public int Value { get; set; }

            public string Suit { get; set; }
        }
    }
}
