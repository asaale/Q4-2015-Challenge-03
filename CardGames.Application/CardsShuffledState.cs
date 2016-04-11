using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Application
{
    public class CardsShuffledState : IBlackjackCardGameState
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
            game.Players.Select(p => p.Bettor.BetAmount = game.AnteAmount).ToArray();  // set the amount to bet for all players to the ante amount
            game.BetAllPlayers();
            game.State = BlackjackGame.AntePlacedState;
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
            throw new NotImplementedException();
        }

        public void RequestCards(BlackjackGame game)
        {
            throw new NotImplementedException();
        }
    }
}
