using GameCore.Enums;
using GameCore.Interfaces;
using System;
using System.ComponentModel;
using System.Windows;

namespace GameCore.Classes
{
    public class MoveObject:GameObject,IMoveObject
    {
        public IMaze Maze { get;}
        public Direction Direction { get; set; }

        public MoveObject(int Row,int Cell, IMaze maze) :base(Row,Cell)
        {
            Maze = maze ?? throw new ArgumentNullException("Maze");
            Direction = Direction.Left;
        }

        public virtual bool Step()
        {
            bool succesfull = false;
            switch (Direction)
            {
                case Direction.Down:
                {
                    if(Maze.StepTo(Row - 1,Cell))
                    {
                        Row--;
                        succesfull = true;
                    }
                }break;
                case Direction.Up:
                {
                    if (Maze.StepTo(Row + 1, Cell))
                    {
                        Row++;
                        succesfull = true;
                    }
                }break;
                case Direction.Left:
                {
                    if (Maze.StepTo(Row, Cell-1))
                    {
                        Cell--;
                        succesfull = true;
                    }
                }break;
                case Direction.Right:
                {
                    if (Maze.StepTo(Row, Cell+1))
                    {
                        Cell++;
                        succesfull = true;
                     }
                }break;
            }
            return succesfull;
        }
        
    }
}
