using GameCore.Classes;
using System.Collections.Generic;

namespace GameCore.Interfaces
{
    public interface IBlock
    {
        int Height { get; set; }
        int Width { get; set; }
        IEnumerable<Wall> Walls { get; set; }
    }
}
