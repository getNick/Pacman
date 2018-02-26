using GameCore.Enums;
using GameCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CustomGamePlugin.PursueAlgo
{
    class SimplePursueAlgo : IPursueAlgo
    {
        public IMaze Maze { get; private set; }
        private Random Random;
        public SimplePursueAlgo(IMaze maze)
        {
            Maze = maze ?? throw new ArgumentNullException("Maze");
            Random = new Random();
        }
        public Direction NextStepDirection(Vector from, Vector to)
        {
            return (Direction)Random.Next(5);
        }
    }
}
