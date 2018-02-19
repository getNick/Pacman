using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pacman.Models
{
    class NewEnemy : IEnemy
    {
        public NewEnemy(int Row, int Cell, IMaze maze,IPacman pacman)
        {
            Pacman = pacman;
            Maze = maze;
            GridPosition = new Vector(Row, Cell);
        }

        public IPacman Pacman { get; }

        public Direction Direction { get; set; }

        public IMaze Maze { get; }

        public Vector GridPosition { get; set; }

        public bool Step()
        {
            
            return false;
        }
    }
}
