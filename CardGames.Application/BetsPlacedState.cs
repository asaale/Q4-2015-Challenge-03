using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Application
{
    public class BetsPlacedState : IBlackjackCardGameState
    {
        public void NoMoreRounds(BlackjackGame game)
        {
            game.DetermineWinner();

            game.State = BlackjackGame.GameFinishedState;
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
            throw new NotImplementedException();
        }

        public void BeginPlay(BlackjackGame game)
        {
            throw new NotImplementedException();
        }

        public void DealRound(BlackjackGame game)
        {
            game.DealToPlayers();

            game.State = BlackjackGame.CardsDealtState;
        }

        public void RequestCards(BlackjackGame game)
        {
            throw new NotImplementedException();
        }
    }
}
