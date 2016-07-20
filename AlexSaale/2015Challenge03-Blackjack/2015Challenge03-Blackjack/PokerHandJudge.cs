using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2015Challenge03_Blackjack {
    public class PokerHandJudge {
        public enum PokerHand { HighCard, OnePair, TwoPair, ThreeOfAKind, Straight, Flush, FullHouse , FourOfAKind, StraightFlush, RoyalFlush }
        public Dictionary<PokerHand, int> HandRankings = new Dictionary<PokerHand, int>{
            { PokerHand.HighCard, 1 },
            { PokerHand.OnePair, 2 },
            { PokerHand.TwoPair, 3 },
            { PokerHand.ThreeOfAKind, 4 },
            { PokerHand.Straight, 5 },
            { PokerHand.Flush, 6 },
            { PokerHand.FullHouse, 7 },
            { PokerHand.FourOfAKind, 8 },
            { PokerHand.StraightFlush, 9 },
            { PokerHand.RoyalFlush, 10 }
        };

        public int JudgeHand(Hand hand) {
            var allOneSuit = hand.OrderBy(h => h.Suit).ToList()[0].Suit == hand.OrderBy(h => h.Suit).ToList()[hand.Count - 1].Suit;
            var orderedHand = hand.OrderBy(h => h.Face).ToList();
            var groupedHand = hand.GroupBy(h => h.Face).ToList();
            var lastCard = orderedHand.Count - 1;

            if (allOneSuit && orderedHand[0].Face == Face.Ten && orderedHand[lastCard].Face == Face.Ace)
                return HandRankings[PokerHand.RoyalFlush];

            if (allOneSuit && orderedHand[lastCard].PokerValue == orderedHand[0].PokerValue + 4)
                return HandRankings[PokerHand.StraightFlush];

            if (groupedHand.Any(g => g.Count() == 4))
                return HandRankings[PokerHand.FourOfAKind];

            if (groupedHand.Any(g => g.Count() == 3) && groupedHand.Any(g => g.Count() == 2))
                return HandRankings[PokerHand.FullHouse];

            if (allOneSuit)
                return HandRankings[PokerHand.Flush];

            if (orderedHand[lastCard].PokerValue == orderedHand[0].PokerValue + 4)
                return HandRankings[PokerHand.Straight];

            if (groupedHand.Any(g => g.Count() == 3))
                return HandRankings[PokerHand.ThreeOfAKind];

            if (groupedHand.Where(g => g.Count() == 2).Count() == 2)
                return HandRankings[PokerHand.TwoPair];

            if (groupedHand.Any(g => g.Count() == 2))
                return HandRankings[PokerHand.OnePair];

            return HandRankings[PokerHand.HighCard];
        }

        public string GetHandType(int value) {
            switch (value) {
                case 1:
                    return "High Card";
                case 2:
                    return "One Pair";
                case 3:
                    return "Two Pair";
                case 4:
                    return "Three of a Kind";
                case 5:
                    return "Straight";
                case 6:
                    return "Flush";
                case 7:
                    return "Full House";
                case 8:
                    return "Four of a Kind";
                case 9:
                    return "Straight Flush";
                case 10:
                    return "Royal Flush";
            }
            return "unknown...";
        }
    }
}
