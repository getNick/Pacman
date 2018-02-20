using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.Interfaces
{
    public interface IPacman : IMoveObject
    {
        int Lifes { get; }
        void UseAdditionalLife();

    }
}
