using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Transactions
{
    using Data;

    public abstract class Deal : Transaction
    {

        public Player Dealer { get; set; }

        public List<Player> Players { get; }

        public Deal(List<Player> players, Player dealer)
        {
            Players = players;
            Players.Select(p => p.Role = null);
            Dealer = Players.Single(p => p.Name == dealer.Name);
            Dealer.Role = CreateRole(players);
        }

        protected abstract IGameRole CreateRole(List<Player> players);

        protected abstract void PerformDeal();

        public override void Execute()
        {
            PerformDeal();
        }
    }
}
