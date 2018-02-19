using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pacman.Models
{
    class Enemy :MoveObject,IEnemy
    {
        public Enemy(int Row, int Cell, IMaze maze,IPacman pacman) : base(Row, Cell, maze)
        {
            if (pacman == null)
            {
                throw new ArgumentNullException("Pacman");
            }
            Pacman = pacman;
        }

        public IPacman Pacman { get;}
        
        public override bool Step()
        {
            /*bool succesfull = false;
            while (!succesfull)
            {
                if (base.Step())
                {
                    if (GridPosition == Pacman.GridPosition)
                    {
                        Pacman.UseAdditionalLife();
                        return true;
                    }
                }
                else
                {
                    if (GridPosition.X < Pacman.GridPosition.X)
                    {
                        Direction = Direction.Up;
                    }else if (GridPosition.X > Pacman.GridPosition.X)
                    {
                        Direction = Direction.Down;
                    }
                }
            }*/
            return false;
        }
    }
}
