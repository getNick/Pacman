using GameCore.Interfaces;
using System;
using System.ComponentModel;
using System.Threading;

namespace GameService.Services
{
    class TimeService
    {
        private readonly BackgroundWorker worker;
        public delegate bool TimeToStep();
        public event TimeToStep StepEvent;
        bool stopWorking = false;
        public TimeService(IPacman pacman)
        {
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        public void StopWorking(object sender, EventArgs e)
        {
            stopWorking = true;
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine("Worker stop");
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!stopWorking)
            {
                Thread.Sleep(400);
                StepEvent?.Invoke();
            }
        }

    }
}
