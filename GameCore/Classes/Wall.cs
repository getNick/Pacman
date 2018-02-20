using GameCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows;

namespace GameCore.Classes
{
    public class Wall : GameObject
    {
        public IEnumerable<Vector> ListBricks { get; set; }

        public Wall(int row, int cell) : base(row, cell)
        {
           
        }
    }
}
