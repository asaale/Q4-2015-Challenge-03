using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Application
{
    public interface IBlackjackCardGameState
    {
        void BeginGame(BlackjackGame game);

        void BeginPlay(BlackjackGame game);

        void Ante(BlackjackGame game);

        void DealRound(BlackjackGame game);

        void AnalyzeHands(BlackjackGame game);

        void NoMoreRounds(BlackjackGame game);
    }
}
