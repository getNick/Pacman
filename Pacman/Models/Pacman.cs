using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.Models
{
    class Pacman:MoveObject,IPacman, INotifyPropertyChanged
    {
        public int Lifes { get; }
        private int _lifes;
        public Pacman(int Row,int Cell,IMaze maze) : base(Row, Cell,maze)
        {
            _lifes = 3;
        }
        public override bool Step()
        {
            if (base.Step())
            {
                UseGift();
                return true;
            }
            return false;
        }
        private void UseGift()
        {
            var Path=Maze.Gifts.First((x) => x.GridPosition.X == this.GridPosition.X & x.GridPosition.Y == this.GridPosition.Y);
            Path.UseGift();
        }
        public void UseAdditionalLife()
        {
            if (Lifes == 0)
            {
                //game over
            }
            else
            {
                _lifes--;
                OnPropertyChanged("Lifes");
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
