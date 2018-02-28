using System.ComponentModel;

namespace GameCore.Interfaces
{
    public interface IPlayer: INotifyPropertyChanged
    {
        string Name { get;}
        int Score { get;}
        void AddToScore(int count);
        bool ChangeName(string newName);
        void ResetScore();
    }
}
