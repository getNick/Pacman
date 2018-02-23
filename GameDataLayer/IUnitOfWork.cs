using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDataLayer
{
    public interface IUnitOfWork : IDisposable
    {
        PlayersRepository PlayersRepository { get; }
        void Save();
    }


}
