﻿using GameCore.Interfaces;
using System;
using System.ComponentModel;

namespace GameCore.Classes
{
    public class Path : GameObject,INotifyPropertyChanged
    {
        private IGift Gift { get; set;}
        public bool HaveGift { get; private set; } = false;
        public Path(int row, int cell) : base(row, cell)
        {
           
        }
        public void SetGift(IGift gift)
        {
            Gift = gift ?? throw new ArgumentNullException("Gift");
            HaveGift = true;
        }
        public void UseGift()
        {
            if (HaveGift)
            {
                Gift.Activate();
                HaveGift = false;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}