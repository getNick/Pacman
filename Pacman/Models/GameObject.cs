using System.Windows;

namespace Pacman.Models
{
    abstract class GameObject
    {
        public Vector GridPosition;
        public GameObject(int Row,int Cell)
        {
            GridPosition.X = Row;
            GridPosition.Y = Cell;
        }
    }
}
