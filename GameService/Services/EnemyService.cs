﻿using GameCore.Classes;
using GameCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Services
{
    class EnemyService
    {
        List<Enemy> ListEnemies;
        IPacman Pacman;
        IMaze Maze;
        IPursueAlgo PursueAlgo;
        public EnemyService(int countEmenies,IMaze maze,IPacman pacman,IPursueAlgo pursueAlgo)
        {
            Maze = maze ?? throw new ArgumentNullException("Maze");
            Pacman = pacman ?? throw new ArgumentNullException("Pacman");
            PursueAlgo = pursueAlgo ?? throw new ArgumentNullException("PursueAlgo");
            ListEnemies = new List<Enemy>();
            for(int i = 0; i < countEmenies; i++)
            {
                ListEnemies.Add(new Enemy((int)Maze.EnemyRespoint.X, (int)Maze.EnemyRespoint.Y, Maze, Pacman));
            }
        }
    }
}