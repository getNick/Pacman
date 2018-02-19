using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.Models
{
    interface IPlayer
    {
        string Name { get; set; }
        int Score { get; set; }
    }
}
