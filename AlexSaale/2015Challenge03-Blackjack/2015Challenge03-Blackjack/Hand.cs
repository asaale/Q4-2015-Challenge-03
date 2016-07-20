using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2015Challenge03_Blackjack {
    public class Hand : List<Card> {
        public int GetTotalValue() {
            var total = 0;
            foreach (var card in this) {
                total += card.BlackjackValue;
            }
            return total;
        }
    }
}
