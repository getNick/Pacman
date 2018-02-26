using GameCore.Interfaces;
using System.ComponentModel;
using System.Windows;

namespace GameCore.Classes
{
    public abstract class GameObject : IGameObject,INotifyPropertyChanged
    {
        private int _row;
        public int Row {
            get
            {
                return _row;
            }
            set
            {
                _row = value;
                OnPropertyChanged("Row");
            }
        }
        private int _cell;
        public int Cell
        {
            get
            {
                return _cell;
            }
            set
            {
                _cell = value;
                OnPropertyChanged("Cell");
            }
        }
        public GameObject(int row, int cell)
        {
            Row = row;
            Cell = cell;
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
