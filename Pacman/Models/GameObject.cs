using System.Windows;

namespace Pacman.Models
{
    abstract class GameObject:IGameObject
    {
        public Vector GridPosition { get;set; }
        public GameObject(int Row,int Cell)
        {
            GridPosition = new Vector(Row, Cell);
        }
    }
}
