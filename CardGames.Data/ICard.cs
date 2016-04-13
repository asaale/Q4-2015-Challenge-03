using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Data
{
    public interface ICard
    {
        CardSuit Suit { get; set; }

        CardName Name { get; set; }

        int Value { get; set; }
    }
}
