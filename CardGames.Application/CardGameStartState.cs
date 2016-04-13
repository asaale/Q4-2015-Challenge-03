using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Application
{
    using Data;

    public class CardGameStartState : IBlackjackCardGameState
    {
        public void NoMoreRounds(BlackjackGame game)
        {
            throw new NotImplementedException();
        }

        public void AnalyzeHands(BlackjackGame game)
        {
            throw new NotImplementedException();
        }

        public void Ante(BlackjackGame game)
        {
            throw new NotImplementedException();
        }

        public void BeginGame(BlackjackGame game)
        {
            game.Initialize();
            game.State = BlackjackGame.ReadyToPlayState;
        }

        public void BeginPlay(BlackjackGame game)
        {
            throw new NotImplementedException();
        }

        public void DealRound(BlackjackGame game)
        {
            throw new NotImplementedException();
        }

        public void RequestCards(BlackjackGame game)
        {
            throw new NotImplementedException();
        }
    }
}
