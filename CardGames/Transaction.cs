using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Transactions
{
    public abstract class Transaction
    {
        public abstract void Execute();
    }
}
