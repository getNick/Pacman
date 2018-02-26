using GameCore.Interfaces;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GameCore.Classes
{
    public class Player : IPlayer,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        private int _score = 0;
        public int Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
                OnPropertyChanged("Score");
            }
        }
        public Player() { }
        public Player(string name)
        {
            Name = name;
            Score = 0;
        }
        

        public void AddToScore(int count)
        {
            if (count > 0)
            {
                Score += count;
            }

        }
        public bool ChangeName(string newName)
        {
            if (newName.Length > 0)
            {
                Name = newName;
                return true;
            }
            return false;
        }
        
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
