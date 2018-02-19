using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.Models
{
    interface IEnemy:IMoveObject
    {
        IPacman Pacman { get;}
        
    }
}
