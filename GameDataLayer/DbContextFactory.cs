using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDataLayer
{
    class DbContextFactory : IDbContextFactory
    {
        private readonly DbContext _context;

        public DbContextFactory()
        {
            _context = new PlayersContext();
        }

        public DbContext GetDbContext()
        {
            return _context;
        }
    }

}
