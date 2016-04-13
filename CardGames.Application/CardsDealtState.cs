using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Application
{
    public class CardsDealtState : IBlackjackCardGameState
    {
        public void NoMoreRounds(BlackjackGame game)
        {
            throw new NotImplementedException();
        }

        public void AnalyzeHands(BlackjackGame game)
        {
            decimal betAmount = 0M;

            foreach (var player in game.Players)
            {
                betAmount = game.GetBetAmount(player.Hand);
                
                if (betAmount > 0)
                {
                    player.Bettor.BetAmount = betAmount;
                    player.Bettor.Bet();
                }
            }

            game.State = BlackjackGame.BetsPlacedState;        
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
            throw new NotImplementedException();
        }

        public void RequestCards(BlackjackGame game)
        {
            throw new NotImplementedException();
        }
    }
}
