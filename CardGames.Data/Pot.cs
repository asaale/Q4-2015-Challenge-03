using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Data
{
    public class Pot
    {
        private static Pot _instance;
        public static Pot Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Pot();
                }

                return _instance;
            }
        }

        public decimal Chips { get; set; }

        private Pot()
        {
            Chips = 0;
        }

        //public static void CreateNewPot()
        //{
        //    _instance = new Pot();
        //}

        public void AddChips(decimal val)
        {
            Chips += val;
        }
        
        public void TakePot(IBet bettor)
        {
            var bettors = new List<IBet>();
            bettors.Add(bettor);
            TakePot(bettors);
        }

        public void TakePot(List<IBet> bettors)
        {
            int pennies = Convert.ToInt32(Chips * 100.0M);
            decimal shareInPennies = pennies / bettors.Count;   // fractions are discarded.  This is a game, not Superman 3 or Office Space.
            decimal shareInDollars = decimal.Round(shareInPennies / 100.0M, 2);

            foreach (var bettor in bettors)
            {
                bettor.TakePot(shareInDollars);
            }
            Chips = 0.0M;
        }
    }
}
