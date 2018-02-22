using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameCore.Interfaces
{
    public interface IPursueAlgo
    {
        IMaze Maze { get;}
        Vector NextStep(Vector from, Vector to);
    }
}
