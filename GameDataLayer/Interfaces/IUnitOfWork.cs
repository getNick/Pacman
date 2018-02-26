using GameDataLayer.Repositories;
using System;

namespace GameDataLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        PlayersRepository PlayersRepository { get; }
        void Save();
    }


}
