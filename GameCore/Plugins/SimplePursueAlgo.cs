using GameCore.EnumsAndConstant;
using GameCore.Interfaces;
using System;
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

        Direction IPursueAlgo.NextStepDirection(Vector from, Vector to)
        {
            throw new NotImplementedException();
        }
    }
}
