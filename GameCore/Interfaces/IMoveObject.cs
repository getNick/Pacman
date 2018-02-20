using GameCore.Enums;

namespace GameCore.Interfaces
{
    public interface IMoveObject : IGameObject
    {
        Direction Direction { get; set; }
        IMaze Maze { get; }
        bool Step();
    }
}
