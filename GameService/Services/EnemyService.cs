using Autofac;
using GameCore.Classes;
using GameCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameService.Services
{
    public class EnemyService
    {
        public List<IEnemy> ListEnemies;
        Random random = new Random();
        public EnemyService(int countEmenies,IMaze maze,IPacman pacman)
        {
            ListEnemies = new List<IEnemy>();
            var timeService = LayerService.Container.Resolve<TimeService>();
            
            for(int i = 0; i < countEmenies; i++)
            {
                var pos = GetRandomEnemyPos(pacman,maze);
                var enemy = LayerService.Container.Resolve<IEnemy>(
                        new NamedParameter("row", pos.X ),
                        new NamedParameter("cell", pos.Y)
                    );
                timeService.StepEvent += new TimeService.TimeToStep(enemy.Step);

                ListEnemies.Add(enemy);
            }
        }
        public Vector GetRandomEnemyPos(IPacman pacman,IMaze maze)
        {
            int index;
            bool finded = false;
            Vector pos = new Vector();
            while (!finded)
            {
                index = random.Next(0, maze.Paths.Count());//random path index
                var ramdomPos = maze.Paths.ElementAt(index);
                if ((Math.Abs(pacman.Row - ramdomPos.Row) + Math.Abs(pacman.Cell - ramdomPos.Cell)) > maze.Height / 2)//but not near the pacman
                {
                    finded = true;
                    pos.X = ramdomPos.Row;
                    pos.Y = ramdomPos.Cell;
                }
            }
            return pos;

        }
    }
}
