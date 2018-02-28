using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.Interfaces
{
    public delegate bool PacmenStep();
    public interface IPacman : IMoveObject, INotifyPropertyChanged
    {
        int Lifes { get; }
        void UseAdditionalLife();
        bool Eating { get;}
        event EventHandler PacmenDead;
        event PacmenStep PacmenStepEvent;
        event EventHandler PacmenCatch;

    }
}
