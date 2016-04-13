using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Data
{
    public class BlackjackDealerRole : DealerRole
    {
        public BlackjackDealerRole(List<Player> players) :
            base(players)
        {
        }

        protected override void PerformDeal()
        {
            foreach (Player player in Players)
            {
                player.Hand.AddCard(Deck.Instance.GetNextCard());
            }
        }
    }
}
