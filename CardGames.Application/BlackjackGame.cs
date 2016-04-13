using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Application
{
    using Data;
    using Transactions;

    public class BlackjackGame : ICardGameEvents, ICardGameActions
    {
        public List<Player> Players { get; private set; }

        public Player Dealer { get; set; }

        public decimal AnteAmount { get; set; }

        // the possible states of the game
        public static IBlackjackCardGameState StartGameState { get; private set; }
        public static IBlackjackCardGameState ReadyToPlayState { get; private set; }
        public static IBlackjackCardGameState CardsShuffledState { get; private set; }
        public static IBlackjackCardGameState AntePlacedState { get; private set; }
        public static IBlackjackCardGameState CardsDealtState { get; private set; }
        public static IBlackjackCardGameState BetsPlacedState { get; private set; }
        public static IBlackjackCardGameState GameFinishedState { get; private set; }

        // the current game state
        public IBlackjackCardGameState State { get; set; }

        public BlackjackGame()
        {
            Players = new List<Player>();

            StartGameState = new CardGameStartState();
            ReadyToPlayState = new ReadyToPlayState();
            CardsShuffledState = new CardsShuffledState();
            AntePlacedState = new AntePlacedState();
            CardsDealtState = new CardsDealtState();
            BetsPlacedState = new BetsPlacedState();
            GameFinishedState = new CardGameFinishedState();

            State = StartGameState;
        }

        public void AddPlayer(Player player)
        {
            Players.Add(player);
        }

        public void RemovePlayer(Player player)
        {
            Players.Remove(player);
        }

        public Player GetPlayerByName(string name)
        {
            return Players.SingleOrDefault(p => p.Name.ToLower() == name.ToLower());
        }

        public void SetDealer(Player player)
        {
            Players.Select(p => p.Role = null).ToArray();  // clear all players' roles
            Players.Single(p => p.Name == player.Name).Role = new BlackjackDealerRole(Players);
            Dealer = player;
        }

        #region Events that transition between states

        public void BeginGame()
        {
            State.BeginGame(this);
        }

        public void BeginPlay()
        {
            State.BeginPlay(this);
        }

        public void Ante()
        {
            State.Ante(this);
        }

        public void DealRound()
        {
            State.DealRound(this);
        }

        public void AnalyzeHands()
        {
            State.AnalyzeHands(this);
        }

        public void NoMoreRounds()
        {
            State.NoMoreRounds(this);
        }

        public void Raise()
        {
            throw new NotImplementedException("Raising is not allowed in Blackjack.");
        }

        #endregion

        #region Actions taken in response to events

        public void Initialize()
        {
            // TODO: Do this initialization based on queries to an interface that can be hooked up to a UI

            AddPlayer(new Player(100) { Name = "Alex", });
            AddPlayer(new Player(100) { Name = "Bill" });
            AddPlayer(new Player(100) { Name = "Charlie" });
            SetDealer(GetPlayerByName("Bill"));
            Pot.Instance.Chips = 0;
        }

        public void ShuffleCards()
        {
            new Shuffle(Dealer).Execute();
        }

        public void DealToPlayers()
        {
            new BlackjackDeal(Players, Dealer).Execute();
        }

        public decimal GetBetAmount(Hand hand)
        {
            decimal betAmount = 0M;

            // TODO: Get the bet amount from the user via an abstraction hooked up to the UI

            Random rand = new Random(DateTime.Now.Millisecond);
            betAmount = rand.Next(0, 11);

            return betAmount;
        }

        public void BetAllPlayers()
        {
            foreach (var player in Players)
            {
                player.Bettor.Bet();
            }
        }

        public void DetermineWinner()
        {
            new IdentifyWinnerBlackjack(Players).Execute();
        }

        #endregion
    }
}
