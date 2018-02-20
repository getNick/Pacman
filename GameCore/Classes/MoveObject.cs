using GameCore.Enums;
using GameCore.Interfaces;
using System;
using System.Windows;

namespace GameCore.Classes
{
    public class MoveObject:GameObject,IMoveObject
    {
        public IMaze Maze { get;}
        public Direction Direction { get; set; }
        private DateTime _lastStepTime;
        public MoveObject(int Row,int Cell, IMaze maze) :base(Row,Cell)
        {
            Maze = maze ?? throw new ArgumentNullException("Maze");
            Direction = Direction.Left;
            _lastStepTime = DateTime.Now;
        }
        public virtual bool Step()
        {
            DateTime timeNow = DateTime.Now;
            if ((timeNow - _lastStepTime).TotalMilliseconds > 500)
            {
                return false;
            }
            bool succesfull = false;
            switch (Direction)
            {
                case Direction.Down:
                {
                    if(Maze.StepTo((int)GridPosition.X - 1, (int)GridPosition.Y))
                    {
                        GridPosition = new Vector(GridPosition.X - 1, GridPosition.Y);
                        succesfull = true;
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
