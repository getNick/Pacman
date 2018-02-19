using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.Models
{
    public enum Direction
    {
        Up, Down, Left, Right
    }
    interface IMoveObject:IGameObject
    {
        Direction Direction { get; set; }
        IMaze Maze { get;}
        bool Step();
    }
}
