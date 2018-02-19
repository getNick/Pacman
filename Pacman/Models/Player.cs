using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.Models
{
    class Player : IPlayer
    {
        public Player(string name)
        {
            Name = name;
            Score = 0;
        }
        public string Name { get; set; }
        public int Score { get; set; }
    }
}
