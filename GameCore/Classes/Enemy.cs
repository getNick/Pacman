using GameCore.Interfaces;
using NLog;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;

namespace GameCore.Classes
{
    public class Enemy : MoveObject,IEnemy
    {
        public IPacman Pacman { get; private set; }
        public IPursueAlgo PursueAlgo { get; private set; }
        private bool EnemisOnPause = false;
        private BackgroundWorker worker;
        private int _pacmanCatchPause;

        public Enemy(int row, int cell,int pacmanCatchPause, IMaze maze,IPacman pacman,IPursueAlgo pursueAlgo) : base(row, cell, maze)
        {
            Pacman = pacman ?? throw new ArgumentNullException("Pacman");
            PursueAlgo = pursueAlgo ?? throw new ArgumentNullException("PursueAlgo");
            _pacmanCatchPause = pacmanCatchPause;
            Pacman.PacmenStepEvent += new PacmenStep(PacmanSteps);
            Pacman.PacmenCatch += new EventHandler(PacmanCatch);
            worker = new BackgroundWorker();
            worker.DoWork += EnemiesStoped;
        }

        private void PacmanCatch(object sender, EventArgs e)
        {
            if (!worker.IsBusy)
            {
                worker.RunWorkerAsync();
            }
        }

        private void EnemiesStoped(object sender, DoWorkEventArgs e)
        {
            EnemisOnPause = true;
            Thread.Sleep(_pacmanCatchPause);
            EnemisOnPause = false;
        }

        private bool PacmanSteps()
        {
            if (!EnemisOnPause)
            {
                if ((Row == Pacman.Row) & (Cell == Pacman.Cell))
                {
                    Pacman.UseAdditionalLife();
                    return true;
                }
            }
            return false;
        }
        public override bool Step()
        {

            if (EnemisOnPause)
            {
                return false;
            }
            if ((Row == Pacman.Row) & (Cell == Pacman.Cell))
            {
                Pacman.UseAdditionalLife();
            }
            Direction = PursueAlgo.NextStepDirection(new Vector(Row, Cell), new Vector(Pacman.Row, Pacman.Cell));
            if (base.Step())
            {
                return true;
            }
            return false;
        }
    }
}
