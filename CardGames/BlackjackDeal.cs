using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardGames.Data;

namespace CardGames.Transactions
{
    public class BlackjackDeal : Deal
    {
        public BlackjackDeal(List<Player> players, Player dealer) :
            base(players, dealer)
        {
        }

        protected override IGameRole CreateRole(List<Player> players)
        {
            return new BlackjackDealerRole(players);
        }

        protected override void PerformDeal()
        {
            Dealer.Role.PerformRole();
        }
    }
}
