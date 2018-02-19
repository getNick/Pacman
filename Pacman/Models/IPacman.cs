using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.Models
{
    interface IPacman:IMoveObject
    {
        int Lifes { get;}
        void UseAdditionalLife();
        
    }
}
