using GameCore.Classes;
using GameCore.Interfaces;
using System.Data.Entity;

namespace GameDataLayer.EF
{
    public class PlayersContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        
        public PlayersContext(string connectionString)
            : base(connectionString)
        {
        }
    }
}
