using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Application
{
    public interface ICardGameEvents
    {
        void BeginGame();

        void BeginPlay();

        void Ante();

        void DealRound();

        void AnalyzeHands();

        void NoMoreRounds();

        void Raise();
    }
}
