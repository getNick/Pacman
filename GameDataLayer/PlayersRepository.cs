using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameCore.Classes;
using GameCore.Interfaces;

namespace GameDataLayer
{
    public class PlayersRepository : Repository<Player>,IPlayersRepository
    {
        public PlayersRepository(DbContext dbContext) : base(dbContext)
        {
        }
        public IQueryable<Player> GetTop(int count) {
            return _dbSet.OrderBy(x => x.Score).Take(count);
        }
    }
}
