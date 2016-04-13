using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Application
{
    public interface ICardGameActions
    {
        void Initialize();

        void ShuffleCards();

        void BetAllPlayers();

        void DealToPlayers();

        void DetermineWinner();
    }
}
