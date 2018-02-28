using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication.Models
{
    class Record
    {
        public int Place { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public Record(int place, string name, int score)
        {
            Place = place;
            Name = name;
            Score = score;
        }
    }
}
