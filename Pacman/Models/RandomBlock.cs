using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pacman.Models
{
    class RandomBlock
    {
        static int count = 0;
        static Random rnd = new Random();
        public int Height { get; private set;}
        public int Width { get; private set; }
        public IEnumerable<Wall> ListWalls;
        public RandomBlock()
        {
            count++;
            List<Wall> figure = new List<Wall>();
            int lenght = rnd.Next(3, 4);
            for (int i = 0; i < lenght; i++)
            {
                figure.Add(new Wall(i, 0));
            }
            int countPoints = (int)Math.Ceiling(lenght / 2.0);
            int index = 0;
            int tempLenght;
            bool positive = false;
            for (int i = 0; i < countPoints; i++)
            {
                tempLenght = rnd.Next(3);
                if (rnd.Next(2) == 1)
                {
                    positive = true;
                }
                for (int j = 0; j < tempLenght; j++)
                {
                    if (positive)
                    {
                        figure.Add(new Wall(index, j + 1));
                    }
                    else
                    {
                        figure.Add(new Wall(index, -(j + 1)));
                    }
                }
                index = rnd.Next(lenght - 1, lenght);
            }
            Normalize(figure);
            if (rnd.Next(2) == 1)
            {
                TransposeFigure(figure);
            }
            Height = (int)figure.Max((f) => f.GridPosition.X);
            Width = (int)figure.Max((f) => f.GridPosition.Y);
            ListWalls = figure;
        }
        private static void TransposeFigure(List<Wall> figure)
        {
            foreach (var wall in figure)
            {
                wall.GridPosition = new Vector(wall.GridPosition.Y, wall.GridPosition.X);
            }
        }
        private static void Normalize(List<Wall> figure)
        {
            var offset = -figure.Min((f) => f.GridPosition.Y);
            if (offset == 0)
            {
                return;
            }
            foreach(var wall in figure)
            {
                wall.GridPosition = new Vector(wall.GridPosition.X, wall.GridPosition.Y + offset);
            }
        }
    }
    
}
