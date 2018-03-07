﻿using GameDataLayer.EF;
using GameDataLayer.Interfaces;
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
        private readonly DbContext _dbContext;
        private bool _disposed;
        private PlayersRepository _playersRepository;

        public PlayersRepository PlayersRepository
        {
            get { return _playersRepository ?? (_playersRepository = new PlayersRepository(_dbContext)); }
        }

        public EFUnitOfWork(string connectionString)
        {
            _dbContext = new PlayersContext(connectionString);
        }


        public void Save()
        {
            _dbContext.SaveChanges();
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