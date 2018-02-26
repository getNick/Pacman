using GameCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.Classes
{
    public class DefaultGift : IGift
    {
        private IPlayer Player;
        public DefaultGift(IPlayer player)
        {
            Player = player ?? throw new ArgumentNullException("Player");
        }
        public void Activate()
        {
            Player.AddToScore(1);

        }
    }
}
