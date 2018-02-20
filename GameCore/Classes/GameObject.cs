using GameCore.Interfaces;
using System.Windows;

namespace GameCore.Classes
{
    public abstract class GameObject : IGameObject
    {
        public Vector GridPosition { get; set; }
        public GameObject(int row, int cell)
        {
            GridPosition = new Vector(row, cell);
        }
    }
}
