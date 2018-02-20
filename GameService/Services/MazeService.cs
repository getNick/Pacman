using GameCore.Classes;
using GameCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Services
{
    class MazeService
    {
        public IMaze Maze;
        public MazeService(int height, int width)
        {
            Maze = new Maze(height, width);
        }
    }
}
