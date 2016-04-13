using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Data
{
    public interface IGameRole
    {
        void InitRole(object obj);

        void PerformRole();

        void DisposeRole();
    }
}
