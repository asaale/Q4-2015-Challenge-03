using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2015Challenge03_Blackjack {
    public class Player {
        public Hand Hand { get; set; }

        public Player() {
            Hand = new Hand();
        }

        public void EmptyHand() {
            if (Hand != null)
                Hand.Clear();
        }

        public string PrintHand(bool ShowBlackJackTotal = true) {
            var str = "~~~Player's Hand~~~";
            foreach (var card in Hand) {
                str += Environment.NewLine + card;
            }

            if (ShowBlackJackTotal)
                str += Environment.NewLine + "Hand Total: " + Hand.GetTotalValue();

            return str;
        }
    }

}
