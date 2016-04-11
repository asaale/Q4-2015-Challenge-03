using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Data
{
    public abstract class DeckDecorator : Deck
    {
        protected Deck _baseDeck;
    }
}
