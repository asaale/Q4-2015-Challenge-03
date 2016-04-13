using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Transactions
{
    using Data;

    public abstract class IdentifyWinner : Transaction
    {
        private List<Player> _players;

        public Player Winner { get; }

        public IdentifyWinner(List<Player> players)
        {
            _players = players;
        }

        public abstract long ScoreHand(Hand hand);

        protected abstract void BreakTies(List<Player> tiedPlayers);

        public override void Execute()
        {
            _players.Select(p => p.HandStatus = HandStatus.Unknown).ToList();   // reset the hand status
            long highScore = _players.Select(p => ScoreHand(p.Hand)).Max();
            var winners = _players.Where(p => p.Hand.Score == highScore).ToList();
            if (winners.Count() > 1)
            {
                BreakTies(winners);
            }

            winners.Select(w => w.HandStatus = HandStatus.Winner).ToList();
        }
    }
}
