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
    public sealed partial class BlackJack : Page
    {
        PokerBase pokerBase = new PokerBase();
        public bool playerBlackJack = false;
        public bool dealerBlackJack = false;
        public bool bust = false;

        public BlackJack()
        {
            this.InitializeComponent();
            pokerBase.CreateDeck();
            txtPlayerTotal.Text = pokerBase.playerTotal.ToString();
            txtDealerTotal.Text = "Hidden";
        }

        private void btnDeal_Click(object sender, RoutedEventArgs e)
        {
            FullReset();
            pokerBase.Shuffle();
            Deal();
            grdMain.Visibility = Visibility.Visible;

            if (!playerBlackJack && !dealerBlackJack && !bust)
            {
                btnDeal.IsEnabled = false;
                btnPlayerHit.IsEnabled = true;
                btnPlayerStand.IsEnabled = true;
            }
        }

        public void Deal()
        {
            pokerBase.DrawCard(true);
            pokerBase.DrawCard(false);
            pokerBase.DrawCard(true);
            pokerBase.DrawCard(false);

            List<PokerBase.Card> tempDealerHand = new List<PokerBase.Card>();

            tempDealerHand.Add(new PokerBase.Card { CardName = "Hidden", Abbreviation = "H1", Value = 0 });
            tempDealerHand.Add(new PokerBase.Card { CardName = "Hidden", Abbreviation = "H2", Value = 0 });
            
            lstPlayerHand.ItemsSource = pokerBase.hand;
            lstDealerHand.ItemsSource = tempDealerHand;

            txtPlayerTotal.Text = pokerBase.playerTotal.ToString();

            CheckForBlackJack();
            CheckForBust(true);
            CheckForBust(false);
        }

        public void CheckForBlackJack()
        {
            if (pokerBase.playerTotal == 21)
            {
                playerBlackJack = true;
                PlayerWins();
            }
            else if (pokerBase.dealerTotal == 21)
            {
                dealerBlackJack = true;
                DealerWins();
            }
        }

        public void CheckForBust(bool player)
        {
            if (player)
            {
                if (pokerBase.playerTotal > 21)
                {
                    PlayerBusts();
                    bust = true;
                }
            }
            else
            {
                if (pokerBase.dealerTotal > 21)
                {
                    DealerBusts();
                    bust = true;
                }
            }
        }

        private void btnPlayerHit_Click(object sender, RoutedEventArgs e)
        {
            pokerBase.DrawCard(true);

            lstPlayerHand.ItemsSource = null;
            lstPlayerHand.ItemsSource = pokerBase.hand;

            txtPlayerTotal.Text = pokerBase.playerTotal.ToString();

            CheckForBlackJack();
            CheckForBust(true);
        }

        private void btnPlayerStand_Click(object sender, RoutedEventArgs e)
        {
            btnPlayerHit.IsEnabled = false;
            btnPlayerStand.IsEnabled = false;

            lstDealerHand.ItemsSource = null;
            lstDealerHand.ItemsSource = pokerBase.dealerHand;

            txtDealerTotal.Text = pokerBase.dealerTotal.ToString();

            ProcessDealer();

            if (!bust)
            {
                if (pokerBase.playerTotal >= pokerBase.dealerTotal)
                {
                    PlayerWins();
                }
                else
                {
                    DealerWins();
                }
            }
        }

        public void ProcessDealer()
        {
            if (pokerBase.dealerTotal < 17)
            {
                pokerBase.DrawCard(false);

                lstDealerHand.ItemsSource = null;
                lstDealerHand.ItemsSource = pokerBase.dealerHand;

                txtDealerTotal.Text = pokerBase.dealerTotal.ToString();

                CheckForBlackJack();
                CheckForBust(false);

                ProcessDealer();
            }
        }

        

        public void PlayerWins()
        {
            Reset();

            if (playerBlackJack)
            {
                txtPlayerWinsBlackJack.Visibility = Visibility.Visible;
                txtPlayAgain.Visibility = Visibility.Visible;
                lstDealerHand.ItemsSource = null;
                lstDealerHand.ItemsSource = pokerBase.dealerHand;

                txtDealerTotal.Text = pokerBase.dealerTotal.ToString();
            }
            else
            {
                txtPlayerWins.Visibility = Visibility.Visible;
                txtPlayAgain.Visibility = Visibility.Visible;
            }
        }

        public void PlayerBusts()
        {
            Reset();

            txtPlayerBusts.Visibility = Visibility.Visible;
            txtPlayAgain.Visibility = Visibility.Visible;
        }

        public void DealerWins()
        {
            Reset();

            if (dealerBlackJack)
            {
                txtDealerWinsBlackJack.Visibility = Visibility.Visible;
                txtPlayAgain.Visibility = Visibility.Visible;
                lstDealerHand.ItemsSource = null;
                lstDealerHand.ItemsSource = pokerBase.dealerHand;

                txtDealerTotal.Text = pokerBase.dealerTotal.ToString();
            }
            else
            {
                txtDealerWins.Visibility = Visibility.Visible;
                txtPlayAgain.Visibility = Visibility.Visible;
            }
        }

        public void DealerBusts()
        {
            Reset();

            txtDealerBusts.Visibility = Visibility.Visible;
            txtPlayAgain.Visibility = Visibility.Visible;
        }

        public void Reset()
        {
            txtDealerBusts.Visibility = Visibility.Collapsed;
            txtPlayerBusts.Visibility = Visibility.Collapsed;
            txtDealerWins.Visibility = Visibility.Collapsed;
            txtPlayerWins.Visibility = Visibility.Collapsed;
            txtPlayAgain.Visibility = Visibility.Collapsed;
            txtDealerWinsBlackJack.Visibility = Visibility.Collapsed;
            txtPlayerWinsBlackJack.Visibility = Visibility.Collapsed;

            btnPlayerStand.IsEnabled = false;
            btnPlayerHit.IsEnabled = false;
            btnDeal.IsEnabled = true;
        }

        public void FullReset()
        {
            pokerBase.CreateDeck();
            lstDealerHand.ItemsSource = null;
            lstPlayerHand.ItemsSource = null;
            pokerBase.hand.Clear();
            pokerBase.dealerHand.Clear();
            pokerBase.playerTotal = 0;
            pokerBase.dealerTotal = 0;
            txtDealerTotal.Text = "Hidden";
            grdMain.Visibility = Visibility.Collapsed;
            bust = false;
            playerBlackJack = false;
            dealerBlackJack = false;

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
