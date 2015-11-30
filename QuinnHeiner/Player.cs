using System.Collections.Generic;

namespace CodeChallenge03_BlackjackPokerGame
{
	public class Player
	{
		// properties
		public string Name { get; private set; }
		public List<Card> Hand { get; set; }

		// constructor
		public Player(string name)
		{
			Name = name;
			Hand = new List<Card>();
		}
	}
}