using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeChallenge03_BlackjackPokerGame
{
	public class Deck
	{
		// properties
		private List<Card> _cards;

		// methods
		public void Shuffle()
		{
			var random = new Random();
			_cards = _cards.OrderBy(x => random.Next()).ToList();
		}

		public Card DealCard()
		{
			var topCard = _cards[0];
			_cards.RemoveAt(0);

			return topCard;
		}

		// constructor
		public Deck()
		{
			_cards = new List<Card>
			{
				new Card("2", "Clubs", 1, 2),
				new Card("3", "Clubs", 2, 3),
				new Card("4", "Clubs", 3, 4),
				new Card("5", "Clubs", 4, 5),
				new Card("6", "Clubs", 5, 6),
				new Card("7", "Clubs", 6, 7),
				new Card("8", "Clubs", 7, 8),
				new Card("9", "Clubs", 8, 9),
				new Card("10", "Clubs", 9, 10),
				new Card("Jack", "Clubs", 10, 10),
				new Card("Queen", "Clubs", 11, 10),
				new Card("King", "Clubs", 12, 10),
				new Card("Ace", "Clubs", 13, 11),

				new Card("2", "Diamonds", 1, 2),
				new Card("3", "Diamonds", 2, 3),
				new Card("4", "Diamonds", 3, 4),
				new Card("5", "Diamonds", 4, 5),
				new Card("6", "Diamonds", 5, 6),
				new Card("7", "Diamonds", 6, 7),
				new Card("8", "Diamonds", 7, 8),
				new Card("9", "Diamonds", 8, 9),
				new Card("10", "Diamonds", 9, 10),
				new Card("Jack", "Diamonds", 10, 10),
				new Card("Queen", "Diamonds", 11, 10),
				new Card("King", "Diamonds", 12, 10),
				new Card("Ace", "Diamonds", 13, 11),

				new Card("2", "Hearts", 1, 2),
				new Card("3", "Hearts", 2, 3),
				new Card("4", "Hearts", 3, 4),
				new Card("5", "Hearts", 4, 5),
				new Card("6", "Hearts", 5, 6),
				new Card("7", "Hearts", 6, 7),
				new Card("8", "Hearts", 7, 8),
				new Card("9", "Hearts", 8, 9),
				new Card("10", "Hearts", 9, 10),
				new Card("Jack", "Hearts", 10, 10),
				new Card("Queen", "Hearts", 11, 10),
				new Card("King", "Hearts", 12, 10),
				new Card("Ace", "Hearts", 13, 11),

				new Card("2", "Spades", 1, 2),
				new Card("3", "Spades", 2, 3),
				new Card("4", "Spades", 3, 4),
				new Card("5", "Spades", 4, 5),
				new Card("6", "Spades", 5, 6),
				new Card("7", "Spades", 6, 7),
				new Card("8", "Spades", 7, 8),
				new Card("9", "Spades", 8, 9),
				new Card("10", "Spades", 9, 10),
				new Card("Jack", "Spades", 10, 10),
				new Card("Queen", "Spades", 11, 10),
				new Card("King", "Spades", 12, 10),
				new Card("Ace", "Spades", 13, 11)
			};
		}
	}
}