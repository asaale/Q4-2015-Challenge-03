using System;

namespace CodeChallenge03_BlackjackPokerGame
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Type b to play Blackjack, p to play Poker, any other key to quit.");

			var userResponse = Console.ReadLine();

			switch (userResponse)
			{
				case "b":
					BlackjackGame.Play();
					break;
				case "p":
					PokerGame.Play();
					break;
			}
		}
	}
}
