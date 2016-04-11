using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Transactions
{
    using Data;

    public abstract class Bet : Transaction
    {
        private Player _player;

        // protected decimal Amount { get; set; }

        protected object BetTarget { get; set; }

        public IBet Bettor
        {
            get { return _player.Bettor; }

            // private set  { _player.Bettor = value; }
        }

        public Bet(Player player, decimal amount, object betTarget)
        {
            _player = player;
            Bettor.BetAmount = amount;
            BetTarget = betTarget;

            // we don't need to use the factory method pattern here to create a Bettor, since the Bet class delegates all
            // betting behavior to its Player member, which has its own member Bettor.

            // Bettor = CreateBettor();
        }

        // protected abstract IBet CreateBettor();

        protected abstract void PerformBet();

        public override void Execute()
        {
            PerformBet();
        }
    }
}
