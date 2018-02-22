using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameCore.Interfaces;

namespace GameDataLayer
{
    public class PlayersRepository : IPlayersRepository, IDisposable
    {

        private PlayersContext context;

        public PlayersRepository(PlayersContext context)
        {
            this.context = context;
        }

        public IEnumerable<IPlayer> GetPlayers()
        {
            return context.Players.ToList();
        }

        public void InsertPlayer(IPlayer player)
        {
            context.Players.Add(player);
        }

        public void Save()
        {
            context.SaveChanges();
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
