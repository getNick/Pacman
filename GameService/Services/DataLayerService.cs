using GameCore.Classes;
using GameCore.Interfaces;
using GameDataLayer.Interfaces;
using GameDataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Services
{
    public class DataLayerService
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public DataLayerService()
        {
            UnitOfWork = new EFUnitOfWork("Reconds");
        }
        public void AddRecord(Player player)
        {
            UnitOfWork.PlayersRepository.Create(player);
            UnitOfWork.Save();
        }
        public IEnumerable<Player> GetTop(int count)
        {
            var list = UnitOfWork.PlayersRepository.GetTop(count).ToList();
            return list;
        }
    }
}
