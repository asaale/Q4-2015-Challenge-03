using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using BlackJack;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BlackJack
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Poker : Page
    {
        PokerBase pokerBase = new PokerBase();
        public int playerRank = 0;
        public int dealerRank = 0;

        public Poker()
        {
            this.InitializeComponent();
            pokerBase.CreateDeck();
            txtPlayerTotal.Text = "Nothing";
            txtDealerTotal.Text = "Nothing";
        }

        private void btnDeal_Click(object sender, RoutedEventArgs e)
        {
            FullReset();
            pokerBase.ModifyDeckForPoker();
            pokerBase.Shuffle();
            Deal();
            grdMain.Visibility = Visibility.Visible;
        }

        public void Deal()
        {
            pokerBase.DrawCard(true);
            pokerBase.DrawCard(false);
            pokerBase.DrawCard(true);
            pokerBase.DrawCard(false);
            pokerBase.DrawCard(true);
            pokerBase.DrawCard(false);
            pokerBase.DrawCard(true);
            pokerBase.DrawCard(false);
            pokerBase.DrawCard(true);
            pokerBase.DrawCard(false);
            
            lstPlayerHand.ItemsSource = pokerBase.hand;
            lstDealerHand.ItemsSource = pokerBase.dealerHand;

            CheckHands();

            CompareHands();
        }

        public void CompareHands()
        {
            if (playerRank > dealerRank)
            {
                PlayerWins();
            }
            else if (dealerRank > playerRank)
            {
                DealerWins();
            }
            else
            {
                Tied();
            }
        }

        public void CheckHands()
        {
            txtDealerTotal.Text = "Nothing";
            txtPlayerTotal.Text = "Nothing";

            playerRank = IsPair(ref pokerBase.hand) ? 1 : playerRank;
            playerRank = IsTwoPair(ref pokerBase.hand) ? 2 : playerRank;
            playerRank = IsThreeOfAKind(ref pokerBase.hand) ? 3 : playerRank;
            playerRank = IsStraight(ref pokerBase.hand) ? 4 : playerRank;
            playerRank = IsFlush(ref pokerBase.hand) ? 5 : playerRank;
            playerRank = IsFullHouse(ref pokerBase.hand) ? 6 : playerRank;
            playerRank = IsFourOfAKind(ref pokerBase.hand) ? 7 : playerRank;
            playerRank = IsStraightFlush(ref pokerBase.hand) ? 8 : playerRank;
            playerRank = IsRoyalFlush(ref pokerBase.hand) ? 9 : playerRank;

            txtPlayerTotal.Text = IsPair(ref pokerBase.hand) ? "A Pair" : txtPlayerTotal.Text;
            txtPlayerTotal.Text = IsTwoPair(ref pokerBase.hand) ? "Two Pair" : txtPlayerTotal.Text;
            txtPlayerTotal.Text = IsThreeOfAKind(ref pokerBase.hand) ? "Three of a Kind" : txtPlayerTotal.Text;
            txtPlayerTotal.Text = IsStraight(ref pokerBase.hand) ? "A Straight" : txtPlayerTotal.Text;
            txtPlayerTotal.Text = IsFlush(ref pokerBase.hand) ? "A Flush" : txtPlayerTotal.Text;
            txtPlayerTotal.Text = IsFullHouse(ref pokerBase.hand) ? "A Full House" : txtPlayerTotal.Text;
            txtPlayerTotal.Text = IsFourOfAKind(ref pokerBase.hand) ? "Four of a Kind" : txtPlayerTotal.Text;
            txtPlayerTotal.Text = IsStraightFlush(ref pokerBase.hand) ? "A Straight Flush" : txtPlayerTotal.Text;
            txtPlayerTotal.Text = IsRoyalFlush(ref pokerBase.hand) ? "A Royal Flush" : txtPlayerTotal.Text;

            dealerRank = IsPair(ref pokerBase.dealerHand) ? 1 : dealerRank;
            dealerRank = IsTwoPair(ref pokerBase.dealerHand) ? 2 : dealerRank;
            dealerRank = IsThreeOfAKind(ref pokerBase.dealerHand) ? 3 : dealerRank;
            dealerRank = IsStraight(ref pokerBase.dealerHand) ? 4 : dealerRank;
            dealerRank = IsFlush(ref pokerBase.dealerHand) ? 5 : dealerRank;
            dealerRank = IsFullHouse(ref pokerBase.dealerHand) ? 6 : dealerRank;
            dealerRank = IsFourOfAKind(ref pokerBase.dealerHand) ? 7 : dealerRank;
            dealerRank = IsStraightFlush(ref pokerBase.dealerHand) ? 8 : dealerRank;
            dealerRank = IsRoyalFlush(ref pokerBase.dealerHand) ? 9 : dealerRank;

            txtDealerTotal.Text = IsPair(ref pokerBase.dealerHand) ? "A Pair" : txtDealerTotal.Text;
            txtDealerTotal.Text = IsTwoPair(ref pokerBase.dealerHand) ? "Two Pair" : txtDealerTotal.Text;
            txtDealerTotal.Text = IsThreeOfAKind(ref pokerBase.dealerHand) ? "Three of a Kind" : txtDealerTotal.Text;
            txtDealerTotal.Text = IsStraight(ref pokerBase.dealerHand) ? "A Straight" : txtDealerTotal.Text;
            txtDealerTotal.Text = IsFlush(ref pokerBase.dealerHand) ? "A Flush" : txtDealerTotal.Text;
            txtDealerTotal.Text = IsFullHouse(ref pokerBase.dealerHand) ? "A Full House" : txtDealerTotal.Text;
            txtDealerTotal.Text = IsFourOfAKind(ref pokerBase.dealerHand) ? "Four of a Kind" : txtDealerTotal.Text;
            txtDealerTotal.Text = IsStraightFlush(ref pokerBase.dealerHand) ? "A Straight Flush" : txtDealerTotal.Text;
            txtDealerTotal.Text = IsRoyalFlush(ref pokerBase.dealerHand) ? "A Royal Flush" : txtDealerTotal.Text;
        }

        public bool IsStraightFlush(ref List<PokerBase.Card> cards)
        {
            return HasStraightFlush(ref cards) && !IsRoyalFlush(ref cards);
        }

        public bool IsRoyalStraight(ref List<PokerBase.Card> cards)
        {
            bool result = false;
            List<PokerBase.Card> newCards = cards.OrderBy(t => t.Value).ToList();

            if (newCards[0].Value == 1 && newCards[1].Value == 10 && IsStraight(ref cards))
            {
                result = true;
            }

            return result;
        }

        public bool IsRoyalFlush(ref List<PokerBase.Card> cards)
        {
            return IsRoyalStraight(ref cards)
                                       && HasStraightFlush(ref cards);
        }

        public bool HasStraightFlush(ref List<PokerBase.Card> cards)
        {
            return IsFlush(ref cards) && IsStraight(ref cards);
        }

        public bool IsFullHouse(ref List<PokerBase.Card> cards)
        {
            return HasThreeOfAKind(ref cards) && HasPair(ref cards);
        }

        public bool IsFourOfAKind(ref List<PokerBase.Card> cards)
        {
            return cards.GroupBy(card => card.Value)
                                            .Any(group => group.Count() == 4);
        }

        public bool IsFlush(ref List<PokerBase.Card> cards)
        {
            return cards.GroupBy(card => card.Suit).Count() == 1;
        }

        public bool IsThreeOfAKind(ref List<PokerBase.Card> cards)
        {
            return HasThreeOfAKind(ref cards) && !HasPair(ref cards);
        }

        public bool HasThreeOfAKind(ref List<PokerBase.Card> cards)
        {
            return cards.GroupBy(card => card.Value)
                                            .Any(group => group.Count() == 3);
        }

        public bool IsStraight(ref List<PokerBase.Card> cards)
        {
            bool result = false;
            List<PokerBase.Card> newCards = cards.OrderBy(t => t.Value).ToList();

            if (cards.Min(card => card.Value == 1))
            {
                newCards.RemoveAt(0);
                result = cards.GroupBy(card => card.Value)
                                            .Count() == cards.Count()
                                       && cards.Max(card => card.Value)
                                        - cards.Min(card => card.Value) == 3;
            }
            else
            {
                result = cards.GroupBy(card => card.Value)
                                                .Count() == cards.Count()
                                           && cards.Max(card => card.Value)
                                            - cards.Min(card => card.Value) == 4;
            }
            return result;
        }

        public bool IsTwoPair(ref List<PokerBase.Card> cards)
        {
            return cards.GroupBy(card => card.Value)
                                            .Count(group => group.Count() >= 2) == 2;
        }

        public bool IsPair(ref List<PokerBase.Card> cards)
        {
            return cards.GroupBy(card => card.Value)
                                            .Count(group => group.Count() == 3) == 0
                                       && HasPair(ref cards);
        }

        public bool HasPair(ref List<PokerBase.Card> cards)
        {
            return cards.GroupBy(card => card.Value)
                                            .Count(group => group.Count() == 2) == 1;
        }

        public void PlayerWins()
        {
            Reset();
            
            txtPlayerWins.Visibility = Visibility.Visible;
            txtPlayAgain.Visibility = Visibility.Visible;
        }

        public void DealerWins()
        {
            Reset();
            
            txtDealerWins.Visibility = Visibility.Visible;
            txtPlayAgain.Visibility = Visibility.Visible;
        }

        public void Tied()
        {
            Reset();

            txtTied.Visibility = Visibility.Visible;
            txtPlayAgain.Visibility = Visibility.Visible;
        }

        public void Reset()
        {
            txtDealerWins.Visibility = Visibility.Collapsed;
            txtPlayerWins.Visibility = Visibility.Collapsed;
            txtPlayAgain.Visibility = Visibility.Collapsed;
            txtTied.Visibility = Visibility.Collapsed;
        }

        public void FullReset()
        {
            pokerBase.CreateDeck();
            lstDealerHand.ItemsSource = null;
            lstPlayerHand.ItemsSource = null;
            pokerBase.hand.Clear();
            pokerBase.dealerHand.Clear();
            grdMain.Visibility = Visibility.Collapsed;
            playerRank = 0;
            dealerRank = 0;

            txtDealerWins.Visibility = Visibility.Collapsed;
            txtPlayerWins.Visibility = Visibility.Collapsed;
            txtPlayAgain.Visibility = Visibility.Collapsed;
            txtTied.Visibility = Visibility.Collapsed;
        }
    }
}
