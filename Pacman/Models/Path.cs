using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.Models
{
    class Path:GameObject
    {
        private Gift _gift;
        private bool haveGift = true;
        public Path(int Row,int Cell) : base(Row, Cell)
        {
            _gift = new Gift();
        }
        public void UseGift()
        {
            if (haveGift)
            {
                _gift.Activate();
                haveGift = false;
            }
        }
    }
}
