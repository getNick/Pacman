using GameCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameCore.Classes
{
    public class AStarAlgo : IPursueAlgo
    {
        public IMaze Maze { get;private set; }
        public AStarAlgo(IMaze maze)
        {
            Maze = maze ?? throw new ArgumentNullException("Maze");
        }

        public Vector NextStep(Vector from, Vector to)
        {
            List<Tail> Closed = new List<Tail>();
            List<Tail> Open = new List<Tail>();
            foreach(var path in Maze.Paths)
            {
                var tail = new Tail((int)path.GridPosition.X, (int)path.GridPosition.Y);
                Closed.Add(tail);
            }
            if ((from.X < 0) | (from.X >= Maze.Height)|(from.Y < 0) | (from.Y >= Maze.Width))
            {
                throw new ArgumentOutOfRangeException();
            }
            if ((to.X < 0) | (to.X >= Maze.Height) | (to.Y < 0) | (to.Y >= Maze.Width))
            {
                throw new ArgumentOutOfRangeException();
            }
            Tail start = Closed.First(x => x.Rows == from.X & x.Cell == from.Y);
            start.SetParam(null, 0);
            Tail current;
            
            Tail end = Closed.First(x => x.Rows == to.X & x.Cell == to.Y);
            while (Closed.Count>0)
            {
                current = FindNearest(Open, end);
                if (current == end)
                {
                    break;
                }
                var neighbors=GetNeighbors(current, Closed);
                foreach(var n in neighbors)
                {
                    Closed.Remove(n);
                    Open.Add(n);
                    n.SetParam(current, current.Cost + 1);
                }
            }
            while (end.Parent != start)
            {
                end = end.Parent;
            }
            return new Vector(end.Rows, end.Cell);
        }
        private List<Tail> GetNeighbors(Tail current,List<Tail> list)
        {
            List<Tail> neighbors = new List<Tail>();
            neighbors.Add(list.First((x) => x.Cell == current.Cell & x.Rows == current.Cell + 1));
            neighbors.Add(list.First((x) => x.Cell == current.Cell & x.Rows == current.Cell - 1));
            neighbors.Add(list.First((x) => x.Cell == current.Cell + 1 & x.Rows == current.Cell));
            neighbors.Add(list.First((x) => x.Cell == current.Cell - 1 & x.Rows == current.Cell));
            return neighbors;
        }
        private Tail FindNearest(List<Tail> list,Tail end)
        {
            int min = Distance(list[0], end);
            int index = 0;
            int temp;
            for (int i = 1; i < list.Count; i++)
            {
                temp = Distance(list[i], end);
                if (temp < min)
                {
                    min = temp;
                    index = i;
                }
            }
            return list[index];
        }
        private int Distance(Tail current,Tail to)
        {
            return Math.Abs(current.Rows - to.Rows) + Math.Abs(current.Cell - to.Cell);
        }
       
        private class Tail
        {
            public Tail Parent;
            public int Cost;
            public int Rows { get; }
            public int Cell { get; }
            public Tail(int i,int j)
            {
                Rows = i;
                Cell = j;
            }
            public void SetParam(Tail tail,int cost)
            {
                //Parent = tail ?? throw new ArgumentNullException("Tail");
                this.Cost = (cost > 0)?cost: throw new ArgumentException("cost<0");
                
            }
        }
    }
}
