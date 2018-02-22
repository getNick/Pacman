using GameCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDataLayer
{
    public interface IPlayersRepository
    {
        IEnumerable<IPlayer> GetPlayers();
        void InsertPlayer(IPlayer player);
        void Save();
    }
}
