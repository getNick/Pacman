using GameCore.Classes;
using GameCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Services
{
    public class EnemyService
    {
        public List<Enemy> ListEnemies;
        IPacman Pacman;
        IMaze Maze;
        IPursueAlgo PursueAlgo;
        public EnemyService(int countEmenies,IMaze maze,IPacman pacman,IPursueAlgo pursueAlgo)
        {
            Maze = maze ?? throw new ArgumentNullException("Maze");
            Pacman = pacman ?? throw new ArgumentNullException("Pacman");
            PursueAlgo = pursueAlgo ?? throw new ArgumentNullException("PursueAlgo");
            ListEnemies = new List<Enemy>();
            Random random = new Random();
            for(int i = 0; i < countEmenies; i++)
            {
                int index = random.Next(0, Maze.Paths.Count);
                ListEnemies.Add(new Enemy(Maze.Paths[index].Row, Maze.Paths[index].Cell, Maze, Pacman,PursueAlgo));
            }
        }
    }
}
