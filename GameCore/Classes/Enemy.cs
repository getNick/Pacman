using GameCore.Enums;
using GameCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameCore.Classes
{
    public class Enemy : MoveObject,IEnemy
    {
        public IPacman Pacman { get; set; }
        public Enemy(int row, int cell, IMaze maze,IPacman pacman) : base(row, cell, maze)
        {
        }

    }
}
