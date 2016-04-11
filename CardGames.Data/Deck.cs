using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Data
{
    /// <summary>
    /// The deck is going to be used by different players performing different roles, so it needs to be a singleton
    /// </summary>
    public abstract class Deck
    {
        private static Deck _instance;
        public static Deck Instance
        {
            get
            {
                if (_instance == null)
                {
                    // default implementation is a basic deck
                    _instance = DeckFactory.CreateBasicDeck();
                }

                return _instance;
            }

            set
            {
                _instance = value;
            }
        }

        public List<ICard> Cards { get; set; }

        /// <summary>
        /// The number of cards in the deck
        /// </summary>
        public int Size
        {
            get { return Cards.Count; }
        }

        public abstract int FullDeckSize { get; }

        public abstract List<ICard> InitializeCards();

        public void Shuffle()
        {
            // algorithm: 
            // (1) Create a new List to hold the shuffled cards (the target).
            // (2) Generate a random number between (a) 0, and (b) the size of the original card list (i.e., the source list) - 1
            // (3) Use that random number to index into the source list and add that card to the new deck
            // (4) Remove the card just used from the source deck
            // (4) Repeat steps 2 through 4 till all cards are moved from the source deck to the target deck.
            // (5) set the target List as the cards to be used.

            List<ICard> newCards = new List<ICard>();
            Random random = new Random(DateTime.Now.Millisecond);
            int startingSize = Size;

            for (int i = 0; i < startingSize; i++)
            {
                int randomIdx = random.Next(Size);
                var card = Cards.ElementAt<ICard>(randomIdx);
                newCards.Add(card);
                Cards.RemoveAt(randomIdx);
            }

            Cards = newCards;
        }

        public ICard GetNextCard()
        {
            ICard nextCard;

            try
            {
                nextCard = Cards[Cards.Count - 1];
                Cards.Remove(nextCard);
            }
            catch (IndexOutOfRangeException)
            {
                nextCard = new NullCard();
            }

            return nextCard;
        }

    }
}
