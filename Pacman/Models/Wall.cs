using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.Models
{
    class Wall:GameObject
    {
        public Wall(int row,int column)
        {
            GrigPosition.X = row;
            GrigPosition.Z = column;
        }
    }
}
