﻿using GameCore.Enums;
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
        private Queue<Tail> QueSteps;
        private Random rnd = new Random();
        public AStarAlgo(IMaze maze)
        {
            Maze = maze ?? throw new ArgumentNullException("Maze");
            QueSteps = new Queue<Tail>();
            Console.WriteLine("ASTAR");
        }

        public Direction NextStepDirection(Vector from, Vector to)
        {
            if (QueSteps.Count == 0)
            {
                GetNextSteps(50, from, to, QueSteps);
            }
            if (QueSteps.Count == 0)
            {
                return Direction.Right;
            }
            var temp = QueSteps.Dequeue();
            if (temp.Rows > from.X)
            {
                return Direction.Up;
            }
            else if (temp.Rows < from.X)
            {
                return Direction.Down;
            }
            else if (temp.Cell > from.Y)
            {
                return Direction.Right;
            }
            else 
            {
                return Direction.Left;
            }
        }
        private void GetNextSteps(int count, Vector from, Vector to,Queue<Tail> queue)
        {
            Console.WriteLine("New roaaaaaaaaaaaaaaaaaaaaaaaaaaadddddd");
            List<Tail> Closed = new List<Tail>();
            List<Tail> Open = new List<Tail>();
            List<Tail> All = new List<Tail>();
            foreach (var path in Maze.Paths)
            {
                var tail = new Tail(path.Row ,path.Cell);
                All.Add(tail);
            }
            if ((from.X < 0) | (from.X >= Maze.Height) | (from.Y < 0) | (from.Y >= Maze.Width))
            {
                throw new ArgumentOutOfRangeException();
            }
            if ((to.X < 0) | (to.X >= Maze.Height) | (to.Y < 0) | (to.Y >= Maze.Width))
            {
                throw new ArgumentOutOfRangeException();
            }
            Tail end = All.First(x => x.Rows == to.X & x.Cell == to.Y);
            Tail start = All.First(x => x.Rows == from.X & x.Cell == from.Y);
            start.SetParam(null, 0,end);
            Open.Add(start);
            Tail current;
            
            while (Open.Count > 0)
            {
                current = Open.First(x=>x.CostRoadToEnd+x.Cost==Open.Min((e)=>e.CostRoadToEnd+e.Cost));
                if (current == end)
                {
                    break;
                }
                Open.Remove(current);
                Closed.Add(current);
                var neighbors = GetNeighbors(current, All);
               
                foreach (var n in neighbors)
                {
                    if (Closed.Contains(n))
                    {
                        continue;
                    }
                    if (!Open.Contains(n))
                    {
                        Open.Add(n);
                        n.SetParam(current, current.Cost + 1, end);
                    }
                    else if ((n.Cost) > (current.Cost + 1)){
                        n.SetParam(current, current.Cost + 1, end);
                    }
                }

            }
            List<Tail> tempList = new List<Tail>();
            if (end.Parent == null)
            {
                return;
            }
            while (end.Parent != null)
            {
                tempList.Add(end);
                end = end.Parent;
            }
            tempList.Reverse();
            for(int i = 0; (i < tempList.Count)&(i<count); i++)
            {
                queue.Enqueue(tempList[i]);
            }
        }
        private List<Tail> GetNeighbors(Tail current,List<Tail> list)
        {
            List<Tail> neighbors = new List<Tail>();
            Tail temp = list.FirstOrDefault((x) => x.Cell == current.Cell & x.Rows == current.Rows + 1);
            if (temp != null)
            {
                neighbors.Add(temp);
            }
            temp = list.FirstOrDefault((x) => x.Cell == current.Cell & x.Rows == current.Rows - 1);
            if (temp != null)
            {
                neighbors.Add(temp);
            }
            temp = list.FirstOrDefault((x) => x.Cell == current.Cell + 1 & x.Rows == current.Rows);
            if (temp != null)
            {
                neighbors.Add(temp);
            }
            temp = list.FirstOrDefault((x) => x.Cell == current.Cell - 1 & x.Rows == current.Rows);
            if (temp != null)
            {
                neighbors.Add(temp);
            }
            return neighbors;
        }
        /*private Tail FindNearest(List<Tail> list,Tail end)
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
        }*/
        
       
        private class Tail
        {
            public Tail Parent;
            public int Cost;
            private Tail End;
            private int _costRoatToEnd;
            public int CostRoadToEnd {
                get
                {
                    if (_costRoatToEnd == 0)
                    {
                        _costRoatToEnd = Distance();
                    }
                    return _costRoatToEnd;
                }
            }
            private int Distance()
            {
                return Math.Abs(Rows - End.Rows) + Math.Abs(Cell - End.Cell);
            }
            public int Rows { get; }
            public int Cell { get; }
            public Tail(int i,int j)
            {
                Rows = i;
                Cell = j;
            }
            public void SetParam(Tail tail,int cost,Tail end)
            {
                Parent = tail;
                Cost = cost;
                End = end;
            }
        }
    }
}
