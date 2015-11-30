using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeChallenge03_BlackjackPokerGame
{
	public class PokerGame
	{
		// properties
		private readonly Deck _deck;
		private readonly Dictionary<int, string> _handRankings;
		public Player You;
		public Player Them;

		// methods
		public static void Play()
		{
			string playAgain;

			do
			{
				Console.Clear();
				Console.WriteLine("RESULTS:");

				var game = new PokerGame("you", "computer");
				game.ShuffleDeck();

				game.DealCards(game.You, 5);
				game.DealCards(game.Them, 5);
				game.PrintResults();

				playAgain = Console.ReadLine();

			} while (!string.IsNullOrEmpty(playAgain) && playAgain.Substring(0, 1).ToLower() == "y");
		}

		private void ShuffleDeck()
		{
			_deck.Shuffle();
		}

		private void DealCards(Player player, int numCards)
		{
			for (var i = 0; i < numCards; i++)
			{
				var card = _deck.DealCard();
				player.Hand.Add(card);
			}
		}

		// rank 10 - five cards, 10-J-Q-K-A of same suit
		private static bool HasRoyalFlush(List<Card> hand)
		{
			var validCardNames = new List<string> {"10", "Jack", "Queen", "King", "Ace"};
			var candidateCards = hand.Where(card => validCardNames.Contains(card.Name)).ToList();
			var hasRoyalFlush = candidateCards.Count == validCardNames.Count && CardsAreSameSuit(hand);

			return hasRoyalFlush;
		}

		// rank 9 - five cards sequential of same suit
		private static bool HasStraightFlush(IReadOnlyCollection<Card> hand)
		{
			var hasStraightFlush = CardsAreSameSuit(hand) && CardsAreSequential(hand);

			return hasStraightFlush;
		}

		// rank 8 - four cards of same numerical rank
		private static bool HasFourOfAKind(IEnumerable<Card> hand)
		{
			var countByRank = GetCardCountByRank(hand);

			var hasFourOfAKind = countByRank.Any(rank => rank.Count >= 4);

			return hasFourOfAKind;
		}

		// rank 7 - three cards of same numerical rank + 2 other cards of same numerical rank
		private static bool HasFullHouse(IEnumerable<Card> hand)
		{
			var countByRank = GetCardCountByRank(hand);

			var hasFullHouse = countByRank.Any(rank => rank.Count == 3) && countByRank.Any(rank => rank.Count == 2);

			return hasFullHouse;
		}

		// rank 6 - five cards of same suit
		private static bool HasFlush(IEnumerable<Card> hand)
		{
			var hasFlush = CardsAreSameSuit(hand);

			return hasFlush;
		}

		// rank 5 - five cards sequential regardless of suit
		private static bool HasStraight(IReadOnlyCollection<Card> hand)
		{
			var hasStraight = CardsAreSequential(hand);

			return hasStraight;
		}

		// rank 4 - three cards of same numerical rank
		private static bool HasThreeOfAKind(IEnumerable<Card> hand)
		{
			var countByRank = GetCardCountByRank(hand);

			var hasThreeOfAKind = countByRank.Any(rank => rank.Count == 3);

			return hasThreeOfAKind;
		}

		// rank 3 - two cards of same numerical rank + 2 other cards of same numerical rank
		private static bool HasTwoPair(IEnumerable<Card> hand)
		{
			var countByRank = GetCardCountByRank(hand);

			var hasTwoPair = countByRank.Count(rank => rank.Count == 2) == 2;

			return hasTwoPair;
		}

		// rank 2 (rank 1 is none of the above) - two cards of same numerical rank
		private static bool HasOnePair(IEnumerable<Card> hand)
		{
			var countByRank = GetCardCountByRank(hand);

			var hasOnePair = countByRank.Any(rank => rank.Count == 2);

			return hasOnePair;
		}

		private static bool CardsAreSameSuit(IEnumerable<Card> hand)
		{
			var cardsAreSameSuit = hand.Select(card => card.Suite).Distinct().Count() == 1;

			return cardsAreSameSuit;
		}

		private static bool CardsAreSequential(IReadOnlyCollection<Card> hand)
		{

			var distinctHand = hand.Select(c => c.Rank).Distinct().ToList();

			if (distinctHand.Count != hand.Count)
			{
				return false;
			}
			
			const int minCardRank = 1; // this is the lowest ranking for a card regardless of suit
			const int maxCardRank = 13; // this is the highest ranking for a card regardless of suit
			var cardsThatHaveNextInSequenceCount = 0;

			// ReSharper disable once LoopCanBeConvertedToQuery
			foreach (var card in hand)
			{
				// allow looping on the sequence of ranks, e.g. a 2 of clubs (rank 1) follows an Ace of hearts (rank 13)
				var nextRankInSequence = card.Rank == maxCardRank ? minCardRank : card.Rank + 1;

				if (hand.Any(c => c.Rank == nextRankInSequence))
				{
					cardsThatHaveNextInSequenceCount++;
				}
			}

			// this means that for every card, the next one in the sequence exists (except the last card in the sequence, hence the - 1)
			var cardsAreSequential = cardsThatHaveNextInSequenceCount == hand.Count - 1;

			return cardsAreSequential;
		}

		private static List<CardCountByRank> GetCardCountByRank(IEnumerable<Card> hand)
		{
			var cardCountByRank = hand
				.GroupBy(h => h.Rank)
				.Select(g => new CardCountByRank(g.Key, g.Count()))
				.ToList();

			return cardCountByRank;
		}

		// 1 is lowest rank, 10 is highest rank
		private static int GetHandRanking(List<Card> hand)
		{
			if (HasRoyalFlush(hand))
			{
				return 10;
			}

			if (HasStraightFlush(hand))
			{
				return 9;
			}

			if (HasFourOfAKind(hand))
			{
				return 8;
			}

			if (HasFullHouse(hand))
			{
				return 7;
			}

			if (HasFlush(hand))
			{
				return 6;
			}

			if (HasStraight(hand))
			{
				return 5;
			}

			if (HasThreeOfAKind(hand))
			{
				return 4;
			}

			if (HasTwoPair(hand))
			{
				return 3;
			}

			return HasOnePair(hand) ? 2 : 1;
		}

		private void PrintResults()
		{
			var yourRank = GetHandRanking(You.Hand);
			var theirRank = GetHandRanking(Them.Hand);

			PrintHand(You);
			Console.WriteLine("\nYou got a: {0}", _handRankings[yourRank]);

			PrintHand(Them);
			Console.WriteLine("\nThey got a: {0}", _handRankings[theirRank]);


			if (yourRank > theirRank)
			{
				Console.WriteLine("\n\nGAME OVER.  You win!  Play again (y/n)?");
			}

			else if (theirRank > yourRank)
			{
				Console.WriteLine("\n\nGAME OVER.  Sorry, the computer won! Play again (y/n)?");
			}

			else if (yourRank == theirRank)
			{
				Console.WriteLine("\n\nGAME OVER.  It's a tie!  Play again (y/n)?");
			}

			else
			{
				throw new Exception("Unable to determine winner");
			}
		}

		private static void PrintHand(Player player)
		{
			Console.WriteLine("\nCurrent hand for: {0}", player.Name);
			var orderedHand = player.Hand.OrderByDescending(c => c.Rank);

			foreach (var card in orderedHand)
			{
				Console.WriteLine(card.DisplayName);
			}
		}

		// constructor
		private PokerGame(string player1Name, string player2Name)
		{
			_deck = new Deck();
			_handRankings = new Dictionary<int, string>
			{
				{10, "Royal Flush"},
				{9, "Straight Flush"},
				{8, "Four-of-a-Kind"},
				{7, "Full House"},
				{6, "Flush"},
				{5, "Straight"},
				{4, "Three-of-a-Kind"},
				{3, "Two Pair"},
				{2, "Pair"},
				{1, "Nothing!"}
			};
			You = new Player(player1Name);
			Them = new Player(player2Name);
		}

		// inner class
		public class CardCountByRank
		{
			public int Rank { get; private set; }
			public int Count { get; private set; }

			public CardCountByRank(int rank, int count)
			{
				Rank = rank;
				Count = count;
			}
		}
	}
}