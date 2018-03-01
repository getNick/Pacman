using GameCore.Classes;
using GameCore.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameService.Services
{
    public class PacmanService : MoveObject, IPacman
    {
        private int _lifes;
        public int Lifes {
            get
            {
                return _lifes;
            }
            set
            {
                _lifes = value;
                OnPropertyChanged("Lifes");
            }
        }
        private bool _invulnerable = false;
        private bool _eating = false;
        public bool Eating {
            get
            {
                return _eating;
            }
            private set
            {
                _eating = value;
                OnPropertyChanged("Eating");
            }
        }

        public event PacmenStep PacmenStepEvent;
        public event EventHandler PacmenDead;
        public event EventHandler PacmenCatch;

        BackgroundWorker EatingWorker;
        BackgroundWorker InvulnerableWorker;

        public PacmanService(IMaze maze,int countLifes): base((int)maze.PacmenPespoint.X, (int)maze.PacmenPespoint.Y,maze)
        {
            Lifes = countLifes;
            EatingWorker = new BackgroundWorker();
            InvulnerableWorker = new BackgroundWorker();
            EatingWorker.DoWork += Eate;
            InvulnerableWorker.DoWork += InvulnerableWorkerMethod;
            
        }

        public override bool Step()
        {
            if (base.Step())
            {
                PacmenStepEvent?.Invoke();
                OnPropertyChanged("Row");
                OnPropertyChanged("Cell");
                if (_invulnerable == false)
                {
                    UseGift();
                }
                return true;
            }
            return false;
        }
        private void UseGift()
        {
            var Path = Maze.Paths.First((x) => x.Row == this.Row & x.Cell == this.Cell);
            if (Path.UseGift())
            {
                if (!EatingWorker.IsBusy)
                {
                    EatingWorker.RunWorkerAsync();
                }
            }
        }
        public void UseAdditionalLife()
        {
            if (Lifes == 0)
            {
                PacmenDead?.Invoke(this, new EventArgs());
            }
            else
            {
                Lifes--;
                PacmenCatch?.Invoke(this, new EventArgs());
                if (!InvulnerableWorker.IsBusy)
                {
                    InvulnerableWorker.RunWorkerAsync();
                }
            }
        }

        public void Eate(object sender, DoWorkEventArgs e)
        {
            Eating = true;
            Thread.Sleep(GameCore.EnumsAndConstant.GameConstants.EatingTime);
            Eating = false;
        }
        public void InvulnerableWorkerMethod(object sender, DoWorkEventArgs e)
        {
            _invulnerable = true;
            Thread.Sleep(GameCore.EnumsAndConstant.GameConstants.PacmanCatchPause);
            _invulnerable = false;
        }

    }
}
