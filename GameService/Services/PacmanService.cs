using GameCore.Classes;
using GameCore.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Services
{
    public class PacmanService : MoveObject, IPacman
    {
        public int Lifes { get; set; }

        public event PacmenStep PacmenStepEvent;

        public PacmanService(IMaze maze): base((int)maze.PacmenPespoint.X, (int)maze.PacmenPespoint.Y,maze)
        {
            Lifes = 3;
        }

        

        public override bool Step()
        {
            if (base.Step())
            {
                PacmenStepEvent?.Invoke();
                OnPropertyChanged("Row");
                OnPropertyChanged("Cell");
                UseGift();
                return true;
            }
            return false;
        }
        private void UseGift()
        {
            var Path = Maze.Paths.First((x) => x.Row == this.Row & x.Cell == this.Cell);
            Path.UseGift();
        }
        public void UseAdditionalLife()
        {
            if (Lifes == 0)
            {
                //game over
            }
            else
            {
                Lifes--;
                OnPropertyChanged("Lifes");
            }
        }

    }
}
