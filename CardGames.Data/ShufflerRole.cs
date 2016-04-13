using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Data
{
    public class ShufflerRole : IGameRole
    {
        public Deck Deck
        {
            get { return Deck.Instance; }
        }

        public void InitRole(object obj)
        {
        }

        public void PerformRole()
        {
            Deck.Shuffle();
        }

        public void DisposeRole()
        {
            
        }
    }
}
