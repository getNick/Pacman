using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pacman.Models
{
    class MoveObject:GameObject,IMoveObject
    {
        public IMaze Maze { get;}
        public Direction Direction { get; set; }

        public MoveObject(int Row,int Cell, IMaze maze) :base(Row,Cell)
        {
            if (maze == null)
            {
                throw new ArgumentNullException("Maze");
            }
            Maze = maze;
            Direction = Direction.Left;
        }
        public virtual bool Step()
        {
            bool succesfull = false;
            switch (Direction)
            {
                case Direction.Down:
                {
                    if(Maze.StepTo((int)GridPosition.X - 1, (int)GridPosition.Y))
                    {
                        GridPosition = new Vector(GridPosition.X - 1, GridPosition.Y);
                        succesfull = true;
                        //pause
                    }
                }break;
                case Direction.Up:
                {
                    if (Maze.StepTo((int)GridPosition.X + 1, (int)GridPosition.Y))
                    {
                        GridPosition = new Vector(GridPosition.X + 1, GridPosition.Y);
                        succesfull = true;
                            //pause
                    }
                }break;
                case Direction.Left:
                {
                    if (Maze.StepTo((int)GridPosition.X, (int)GridPosition.Y-1))
                    {
                        GridPosition = new Vector(GridPosition.X, GridPosition.Y-1);
                        succesfull = true;
                            //pause
                    }
                }break;
                case Direction.Right:
                {
                    if (Maze.StepTo((int)GridPosition.X, (int)GridPosition.Y+1))
                    {
                        GridPosition = new Vector(GridPosition.X, GridPosition.Y+1);
                        succesfull = true;
                            //pause
                     }
                }break;
            }
            return succesfull;
        }

    }
}
