﻿using System;
using System.ComponentModel;
using System.Threading;

namespace GameService.Services
{
    class TimeService
    {
        private readonly BackgroundWorker worker;
        public delegate bool TimeToStep();
        public event TimeToStep StepEvent;
        public TimeService()
        {
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync(100);
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                Thread.Sleep(300);
                Console.WriteLine("Tic");
                StepEvent?.Invoke();
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine("Worker stop");
        }
    }
}
