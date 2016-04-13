using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CardGames.Test
{
    using Data;
    using Transactions;
    using Application;

    [TestClass]
    public class BlackJackTest
    {
        [TestMethod]
        public void ShuffleTest_BasicDeck()
        {
            // arrange
            var deck = DeckFactory.CreateBasicDeck();

            // act
            deck.Shuffle();

            // assert
            CollectionAssert.AllItemsAreNotNull(deck.Cards);
            CollectionAssert.AllItemsAreUnique(deck.Cards);
            Assert.AreEqual(deck.Cards.Count, deck.Size);
            Assert.IsTrue(IsDeckShuffled(deck));            
        }

        [TestMethod]
        public void ShuffleTest_PokerDeck()
        {
            // arrange
            Deck deck = DeckFactory.CreatePokerDeck();

            // act
            deck.Shuffle();

            // assert
            CollectionAssert.AllItemsAreNotNull(deck.Cards);
            CollectionAssert.AllItemsAreUnique(deck.Cards);
            Assert.AreEqual(deck.Cards.Count, deck.Size);
        }

        [TestMethod]
        public void DealTest_BasicDeck()
        {
            // arrange
            Deck deck = DeckFactory.CreateBasicDeck();
            Player player = new Player();

            // arrange/act
            Assert.AreEqual(deck.Size, 52);

            ICard card = deck.GetNextCard();
            Assert.IsInstanceOfType(card, typeof(Card));
            player.Hand.AddCard(card);
            Assert.AreEqual(player.Hand.Size, 1);
            Assert.AreEqual(deck.Size, 51);

            card = deck.GetNextCard();
            Assert.IsInstanceOfType(card, typeof(Card));
            player.Hand.AddCard(card);
            Assert.AreEqual(player.Hand.Size, 2);
            Assert.AreEqual(deck.Size, 50);

            card = deck.GetNextCard();
            Assert.IsInstanceOfType(card, typeof(Card));
            player.Hand.AddCard(card);
            Assert.AreEqual(player.Hand.Size, 3);
            Assert.AreEqual(deck.Size, 49);
        }

        [TestMethod]
        public void DealTest_PokerDeck()
        {
            // arrange
            Deck deck = DeckFactory.CreatePokerDeck();
            Player player = new Player();

            // arrange/act
            Assert.AreEqual(deck.Size, 54);

            ICard card = deck.GetNextCard();
            Assert.IsInstanceOfType(card, typeof(Card));
            player.Hand.AddCard(card);
            Assert.AreEqual(player.Hand.Size, 1);
            Assert.AreEqual(deck.Size, 53);

            card = deck.GetNextCard();
            Assert.IsInstanceOfType(card, typeof(Card));
            player.Hand.AddCard(card);
            Assert.AreEqual(player.Hand.Size, 2);
            Assert.AreEqual(deck.Size, 52);

            card = deck.GetNextCard();
            Assert.IsInstanceOfType(card, typeof(Card));
            player.Hand.AddCard(card);
            Assert.AreEqual(player.Hand.Size, 3);
            Assert.AreEqual(deck.Size, 51);
        }

        [TestMethod]
        public void ScoreHandTest_Blackjack_Aces_BasicDeck()
        {
            // arrange
            IdentifyWinnerBlackjack identWinnerBlackjack = new IdentifyWinnerBlackjack(null);
            Player player = new Player();
            long expectedScore = 0;
            player.Hand.AddCard(new Card() { Name = CardName.Ace, Suit = CardSuit.Clubs});
            expectedScore = 11;

            // act
            identWinnerBlackjack.ScoreHand(player.Hand);

            // assert
            Assert.AreEqual(expectedScore, player.Hand.Score);

            // arrange
            player.Hand.AddCard(new Card() { Name = CardName.Ace, Suit = CardSuit.Diamonds });
            expectedScore = 12;  // value the Ace as 1, not 11, or we would bust

            // act
            identWinnerBlackjack.ScoreHand(player.Hand);

            // assert
            Assert.AreEqual(expectedScore, player.Hand.Score);

            // arrange
            player.Hand.AddCard(new Card() { Name = CardName.Two, Suit = CardSuit.Diamonds });
            expectedScore = 14;

            // act
            identWinnerBlackjack.ScoreHand(player.Hand);

            // assert
            Assert.AreEqual(expectedScore, player.Hand.Score);

            // arrange
            player.Hand.AddCard(new Card() { Name = CardName.King, Suit = CardSuit.Diamonds });
            expectedScore = 14;  // both Aces should now be valued as 1 to avoid a bust

            // act
            identWinnerBlackjack.ScoreHand(player.Hand);

            // assert
            Assert.AreEqual(expectedScore, player.Hand.Score);

            // arrange
            player.Hand.AddCard(new Card() { Name = CardName.Nine, Suit = CardSuit.Diamonds });
            expectedScore = -1;  // even with both Aces valued as 1, we will still bust

            // act
            identWinnerBlackjack.ScoreHand(player.Hand);

            // assert
            Assert.AreEqual(expectedScore, player.Hand.Score);
        }

        [TestMethod]
        public void BlackjackBetTest()
        {
            // arrange
            decimal Chips = 100.0M;
            decimal betAmount = 5.00M;
            Pot.Instance.Chips = 0;
            List<Player> players = new List<Player>();
            Player alex = new Player { Name = "Alex", Bettor = new BlackjackBettor(Chips) };
            Player bill = new Player { Name = "Bill", Bettor = new BlackjackBettor(Chips) };
            Player charlie = new Player { Name = "Charlie", Bettor = new BlackjackBettor(Chips) };
            players.Add(alex);
            players.Add(bill);
            players.Add(charlie);
            players.Select(p => p.Bettor.BetAmount = betAmount).ToArray();

            // act
            foreach (var player in players)
            {
                player.Bettor.Bet();
            }

            // assert
            Assert.AreEqual<decimal>(Pot.Instance.Chips, betAmount * players.Count);
            foreach (var player in players)
            {
                Assert.AreEqual<decimal>(player.Bettor.Chips, Chips - betAmount);
            }

            // simulate a winner taking the pot

            // act
            Pot.Instance.TakePot(alex.Bettor);

            // assert
            Assert.AreEqual<decimal>(alex.Bettor.Chips, 110.0M);
            Assert.AreEqual<decimal>(Pot.Instance.Chips, 0);
        }

        [TestMethod]
        public void BlackjackBetTest_MultipleWinners()
        {
            // arrange
            decimal startingChips = 100.0M;
            decimal betAmount = 5.00M;
            Pot.Instance.Chips = 0;
            List<Player> players = new List<Player>();
            Player alex = new Player { Name = "Alex", Bettor = new BlackjackBettor(startingChips) };
            Player bill = new Player { Name = "Bill", Bettor = new BlackjackBettor(startingChips) };
            Player charlie = new Player { Name = "Charlie", Bettor = new BlackjackBettor(startingChips) };
            players.Add(alex);
            players.Add(bill);
            players.Add(charlie);

            // act
            foreach (var player in players)
            {
                player.Bettor.BetAmount = betAmount;
                player.Bettor.Bet();
            }

            // assert
            Assert.AreEqual<decimal>(Pot.Instance.Chips, betAmount * players.Count);
            foreach (var player in players)
            {
                Assert.AreEqual<decimal>(player.Bettor.Chips, startingChips - betAmount);
            }

            // simulate three winner splitting the pot

            // act
            Pot.Instance.TakePot(players.Select(p => p.Bettor).ToList());

            // assert
            Assert.AreEqual<decimal>(alex.Bettor.Chips, startingChips);
            Assert.AreEqual<decimal>(bill.Bettor.Chips, startingChips);
            Assert.AreEqual<decimal>(charlie.Bettor.Chips, startingChips);
            Assert.AreEqual<decimal>(Pot.Instance.Chips, 0);
        }

        [TestMethod]
        public void BlackjackDealTransactionTest_BasicDeck()
        {
            // NOTE: A game transaction using the basic (52-card) deck requires no explicit instantiation of the deck
            // by the client code; a basic deck is the default Instance.

            BlackjackDealTransactionTest();
        }

        [TestMethod]
        public void BlackjackDealTransactionTest_PokerDeck()
        {
            Deck.Instance = DeckFactory.CreatePokerDeck();
            BlackjackDealTransactionTest();
        }

        [TestMethod]
        public void BlackjackShuffleTransactionTest_BasicDeck()
        {
            // NOTE: A game transaction using the basic (52-card) deck requires no explicit instantiation of the deck
            // by the client code; a basic deck is the default Instance.

            // arrange
            List<Player> players = new List<Player>();
            Player alex = new Player { Name = "Alex" };
            Player bill = new Player { Name = "Bill" };
            Player charlie = new Player { Name = "Charlie" };
            players.Add(alex);
            players.Add(bill);
            players.Add(charlie);
            Shuffle shuffle = new Shuffle(bill);

            // act
            shuffle.Execute();

            // assert
            Assert.IsTrue(IsDeckShuffled(Deck.Instance));            
        }

        [TestMethod]
        public void BlackjackShuffleTransactionTest_PokerDeck()
        {
            // arrange
            Deck.Instance = DeckFactory.CreatePokerDeck();
            List<Player> players = new List<Player>();
            Player alex = new Player { Name = "Alex" };
            Player bill = new Player { Name = "Bill" };
            Player charlie = new Player { Name = "Charlie" };
            players.Add(alex);
            players.Add(bill);
            players.Add(charlie);
            Shuffle shuffle = new Shuffle(bill);

            // act
            shuffle.Execute();

            // assert
            Assert.IsTrue(IsDeckShuffled(Deck.Instance));
        }

        [TestMethod]
        public void BlackjackIdentifyWinnerTransactionTest_BasicDeck()
        {
            // arrange
            List<Player> players = new List<Player>();
            Player alex = new Player { Name = "Alex" };
            Player bill = new Player { Name = "Bill" };
            Player charlie = new Player { Name = "Charlie" };
            players.Add(alex);
            players.Add(bill);
            players.Add(charlie);

            Shuffle shuffle = new Shuffle(bill);
            shuffle.Execute();

            BlackjackDeal blackjackDeal = new BlackjackDeal(players, bill);
            Random rand = new Random(DateTime.Now.Millisecond);
            int upperBound = rand.Next(2,6);
            // deal anywhere from 2 to 5 cards to each player
            for (int i = 1; i <= upperBound; i++)
            {
                blackjackDeal.Execute();
            }            

            IdentifyWinnerBlackjack identWinner = new IdentifyWinnerBlackjack(players);

            // act
            identWinner.Execute();

            // assert
                // verify that the winner(s) has/have the highest score
            Assert.AreEqual(players.Where(p => p.HandStatus == HandStatus.Winner).Select(p => p.Hand.Score).Max(), players.Select(p => p.Hand.Score).Max());
        }

        [TestMethod]
        public void BlackjackBetTransactionTest()
        {
            // arrange
            decimal startingChips = 100;
            decimal alexBetAmount = 5;
            decimal billBetAmount = 6;
            decimal charlieBetAmount = 7;
            List<Player> players = new List<Player>();
            Player alex = new Player(startingChips) { Name = "Alex" };
            Player bill = new Player(startingChips) { Name = "Bill" };
            Player charlie = new Player(startingChips) { Name = "Charlie" };

            players.Add(alex);
            players.Add(bill);
            players.Add(charlie);
            var bet1 = new BlackjackBet(alex, alexBetAmount, Pot.Instance);
            var bet2 = new BlackjackBet(bill, billBetAmount, Pot.Instance);
            var bet3 = new BlackjackBet(charlie, charlieBetAmount, Pot.Instance);

            // act
            bet1.Execute();
            bet2.Execute();
            bet3.Execute();

            // assert
            Assert.AreEqual<decimal>(Pot.Instance.Chips, alexBetAmount + billBetAmount + charlieBetAmount);
            Assert.AreEqual<decimal>(alex.Bettor.Chips, startingChips - alexBetAmount);
            Assert.AreEqual<decimal>(alex.Bettor.Chips, bet1.Bettor.Chips);
            Assert.AreEqual<decimal>(bill.Bettor.Chips, startingChips - billBetAmount);
            Assert.AreEqual<decimal>(bill.Bettor.Chips, bet2.Bettor.Chips);
            Assert.AreEqual<decimal>(charlie.Bettor.Chips, startingChips - charlieBetAmount);
            Assert.AreEqual<decimal>(charlie.Bettor.Chips, bet3.Bettor.Chips);

            // simulate three winner splitting the pot

            // act
            Pot.Instance.TakePot(players.Select(p => p.Bettor).ToList());

            // assert
            decimal potAmount = alexBetAmount + billBetAmount + charlieBetAmount;
            Assert.AreEqual<decimal>(alex.Bettor.Chips, startingChips - alexBetAmount + (potAmount / 3.0M));
            Assert.AreEqual<decimal>(bill.Bettor.Chips, startingChips - billBetAmount + (potAmount / 3.0M));
            Assert.AreEqual<decimal>(charlie.Bettor.Chips, startingChips - charlieBetAmount + (potAmount / 3.0M));
            Assert.AreEqual<decimal>(Pot.Instance.Chips, 0);
        }

        [TestMethod]
        public void BlackjackGameFullSimulatedGameTest()
        {
            // arrange
            BlackjackGame game = new BlackjackGame() { AnteAmount = 1.00M };
            decimal startingChipsPerPlayer = 100M;
            int roundsToDeal = 3;

            // act
            game.BeginGame();

            // assert
            Assert.IsTrue(game.State.GetType() == typeof(ReadyToPlayState));
            Assert.IsTrue(game.Players.Count > 1);    // we have players?
            Assert.AreEqual(game.Players.Where(p => p.Bettor.Chips > 0).Count(), game.Players.Count());   // the players all have chips?

            // act
            game.BeginPlay();

            // assert
            Assert.IsTrue(game.State.GetType() == typeof(CardsShuffledState));  
            Assert.IsTrue(IsDeckShuffled(Deck.Instance));  // the deck is shuffled?

            // act
            game.Ante();

            // assert
            Assert.IsTrue(game.State.GetType() == typeof(AntePlacedState));
            Assert.AreEqual(Pot.Instance.Chips, game.AnteAmount * game.Players.Count);  // pot has the correct amount in it?
            Assert.IsTrue(game.Players.Where(p => p.Bettor.Chips == startingChipsPerPlayer - game.AnteAmount).Count() == game.Players.Count);  // all the players' chips are reduced by the ante amount?

            // Deal / bet iterations

            for (int i = 1; i <= roundsToDeal; i++)
            {
                // act
                game.DealRound();

                // assert
                Assert.IsTrue(game.State.GetType() == typeof(CardsDealtState));
                Assert.IsTrue(game.Players.Where(p => p.Hand.Size == i).Count() == game.Players.Count());  // all the players have the correct number of cards?

                // act
                game.AnalyzeHands();

                // assert
                Assert.IsTrue(game.State.GetType() == typeof(BetsPlacedState));
                decimal currentPlayersChipsTotal = game.Players.Select(p => p.Bettor.Chips).Aggregate((ch1, ch2) => ch1 + ch2);
                Assert.AreEqual(currentPlayersChipsTotal + Pot.Instance.Chips, startingChipsPerPlayer * game.Players.Count);  // pot has the correct amount in it?
            }

            // act
            game.NoMoreRounds();

            // assert
            Assert.IsTrue(game.State.GetType() == typeof(CardGameFinishedState));
            Assert.IsTrue(game.Players.Where(p => p.HandStatus == HandStatus.Winner).Count() > 0);  // there was at least one winner?

        }


        #region Helper Methods

        private bool IsDeckShuffled(Deck deck)
        {
            // a quick and dirty check for randomization; this will fail on average once every couple thousand
            // times through pure chance, even if the cards have been shuffled correctly
            bool isShuffled = true;
            ICard card1 = deck.GetNextCard();
            ICard card2 = deck.GetNextCard();
            Deck unshuffledDeck = DeckFactory.CreateBasicDeck();

            try
            {
                Assert.IsFalse(card1.Name == unshuffledDeck.Cards[0].Name && card1.Suit == unshuffledDeck.Cards[0].Suit);
            }
            catch (AssertFailedException)
            {
                try
                {
                    Assert.IsFalse(card2.Name == unshuffledDeck.Cards[1].Name && card2.Suit == unshuffledDeck.Cards[1].Suit);
                }
                catch (AssertFailedException)
                {
                    isShuffled = false;
                }
            }

            return isShuffled;
        }

        private void BlackjackDealTransactionTest()
        {
            // arrange
            int numOfRoundsDealt = 0;
            List<Player> players = new List<Player>();
            Player alex = new Player { Name = "Alex" };
            Player bill = new Player { Name = "Bill" };
            Player charlie = new Player { Name = "Charlie" };
            players.Add(alex);
            players.Add(bill);
            players.Add(charlie);
            BlackjackDeal blackjackDeal = new BlackjackDeal(players, bill);

            // act
            blackjackDeal.Execute();

            // assert
            numOfRoundsDealt++;
            foreach (Player player in players)
            {
                Assert.AreEqual(player.Hand.Size, numOfRoundsDealt);
            }
            Assert.AreEqual(Deck.Instance.Size, Deck.Instance.FullDeckSize - (players.Count * numOfRoundsDealt));

            // act
            blackjackDeal.Execute();

            // assert
            numOfRoundsDealt++;
            foreach (Player player in players)
            {
                Assert.AreEqual(player.Hand.Size, numOfRoundsDealt);
            }
            Assert.AreEqual(Deck.Instance.Size, Deck.Instance.FullDeckSize - (players.Count * numOfRoundsDealt));

            // act
            blackjackDeal.Execute();

            // assert
            numOfRoundsDealt++;
            foreach (Player player in players)
            {
                Assert.AreEqual(player.Hand.Size, numOfRoundsDealt);
            }
            Assert.AreEqual(Deck.Instance.Size, Deck.Instance.FullDeckSize - (players.Count * numOfRoundsDealt));
        }

        #endregion
    }
}
