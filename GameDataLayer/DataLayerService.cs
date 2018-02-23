using GameCore.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDataLayer
{
    public class DataLayerService
    {
        DbContextFactory _dbContextFactory;
        public IUnitOfWork UnitOfWork;
        public IPlayersRepository PlayersRepository;
        public DataLayerService()
        {
            _dbContextFactory = new DbContextFactory();
            UnitOfWork = new UnitOfWork(_dbContextFactory);
            PlayersRepository = UnitOfWork.PlayersRepository;
        }
    }
}
