using System;
using System.Linq;

namespace CodeChallenge03_BlackjackPokerGame
{
	public class BlackjackGame
	{
		// properties
		private readonly Deck _deck;
		public Player You;
		public Player Them; 

		// methods
		public static void Play()
		{
			string playAgain;
			const int computerMaxScoreAllowed = 18; // this is the highest the computer will go score-wise before standing
			const int perfectScore = 21;

			do
			{
				Console.Clear();
				Console.WriteLine("Welcome to Blackjack!  Hit enter to draw a card, type s to stand and end game.");

				var game = new BlackjackGame("you", "computer");
				game.ShuffleDeck();

				var you = game.You;
				var computer = game.Them;

				// start out with 2 cards
				var firstCard = game.Hit(you);
				Console.WriteLine("\n" + game.DisplayPlayerCardAndScore(you, firstCard));

				var secondCard = game.Hit(you);
				Console.WriteLine("\n" + game.DisplayPlayerCardAndScore(you, secondCard));

				// your turn
				while (game.GetPlayerScore(you) < perfectScore)
				{
					var userAction = Console.ReadLine();
					if (!string.IsNullOrEmpty(userAction) && userAction.ToLower() == "s")
					{
						break;
					}

					var card = game.Hit(you);
					Console.WriteLine(game.DisplayPlayerCardAndScore(you, card));
				}

				// computer turn
				while (game.GetPlayerScore(computer) < computerMaxScoreAllowed)
				{
					var card = game.Hit(computer);
					Console.WriteLine("\n" + game.DisplayPlayerCardAndScore(computer, card));
				}

				Console.WriteLine(game.DisplayResults());
				playAgain = Console.ReadLine();

			} while (!string.IsNullOrEmpty(playAgain) && playAgain.Substring(0, 1).ToLower() == "y");
		}

		private void ShuffleDeck()
		{
			_deck.Shuffle();
		}

		private Card Hit(Player player)
		{
			var card = _deck.DealCard();
			player.Hand.Add(card);

			return card;
		}

		private int GetPlayerScore(Player player)
		{
			var score = player.Hand.Sum(card => card.NumericalValue);
			return score;
		}

		private string DisplayPlayerCardAndScore(Player player, Card card)
		{
			return string.Format("{0} (Current score for {1}: {2})", card.DisplayName, player.Name, GetPlayerScore(player));
		}

		private string DisplayResults()
		{
			var yourScore = GetPlayerScore(You);
			var computerScore = GetPlayerScore(Them);
			const int perfectScore = 21;
			Player winner;

			if (yourScore == perfectScore)
			{
				winner = You;
			} 
			else if (yourScore > perfectScore)
			{
				winner = Them;
			} 
			else if (yourScore >= computerScore || computerScore > perfectScore)
			{
				winner = You;
			}
			else
			{
				winner = Them;
			}

			return string.Format("\n\nGAME OVER.  Your score was {0}.  Computer score was {1}.  Winner is {2}.  Play again (y/n)?", yourScore, computerScore, winner.Name);
		}

		// constructor
		private BlackjackGame(string player1Name, string player2Name)
		{
			_deck = new Deck();
			You = new Player(player1Name);
			Them = new Player(player2Name);
		}
	}
}