﻿using GameCore.Classes;
using GameCore.Interfaces;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using GameCore.EnumsAndConstant;
using NLog;

namespace GameService.Services
{
    public class PacmanService : MoveObject, IPacman
    {
        private static Logger logger = LogManager.GetLogger("fileLogger");
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
        private int _timeInvulnerable;
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

        public bool IsDead { get; private set; }

        public event PacmenStep PacmenStepEvent;
        public event EventHandler PacmenDead;
        public event EventHandler PacmenCatch;

        BackgroundWorker EatingWorker;
        BackgroundWorker InvulnerableWorker;

        public PacmanService(IMaze maze,int countLifes,int timeInvulnerable): base(GameConstants.PacmanRespointRow, GameConstants.PacmanRespointCell,maze)
        {
            Lifes = countLifes;
            _timeInvulnerable = timeInvulnerable;
            EatingWorker = new BackgroundWorker();
            InvulnerableWorker = new BackgroundWorker();
            EatingWorker.DoWork += Eate;
            InvulnerableWorker.DoWork += InvulnerableWorkerMethod;
            
        }

        public override bool Step()
        {
            if (base.Step())
            {
                logger.Info($"Pacman steps to {Row},{Cell}");
                PacmenStepEvent?.Invoke();
                //dont eat gifts if invulnerable
                if (_invulnerable == false)
                {
                    UseGift();
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// Use Gifr if exist
        /// </summary>
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
        /// <summary>
        /// Decrement Lifes value,invoke PacmanDead and Pacman  Catch event
        /// </summary>
        public void UseAdditionalLife()
        {
            if (Lifes == 0)
            {
                PacmenDead?.Invoke(this, new EventArgs());
                IsDead = true;
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
        /// <summary>
        /// event method what manages eating animation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Eate(object sender, DoWorkEventArgs e)
        {
            Eating = true;
            Thread.Sleep(GameConstants.EatingTime);
            Eating = false;
        }/// <summary>
        /// event method what manages Invuilnerable pacmans state
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void InvulnerableWorkerMethod(object sender, DoWorkEventArgs e)
        {
            _invulnerable = true;
            Thread.Sleep(_timeInvulnerable);
            _invulnerable = false;
        }

    }
}
