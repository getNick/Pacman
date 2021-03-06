﻿using GameCore.EnumsAndConstant;
using GameCore.Interfaces;
using System;

namespace GameCore.Classes
{
    public class MoveObject:GameObject,IMoveObject
    {
        public IMaze Maze { get;}
        private Direction _direction;
        public Direction Direction
        {
            get
            {
                return _direction;
            }
            set
            {
                _direction = value;
                OnPropertyChanged("Direction");
            }
        }

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
