using System.Windows;

namespace GameCore.Interfaces
{
    public interface IGameObject
    {
        int Row { get; set; }
        int Cell { get; set; }
    }
}
