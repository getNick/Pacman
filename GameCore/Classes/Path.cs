using GameCore.Interfaces;
using System;
using System.ComponentModel;

namespace GameCore.Classes
{
    public class Path : GameObject,INotifyPropertyChanged
    {
        private IGift Gift { get; set;}
        private bool _haveGift = false;
        public bool HaveGift
        {
            get
            {
                return _haveGift;
            }
            set
            {
                _haveGift = value;
                OnPropertyChanged("HaveGift");
            }
        }
        public Path(int row, int cell) : base(row, cell)
        {
           
        }
        public void SetGift(IGift gift)
        {
            Gift = gift ?? throw new ArgumentNullException("Gift");
            HaveGift = true;
        }
        public bool UseGift()
        {
            if (HaveGift)
            {
                Gift.Activate();
                HaveGift = false;
                return true;
            }
            return false;
        }
    }
}
