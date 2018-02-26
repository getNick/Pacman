using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCore.Interfaces
{
    public interface IPlayer
    {
        string Name { get;}
        int Score { get;}
        void AddToScore(int count);
        bool ChangeName(string newName);
    }
}
