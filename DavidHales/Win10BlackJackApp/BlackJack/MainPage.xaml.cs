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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BlackJack
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private List<Card> deck = new List<Card>();
        private static Random random = new Random();
        public List<Card> hand = new List<Card>();
        public List<Card> dealerHand = new List<Card>();
        public int playerTotal = 0;
        public int dealerTotal = 0;
        public bool playerBlackJack = false;
        public bool dealerBlackJack = false;
        public bool bust = false;

        public MainPage()
        {
            this.InitializeComponent();
            CreateDeck();
            txtPlayerTotal.Text = playerTotal.ToString();
            txtDealerTotal.Text = "Hidden";
        }

        private void btnDeal_Click(object sender, RoutedEventArgs e)
        {
            FullReset();
            
            Shuffle();
            Deal();
            grdMain.Visibility = Visibility.Visible;

            btnDeal.IsEnabled = false;
            btnPlayerHit.IsEnabled = true;
            btnPlayerStand.IsEnabled = true;
        }

        protected void Shuffle()
        {
            if (deck.Count > 1)
            {
                for (int i = deck.Count - 1; i >= 0; i--)
                {
                    Card tmp = deck[i];
                    int randomIndex = random.Next(i + 1);

                    //Swap elements
                    deck[i] = deck[randomIndex];
                    deck[randomIndex] = tmp;
                }
            }
        }

        protected void Deal()
        {
            var firstCard = deck[0];
            var secondCard = deck[1];
            var thirdCard = deck[2];
            var fourthCard = deck[3];

            hand.Add(firstCard);
            playerTotal = playerTotal + firstCard.Value;
            deck.Remove(firstCard);
            
            dealerHand.Add(secondCard);
            dealerTotal = dealerTotal + secondCard.Value;
            deck.Remove(secondCard);

            hand.Add(thirdCard);
            playerTotal = playerTotal + thirdCard.Value;
            deck.Remove(thirdCard);
            
            dealerHand.Add(fourthCard);
            dealerTotal = dealerTotal + fourthCard.Value;
            deck.Remove(fourthCard);

            List<Card> tempDealerHand = new List<Card>();

            tempDealerHand.Add(new Card { CardName = "Hidden", Abbreviation = "H1", Value = 0 });
            tempDealerHand.Add(new Card { CardName = "Hidden", Abbreviation = "H2", Value = 0 });

            lstPlayerHand.ItemsSource = hand;
            lstDealerHand.ItemsSource = tempDealerHand;

            txtPlayerTotal.Text = playerTotal.ToString();

            CheckForBlackJack();
        }

        protected void CheckForBlackJack()
        {
            if (playerTotal == 21)
            {
                playerBlackJack = true;
                PlayerWins();
            }
            else if (dealerTotal == 21)
            {
                dealerBlackJack = true;
                DealerWins();
            }
        } 

        protected void CheckForBust(bool player)
        {
            if (player)
            {
                if (playerTotal > 21)
                {
                    PlayerBusts();
                    bust = true;
                }
            }
            else
            {
                if (dealerTotal > 21)
                {
                    DealerBusts();
                    bust = true;
                }
            }
        }

        private void CreateDeck()
        {
            deck.Clear();

            deck.Add(new Card { CardName = "Ace of Hearts", Abbreviation = "AH", Value = 11 });
            deck.Add(new Card { CardName = "King of Hearts", Abbreviation = "KH", Value = 10 });
            deck.Add(new Card { CardName = "Queen of Hearts", Abbreviation = "QH", Value = 10 });
            deck.Add(new Card { CardName = "Jack of Hearts", Abbreviation = "JH", Value = 10 });
            deck.Add(new Card { CardName = "Ten of Hearts", Abbreviation = "10H", Value = 10 });
            deck.Add(new Card { CardName = "Nine of Hearts", Abbreviation = "9H", Value = 9 });
            deck.Add(new Card { CardName = "Eight of Hearts", Abbreviation = "8H", Value = 8 });
            deck.Add(new Card { CardName = "Seven of Hearts", Abbreviation = "7H", Value = 7 });
            deck.Add(new Card { CardName = "Six of Hearts", Abbreviation = "6H", Value = 6 });
            deck.Add(new Card { CardName = "Five of Hearts", Abbreviation = "5H", Value = 5 });
            deck.Add(new Card { CardName = "Four of Hearts", Abbreviation = "4H", Value = 4 });
            deck.Add(new Card { CardName = "Three of Hearts", Abbreviation = "3H", Value = 3 });
            deck.Add(new Card { CardName = "Two of Hearts", Abbreviation = "2H", Value = 2 });

            deck.Add(new Card { CardName = "Ace of Clubs", Abbreviation = "AC", Value = 11 });
            deck.Add(new Card { CardName = "King of Clubs", Abbreviation = "KC", Value = 10 });
            deck.Add(new Card { CardName = "Queen of Clubs", Abbreviation = "QC", Value = 10 });
            deck.Add(new Card { CardName = "Jack of Clubs", Abbreviation = "JC", Value = 10 });
            deck.Add(new Card { CardName = "Ten of Clubs", Abbreviation = "10C", Value = 10 });
            deck.Add(new Card { CardName = "Nine of Clubs", Abbreviation = "9C", Value = 9 });
            deck.Add(new Card { CardName = "Eight of Clubs", Abbreviation = "8C", Value = 8 });
            deck.Add(new Card { CardName = "Seven of Clubs", Abbreviation = "7C", Value = 7 });
            deck.Add(new Card { CardName = "Six of Clubs", Abbreviation = "6C", Value = 6 });
            deck.Add(new Card { CardName = "Five of Clubs", Abbreviation = "5C", Value = 5 });
            deck.Add(new Card { CardName = "Four of Clubs", Abbreviation = "4C", Value = 4 });
            deck.Add(new Card { CardName = "Three of Clubs", Abbreviation = "3C", Value = 3 });
            deck.Add(new Card { CardName = "Two of Clubs", Abbreviation = "2C", Value = 2 });

            deck.Add(new Card { CardName = "Ace of Spades", Abbreviation = "AS", Value = 11 });
            deck.Add(new Card { CardName = "King of Spades", Abbreviation = "KS", Value = 10 });
            deck.Add(new Card { CardName = "Queen of Spades", Abbreviation = "QS", Value = 10 });
            deck.Add(new Card { CardName = "Jack of Spades", Abbreviation = "JS", Value = 10 });
            deck.Add(new Card { CardName = "Ten of Spades", Abbreviation = "10S", Value = 10 });
            deck.Add(new Card { CardName = "Nine of Spades", Abbreviation = "9S", Value = 9 });
            deck.Add(new Card { CardName = "Eight of Spades", Abbreviation = "8S", Value = 8 });
            deck.Add(new Card { CardName = "Seven of Spades", Abbreviation = "7S", Value = 7 });
            deck.Add(new Card { CardName = "Six of Spades", Abbreviation = "6S", Value = 6 });
            deck.Add(new Card { CardName = "Five of Spades", Abbreviation = "5S", Value = 5 });
            deck.Add(new Card { CardName = "Four of Spades", Abbreviation = "4S", Value = 4 });
            deck.Add(new Card { CardName = "Three of Spades", Abbreviation = "3S", Value = 3 });
            deck.Add(new Card { CardName = "Two of Spades", Abbreviation = "2S", Value = 2 });

            deck.Add(new Card { CardName = "Ace of Diamonds", Abbreviation = "AD", Value = 11 });
            deck.Add(new Card { CardName = "King of Diamonds", Abbreviation = "KD", Value = 10 });
            deck.Add(new Card { CardName = "Queen of Diamonds", Abbreviation = "QD", Value = 10 });
            deck.Add(new Card { CardName = "Jack of Diamonds", Abbreviation = "JD", Value = 10 });
            deck.Add(new Card { CardName = "Ten of Diamonds", Abbreviation = "10D", Value = 10 });
            deck.Add(new Card { CardName = "Nine of Diamonds", Abbreviation = "9D", Value = 9 });
            deck.Add(new Card { CardName = "Eight of Diamonds", Abbreviation = "8D", Value = 8 });
            deck.Add(new Card { CardName = "Seven of Diamonds", Abbreviation = "7D", Value = 7 });
            deck.Add(new Card { CardName = "Six of Diamonds", Abbreviation = "6D", Value = 6 });
            deck.Add(new Card { CardName = "Five of Diamonds", Abbreviation = "5D", Value = 5 });
            deck.Add(new Card { CardName = "Four of Diamonds", Abbreviation = "4D", Value = 4 });
            deck.Add(new Card { CardName = "Three of Diamonds", Abbreviation = "3D", Value = 3 });
            deck.Add(new Card { CardName = "Two of Diamonds", Abbreviation = "2D", Value = 2 });

        }
        public class Card
        {
            public string CardName { get; set; }
            public string Abbreviation { get; set; }
            public int Value { get; set; }
        }

        private void btnPlayerHit_Click(object sender, RoutedEventArgs e)
        {
            DrawCard(true);
        }

        private void btnPlayerStand_Click(object sender, RoutedEventArgs e)
        {
            btnPlayerHit.IsEnabled = false;
            btnPlayerStand.IsEnabled = false;

            lstDealerHand.ItemsSource = null;
            lstDealerHand.ItemsSource = dealerHand;

            txtDealerTotal.Text = dealerTotal.ToString();

            ProcessDealer();

            if (!bust)
            {
                if (playerTotal >= dealerTotal)
                {
                    PlayerWins();
                }
                else
                {
                    DealerWins();
                }
            }
        }

        protected void ProcessDealer()
        {
            if (dealerTotal < 17)
            {
                DrawCard(false);

                ProcessDealer();
            }
        }

        protected void DrawCard(bool player)
        {
            var firstCard = deck[0];

            if (player)
            {
                hand.Add(firstCard);
                playerTotal = playerTotal + firstCard.Value;
                deck.Remove(firstCard);

                lstPlayerHand.ItemsSource = null;
                lstPlayerHand.ItemsSource = hand;

                txtPlayerTotal.Text = playerTotal.ToString();
            }
            else
            {
                dealerHand.Add(firstCard);
                dealerTotal = dealerTotal + firstCard.Value;
                deck.Remove(firstCard);

                lstDealerHand.ItemsSource = null;
                lstDealerHand.ItemsSource = dealerHand;

                txtDealerTotal.Text = dealerTotal.ToString();
            }

            CheckForBlackJack();

            CheckForBust(player);
        }

        protected void PlayerWins()
        {
            Reset();

            if (playerBlackJack)
            {
                txtPlayerWinsBlackJack.Visibility = Visibility.Visible;
                txtPlayAgain.Visibility = Visibility.Visible;
                lstDealerHand.ItemsSource = null;
                lstDealerHand.ItemsSource = dealerHand;

                txtDealerTotal.Text = dealerTotal.ToString();
            }
            else
            {
                txtPlayerWins.Visibility = Visibility.Visible;
                txtPlayAgain.Visibility = Visibility.Visible;
            }
        }

        protected void PlayerBusts()
        {
            Reset();

            txtPlayerBusts.Visibility = Visibility.Visible;
            txtPlayAgain.Visibility = Visibility.Visible;
        }

        protected void DealerWins()
        {
            Reset();

            if (dealerBlackJack)
            {
                txtDealerWinsBlackJack.Visibility = Visibility.Visible;
                txtPlayAgain.Visibility = Visibility.Visible;
                lstDealerHand.ItemsSource = null;
                lstDealerHand.ItemsSource = dealerHand;

                txtDealerTotal.Text = dealerTotal.ToString();
            }
            else
            {
                txtDealerWins.Visibility = Visibility.Visible;
                txtPlayAgain.Visibility = Visibility.Visible;
            }
        }

        protected void DealerBusts()
        {
            Reset();

            txtDealerBusts.Visibility = Visibility.Visible;
            txtPlayAgain.Visibility = Visibility.Visible;
        }

        protected void Reset()
        {
            btnPlayerStand.IsEnabled = false;
            btnPlayerHit.IsEnabled = false;
            btnDeal.IsEnabled = true;

            txtDealerBusts.Visibility = Visibility.Collapsed;
            txtPlayerBusts.Visibility = Visibility.Collapsed;
            txtDealerWins.Visibility = Visibility.Collapsed;
            txtPlayerWins.Visibility = Visibility.Collapsed;
            txtPlayAgain.Visibility = Visibility.Collapsed;
            txtDealerWinsBlackJack.Visibility = Visibility.Collapsed;
            txtPlayerWinsBlackJack.Visibility = Visibility.Collapsed;
        }

        protected void FullReset()
        {
            CreateDeck();
            lstDealerHand.ItemsSource = null;
            lstPlayerHand.ItemsSource = null;
            hand.Clear();
            dealerHand.Clear();
            playerTotal = 0;
            dealerTotal = 0;
            txtDealerTotal.Text = "Hidden";
            grdMain.Visibility = Visibility.Collapsed;
            bust = false;

            txtDealerBusts.Visibility = Visibility.Collapsed;
            txtDealerWinsBlackJack.Visibility = Visibility.Collapsed;
            txtPlayerWinsBlackJack.Visibility = Visibility.Collapsed;
            txtPlayerBusts.Visibility = Visibility.Collapsed;
            txtDealerWins.Visibility = Visibility.Collapsed;
            txtPlayerWins.Visibility = Visibility.Collapsed;
            txtPlayAgain.Visibility = Visibility.Collapsed;

            btnPlayerStand.IsEnabled = false;
            btnPlayerHit.IsEnabled = false;
            btnDeal.IsEnabled = true;
        }
    }
}
