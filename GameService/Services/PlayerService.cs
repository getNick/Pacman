using GameCore.Classes;
using GameCore.Interfaces;
using GameDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Services
{
    public class PlayerService
    {
        private IPlayersRepository playersRepository;
        public PlayerService()
        {
            //playersRepository= new PlayersRepository(new PlayersContext());
        }
        private IPlayer Player;
        public IPlayer  CreateNewPlayer(string name)
        {
            Player = new Player(name);
            return Player;
        }
        bool ChangeName(string newName)
        {
            if (newName.Length > 0)
            {
                Player.Name = newName;
                return true;
            }
            return false;
        }
        public void  SaveResult()
        {
            playersRepository.InsertPlayer(Player);
            playersRepository.Save();
        }
    }
}
