using Autofac;
using GameCore.Classes;
using GameCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GameCore.EnumsAndConstant;

namespace GameService.Services
{
    public class EnemyService
    {
        public List<IEnemy> ListEnemies;
        Random random = new Random();
        public EnemyService(int countEnemies,IMaze maze,IPacman pacman,TimeService timeService)
        {
            if (countEnemies < 1) {
                throw new ArgumentException("Count Enemies can't be less 1");
            }
            maze = maze ?? throw new ArgumentNullException("Maze");
            pacman = pacman ?? throw new ArgumentNullException("Pacman");
            timeService = timeService ?? throw new ArgumentNullException("TimeService");

            ListEnemies = new List<IEnemy>();
            
            for(int i = 0; i < countEnemies; i++)
            {
                var pos = GetRandomEnemyPos(pacman,maze,(maze.Height+maze.Width)/2);
                var enemy = LayerService.Container.Resolve<IEnemy>(
                        new NamedParameter(GameConstants.NamedParameterRow, pos.X ),
                        new NamedParameter(GameConstants.NamedParameterCell, pos.Y),
                        new NamedParameter(GameConstants.NamedParameterPacmanCatchPause,GameConstants.PacmanCatchPause)
                    );
                timeService.StepEvent += new TimeService.TimeToStep(enemy.Step);
                ListEnemies.Add(enemy);
            }
        }
        private Vector GetRandomEnemyPos(IPacman pacman,IMaze maze,double dontNear)
        {
            int index;
            bool finded = false;
            Vector pos = new Vector();
            while (!finded)
            {
                index = random.Next(0, maze.Paths.Count());//random path index
                var ramdomPos = maze.Paths.ElementAt(index);
                if ((Math.Abs(pacman.Row - ramdomPos.Row) + Math.Abs(pacman.Cell - ramdomPos.Cell)) > dontNear)//but not near the pacman
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
