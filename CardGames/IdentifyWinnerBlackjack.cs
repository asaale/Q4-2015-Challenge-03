using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardGames.Data;

namespace CardGames.Transactions
{
    using Data;

    public class IdentifyWinnerBlackjack : IdentifyWinner
    {
        public IdentifyWinnerBlackjack(List<Player> players)
            : base(players)
        {
        }

        public override long ScoreHand(Hand hand)
        {
            long score = 0;

            // We have to handle the dual value of Aces in Blackjack.  Our strategy is to start by valuing an Ace as 11,
            // but if that would cause a bust, value the Ace as 1 instead.  Keep doing this until we either get to 21 or below, or run out of aces
            // NOTE: The calls to ToList() in this method are solely for the purpose of running the LINQ queries; their return values are not needed.

            hand.Cards.Where(c => c.Name == CardName.Ace).Select(c => c.Value = 11).ToList();
            score = hand.Cards.Select(c => c.Value).Aggregate((a, b) => a + b);

            while (score > 21 && hand.Cards.Any(c => c.Name == CardName.Ace && c.Value == 11))
            {
                hand.Cards.Where(c => c.Name == CardName.Ace && c.Value == 11).Take(1).Select(c => c.Value = 1).ToList();
                score = hand.Cards.Select(c => c.Value).Aggregate((a, b) => a + b);
            }

            hand.Score = score <= 21 ? score : -1;   // -1 means a bust

            return hand.Score;
        }

        protected override void BreakTies(List<Player> tiedPlayers)
        {
            tiedPlayers.Remove(tiedPlayers.SingleOrDefault(p => p.Role != null && p.Role.GetType() == typeof(BlackjackDealerRole)));
        }
    }
}
