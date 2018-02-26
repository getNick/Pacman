using GameCore.Enums;
using GameCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameCore.Classes
{
    public class Enemy : MoveObject,IEnemy
    {
        public IPacman Pacman { get; private set; }

        public IPursueAlgo PursueAlgo { get; private set; }

        public Enemy(int row, int cell, IMaze maze,IPacman pacman,IPursueAlgo pursueAlgo) : base(row, cell, maze)
        {
            Pacman = pacman ?? throw new ArgumentNullException("Pacman");
            PursueAlgo = pursueAlgo ?? throw new ArgumentNullException("PursueAlgo");
            Pacman.PacmenStepEvent += new PacmenStep(PacmanSteps);
        }
        private bool PacmanSteps()
        {
            if ((Row == Pacman.Row) & (Cell == Pacman.Cell))
            {
                Pacman.UseAdditionalLife();
                return true;
            }
            return false;
        }
        public override bool Step()
        {
            Direction =PursueAlgo.NextStepDirection(new Vector(Row,Cell),new Vector(Pacman.Row, Pacman.Cell));
            if (base.Step())
            {
                OnPropertyChanged("Row");
                OnPropertyChanged("Cell");
                return true;
            }
            if ((Row == Pacman.Row) & (Cell == Pacman.Cell))
            {
                Pacman.UseAdditionalLife();
            }
            return false;
        }
    }
}
