using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2015Challenge03_Blackjack
{
    class Program
    {
        static void Main(string[] args) {
            var Deck = new Deck();
            var Player = new Player();
            var House = new Player();
            var playerWantsToPlayBlackjackAgain = true;
            var playerWantsToPlayPokerAgain = true;
            var playerWantsToPlay = true;

            var judge = new PokerHandJudge();

            while (playerWantsToPlay) {

                Console.WriteLine("What would you like to play? (P)oker or (B)lackjack?");
                var response = Console.ReadLine().ToLower();
                while(response != "p" && response != "b") {
                    Console.WriteLine(response + " is not in the correct format.  try again, dummy");
                    Console.WriteLine("What would you like to play? (P)oker or (B)lackjack?");
                    response = Console.ReadLine().ToLower();
                }

                playerWantsToPlayPokerAgain = response == "p";
                playerWantsToPlayBlackjackAgain = response == "b";

                #region Poker

                while (playerWantsToPlayPokerAgain) {
                    Console.WriteLine("New Round of Poker!");
                    Console.WriteLine("Dealing Cards"); Deck = new Deck();
                    Deck.Shuffle();

                    Player.EmptyHand();
                    House.EmptyHand();

                    Player.Hand.Add(Deck.DealCard());
                    Player.Hand.Add(Deck.DealCard());
                    Player.Hand.Add(Deck.DealCard());
                    Player.Hand.Add(Deck.DealCard());
                    Player.Hand.Add(Deck.DealCard());

                    House.Hand.Add(Deck.DealCard());
                    House.Hand.Add(Deck.DealCard());
                    House.Hand.Add(Deck.DealCard());
                    House.Hand.Add(Deck.DealCard());
                    House.Hand.Add(Deck.DealCard());

                    var playerValue = judge.JudgeHand(Player.Hand);
                    var houseValue = judge.JudgeHand(House.Hand);

                    Console.WriteLine(Player.PrintHand(false));
                    Console.WriteLine("Player's Hand: " + judge.GetHandType(playerValue));
                    Console.WriteLine("House's Hand: " + judge.GetHandType(houseValue));

                    if (playerValue >= houseValue)
                        Console.WriteLine("Player Wins!");
                    else
                        Console.WriteLine("House Wins!");

                    Console.WriteLine(Environment.NewLine + "Would you like to play again? (y/n)");
                    playerWantsToPlayPokerAgain = Console.ReadLine().ToLower() == "y";
                    Console.Clear();
                }

                #endregion Poker
               
                #region BlackJack
                while (playerWantsToPlayBlackjackAgain) {
                    Console.WriteLine("New Round of Blackjack!");
                    Console.WriteLine("Dealing Cards");

                    Deck = new Deck();
                    Deck.Shuffle();

                    Player.EmptyHand();
                    House.EmptyHand();

                    Player.Hand.Add(Deck.DealCard());
                    Player.Hand.Add(Deck.DealCard());

                    House.Hand.Add(Deck.DealCard());
                    House.Hand.Add(Deck.DealCard());

                    var playerTurnOver = Player.Hand.GetTotalValue() >= 21;
                    if (Player.Hand.GetTotalValue() == 21) {
                        Console.WriteLine("You were dealt blackjack!");
                        playerTurnOver = true;
                    }
                    while (!playerTurnOver) {
                        Console.WriteLine(Player.PrintHand());

                        //Prompt user to take turn.  Hit or Stand
                        Console.WriteLine("What would you like to do? (H)it or (S)tand?");
                        string userInput = Console.ReadLine().ToLower();
                        if (userInput != "h" && userInput != "s") {
                            Console.WriteLine("'" + userInput + "' is not in the correct format.  try again, dummy.");
                            Console.WriteLine("What would you like to do? (H)it or (S)tand?");
                            userInput = Console.ReadLine().ToLower();
                        }

                        //If Hit
                        if (userInput == "h") {
                            var card = Deck.DealCard();
                            Player.Hand.Add(card);
                            Console.WriteLine("you drew the " + card);
                            if (Player.Hand.GetTotalValue() == 21) {
                                playerTurnOver = true;
                                Console.WriteLine("New Total: " + Player.Hand.GetTotalValue() + ". You Should Stand.");
                            }
                            if (Player.Hand.GetTotalValue() > 21) {
                                playerTurnOver = true;
                                Console.WriteLine("New Total: " + Player.Hand.GetTotalValue() + ". You busted.");
                            }
                        }
                        //if Stand
                        else if (userInput == "s") {
                            playerTurnOver = true;
                        }

                    }

                    var playerTotal = Player.Hand.GetTotalValue();
                    var playerHasBusted = playerTotal > 21;

                    var houseTurnOver = House.Hand.GetTotalValue() >= 21 || playerHasBusted;
                    if (House.Hand.GetTotalValue() == 21) {
                        Console.WriteLine("House was dealt 21.  Ha Ha Ha");
                        houseTurnOver = true;
                    }
                    while (!houseTurnOver) {
                        var total = House.Hand.GetTotalValue();
                        Console.WriteLine("House's Total: " + total);

                        //the house will hit on 17 or lower, stay on 18 or higher
                        if (total <= 17) {
                            Console.WriteLine("House Hits");
                            var card = Deck.DealCard();
                            House.Hand.Add(card);
                            Console.WriteLine("House draws " + card);
                            if (House.Hand.GetTotalValue() >= 21)
                                houseTurnOver = true;
                        } else {
                            Console.WriteLine("House Stands");
                            houseTurnOver = true;
                        }
                    }

                    var houseTotal = House.Hand.GetTotalValue();
                    var houseHasBusted = houseTotal > 21;

                    //player wins if player has not busted and house has busted
                    // or if player total less than or equal to 21 and player greater than or equal to house
                    var playerHasWon = !playerHasBusted && houseHasBusted || playerTotal <= 21 && playerTotal >= houseTotal;
                    var playerGotBlackJack = playerTotal == 21;
                    var houseGotBlackjack = houseTotal == 21;

                    Console.WriteLine("End Result: Player - " + playerTotal + " vs The House - " + houseTotal);
                    if (playerHasWon) {
                        if (playerGotBlackJack) {
                            Console.Write("Blackjack! ");
                        }
                        Console.WriteLine("You Win!");
                    } else {
                        if (playerHasBusted) {
                            Console.Write("You Busted! ");
                        }
                        if (houseGotBlackjack) {
                            Console.Write("House Got Blackjack! ");
                        }
                        Console.WriteLine("You Lose!");
                    }

                    Console.WriteLine(Environment.NewLine + "Would you like to play again? (y/n)");
                    playerWantsToPlayBlackjackAgain = Console.ReadLine().ToLower() == "y";
                    Console.Clear();
                }
                #endregion Blackjack
            }
        }
    }
}
