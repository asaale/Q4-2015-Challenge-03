using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Data
{
    public class Hand
    {
        public List<ICard> Cards { get; private set; }

        /// <summary>
        /// Represents the relative value of the hand, expressed as an integer.
        /// </summary>
        public long Score { get; set; }

        public int Size
        {
            get { return (Cards == null || Cards.Count == 0) ? 0 : Cards.Count; }
        }

        public Hand()
        {
            Cards = new List<ICard>();
        }

        public void AddCard(ICard card)
        {
            Cards.Add(card);
        }

        public void RemoveCard(ICard card, int idx = 0)
        {
            Cards.RemoveAt(idx);
        }

        public void RemoveAllCards()
        {
            Cards.Clear();
        }
    }
}
