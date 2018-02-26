using GameCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;


namespace GameCore.Classes
{
    public class RandomBlock : IBlock
    {
        static Random rnd = new Random();
        public int Height { get; set; }
        public int Width { get; set; }
        public IEnumerable<Wall> Walls { get; set; }
        public RandomBlock()
        {
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
            Height = figure.Max((f) => f.Row);
            Width = figure.Max((f) => f.Cell);
            Walls = figure;
        }
        private static void TransposeFigure(List<Wall> figure)
        {
            int temp;
            foreach (var wall in figure)
            {
                temp = wall.Row;
                wall.Row = wall.Cell;
                wall.Cell = temp;
            }
        }
        private static void Normalize(List<Wall> figure)
        {
            var offset = -figure.Min((f) => f.Cell);
            if (offset == 0)
            {
                return;
            }
            foreach (var wall in figure)
            {
                wall.Cell += offset;
            }
        }

    }
}
