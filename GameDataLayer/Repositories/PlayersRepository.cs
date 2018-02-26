using System.Data.Entity;
using System.Linq;
using GameCore.Classes;

namespace GameDataLayer.Repositories
{
    public class PlayersRepository : Repository<Player>
    {
        public PlayersRepository(DbContext dbContext) : base(dbContext)
        {
        }
        public IQueryable<Player> GetTop(int count) {
            return _dbSet.OrderBy(x => x.Score).Take(count);
        }
    }
}
