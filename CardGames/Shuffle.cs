using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Transactions
{
    using Data;

    public class Shuffle : Transaction
    {
        public Player Shuffler { get; set; }

        public Shuffle(Player shuffler)
        {
            Shuffler = shuffler;
            Shuffler.Role = new ShufflerRole();
        }

        public override void Execute()
        {
            Shuffler.Role.PerformRole();
        }
    }
}
