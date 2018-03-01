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
            UnitOfWork = new EFUnitOfWork("Records");
        }
        public void AddRecord(IPlayer player)
        {
            var Player = player as Player;
            if (Player != null)
            {
                UnitOfWork.PlayersRepository.Create(Player);
                UnitOfWork.Save();
            }
            
        }
        public IEnumerable<IPlayer> GetTop(int count)
        {
            var list = UnitOfWork.PlayersRepository.GetTop(count).ToList();
            return list;
        }
    }
}
