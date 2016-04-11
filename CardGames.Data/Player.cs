using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Data
{
    public class Player
    {
        public string Name { get; set; }

        public IGameRole Role { get; set; }

        public HandStatus HandStatus { get; set; }

        public Hand Hand { get; private set; }

        public IBet Bettor;

        // public decimal Chips { get; set; }

        public Player(decimal chips = 0)
        {
            Hand = new Hand();
            Bettor = new BlackjackBettor(chips);
        }
        
    }
}
