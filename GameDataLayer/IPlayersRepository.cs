using GameCore.Classes;
using GameCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDataLayer
{
    public interface IPlayersRepository:IRepository<Player>
    {
        IQueryable<Player> GetTop(int count);
    }
}
