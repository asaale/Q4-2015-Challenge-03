using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Data
{
    public abstract class DealerRole : IGameRole
    {
        protected List<Player> Players;

        public DealerRole(List<Player> players)
        {
            Players = players;
        }

        protected abstract void PerformDeal();

        public void PerformRole()
        {
            PerformDeal();
        }

        public void InitRole(object obj)
        {
        }

        public void DisposeRole()
        {
            Players = null;
        }
    }
}
