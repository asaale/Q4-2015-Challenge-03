using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Data
{
    public interface IBet
    {
        decimal Chips { get; set; }

        decimal BetAmount { get; set; }

        object BetTarget { get; set; }

        void Bet();

        void TakePot(decimal amount);
    }
}
