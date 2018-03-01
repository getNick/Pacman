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
        /// <summary>
        /// Create and start StepEvent
        /// </summary>
        public TimeService()
        {
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerAsync();
        }
        /// <summary>
        /// Stop working, created new layer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void StopWorking(object sender, EventArgs e)
        {
            stopWorking = true;
        }


        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!stopWorking)
            {
                Thread.Sleep(GameCore.EnumsAndConstant.GameConstants.PauseBetweenSteps);
                StepEvent?.Invoke();
            }
        }

    }
}
