using GameDataLayer.EF;
using GameDataLayer.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDataLayer.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private static Logger logger = LogManager.GetLogger("fileLogger");
        private readonly DbContext _dbContext;
        private bool _disposed;
        private PlayersRepository _playersRepository;

        public virtual PlayersRepository PlayersRepository
        {
            get { return _playersRepository ?? (_playersRepository = new PlayersRepository(_dbContext)); }
        }

        public EFUnitOfWork(string connectionString)
        {
            try
            {
                _dbContext = new PlayersContext(connectionString);
            }catch(Exception ex)
            {
                logger.Error(ex);
            }
        }


        public void Save()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
