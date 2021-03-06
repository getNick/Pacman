﻿using GameCore.Classes;
using GameCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using GameCore.EnumsAndConstant;

namespace GameService.Services
{
    public class GiftsService
    {
        private IMaze Maze;
        private IPlayer Player;
        public int GiftsCount { get;private set; }
        public GiftsService(IMaze maze,IPlayer player)
        {
            Maze = maze ?? throw new ArgumentNullException("Maze");
            Player = player ?? throw new ArgumentNullException("Player");
        }
        /// <summary>
        /// Set gifts to all available place
        /// </summary>
        public void SetGifts()
        {
            var availablePaths = FindAllAvailablePath();
            GiftsCount = availablePaths.Count;
            foreach (var p in availablePaths)
            {
                p.SetGift(new DefaultGift(Player));
            }
        }
        private List<Path> FindAllAvailablePath()
        {
            List<Path> Closed = new List<Path>();
            List<Path> Open = new List<Path>();

            Path current = Maze.Paths.First(x=>x.Row== GameConstants.PacmanRespointRow 
                &x.Cell== GameConstants.PacmanRespointCell);
            Open.Add(current);
            while (Open.Count > 0)
            {
                current = Open.First();
                Open.Remove(current);
                Closed.Add(current);
                var neighbors = GetNeighbors(current, Maze.Paths);
                foreach (var n in neighbors)
                {
                    if (Closed.Contains(n))
                    {
                        continue;
                    }
                    if (!Open.Contains(n))
                    {
                        Open.Add(n);
                    }
                }
            }
            return Closed;
        }
        private List<Path> GetNeighbors(Path current, IEnumerable<Path> list)
        {
            List<Path> neighbors = new List<Path>();
            Path temp = list.FirstOrDefault((x) => x.Cell == current.Cell & x.Row == current.Row + 1);
            if (temp != null)
            {
                neighbors.Add(temp);
            }
            temp = list.FirstOrDefault((x) => x.Cell == current.Cell & x.Row == current.Row - 1);
            if (temp != null)
            {
                neighbors.Add(temp);
            }
            temp = list.FirstOrDefault((x) => x.Cell == current.Cell + 1 & x.Row == current.Row);
            if (temp != null)
            {
                neighbors.Add(temp);
            }
            temp = list.FirstOrDefault((x) => x.Cell == current.Cell - 1 & x.Row == current.Row);
            if (temp != null)
            {
                neighbors.Add(temp);
            }
            return neighbors;
        }
    }
}
