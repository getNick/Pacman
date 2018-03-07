using GameCore.Classes;
using GameCore.Interfaces;
using GameDataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameService.Services
{
    public class DataLayerService
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public DataLayerService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException("UnitOfWork");
        }
        public void AddRecord(IPlayer player)
        {
            if (player is Player Player)
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
