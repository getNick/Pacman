using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pacman.Models
{
    interface IMaze
    {
        IEnumerable<Vector> Walls { get;}
        ObservableCollection<Path> Gifts { get;}

        bool StepTo(int i, int j);
    }
}
