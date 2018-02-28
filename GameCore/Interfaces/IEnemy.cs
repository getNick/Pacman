using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameCore.Interfaces
{
    public interface IEnemy : IMoveObject
    {
        IPacman Pacman { get;}
        IPursueAlgo PursueAlgo {get;}

    }
}
