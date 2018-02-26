using GameCore.Enums;
using System.ComponentModel;

namespace GameCore.Interfaces
{
    public interface IMoveObject : IGameObject, INotifyPropertyChanged
    {
        Direction Direction { get; set; }
        IMaze Maze { get; }
        bool Step();
    }
}
