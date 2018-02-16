using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pacman.Models
{
    class Wall:GameObject
    {
        private List<Vector> ListBricks;
        public Wall(int Row,int Cell):base(Row,Cell)
        {
            
        }
        public List<Vector> GetEnvironment(GameObject[,] maze)
        {
            int j = (int)GridPosition.Y;
            int i = (int)GridPosition.X;

            int rows = maze.GetUpperBound(0) + 1;
            int columns = maze.Length / rows;
            ListBricks = new List<Vector>();
            ListBricks.Add(GridPosition);
            bool Up = true;
            bool Down = true;
            bool Right = true;
            bool Left = true;
            double third = 1 / 3.0;
            if (i == 0)
            {
                Up = false;
            }else if (i == rows - 1)
            {
                Down = false;
            }

            if ((Down)&&(maze[i + 1, j] is Wall))
            {
                ListBricks.Add(new Vector(i + third, j));
            }
            if ((Up)&&(maze[i - 1, j] is Wall))
            {
                ListBricks.Add(new Vector(i - third, j));
            }


            if (j == 0)
            {
                Left = false;
            }
            else if (j == columns - 1)
            {
                Right = false;
            }
            if ((Left) && (maze[i, j-1] is Wall))
            {
                ListBricks.Add(new Vector(i, j-third));
            }
            if ((Right) && (maze[i, j+1] is Wall))
            {
                ListBricks.Add(new Vector(i, j+third));
            }
            return ListBricks;
        }
    }
}
