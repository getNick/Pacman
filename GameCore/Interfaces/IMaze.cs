using GameCore.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace GameCore.Interfaces
{
    public interface IMaze
    {
        int Height { get;}
        int Width { get;}
        IEnumerable<Wall> Walls { get;}
        IEnumerable<Path> Paths { get;}
        bool StepTo(int i, int j);
    }
}
