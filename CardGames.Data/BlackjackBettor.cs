using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Data
{
    public class BlackjackBettor : IBet
    {
        public decimal BetAmount { get; set; }

        public decimal Chips { get; set; }

        public object BetTarget { get; set; }


        public BlackjackBettor(decimal chips)
        {
            Chips = chips;
            BetTarget = Pot.Instance;
        }

        public void Bet()
        {
            Pot pot = BetTarget as Pot;
            if (pot != null)
            {
                Chips -= BetAmount;
                pot.AddChips(BetAmount);
            }
        }

        public void TakePot(decimal amount)
        {
            Chips += amount;
        }
    }
}
