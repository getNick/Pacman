using GameCore.EnumsAndConstant;
using System.Windows;

namespace GameCore.Interfaces
{
    public interface IPursueAlgo
    {
        IMaze Maze { get;}
        Direction NextStepDirection(Vector from, Vector to);
    }
}
