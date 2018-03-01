using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GameCore.Interfaces
{
    public interface IPlayer: INotifyPropertyChanged
    {
        [Key]
        int Id { get;}
        string Name { get;}
        int Score { get;}
        void AddToScore(int count);
        bool ChangeName(string newName);
        void ResetScore();
    }
}
