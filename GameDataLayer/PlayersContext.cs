using GameCore.Interfaces;
using System.Data.Entity;

namespace GameDataLayer
{
    public class PlayersContext : DbContext
    {
        public DbSet<IPlayer> Players { get; set; }

    }
}
