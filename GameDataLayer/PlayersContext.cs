using GameCore.Classes;
using GameCore.Interfaces;
using System.Data.Entity;

namespace GameDataLayer
{
    public class PlayersContext : DbContext
    {
        public DbSet<Player> Players { get; set; }

    }
}
