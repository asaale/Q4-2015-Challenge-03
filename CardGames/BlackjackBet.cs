using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Transactions
{
    using Data;

    public class BlackjackBet : Bet
    {
        private Pot _pot
        {
            get
            {
                return BetTarget as Pot;
            }
        }

        public BlackjackBet(Player player, decimal amount, object betTarget) :
            base(player, amount, betTarget)
        {
        }


        protected override void PerformBet()
        {
            Bettor.Bet();
        }
    }
}
