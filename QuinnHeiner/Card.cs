using System;

namespace CodeChallenge03_BlackjackPokerGame
{
	public class Card
	{
		// properties
		public string Name { get; private set; }
		public string Suite { get; private set; }
		public int Rank { get; private set; }
		public int NumericalValue { get; private set; }

		public string DisplayName
		{
			get
			{
				return String.Concat(Name, " ", Suite);
			}
		}

		// constructor
		public Card(string name, string suite, int rank, int numericalValue)
		{
			Name = name;
			Suite = suite;
			Rank = rank;
			NumericalValue = numericalValue;
		}
	}
}