using GameCore.Interfaces;

namespace GameCore.Classes
{
    public class Player : IPlayer
    {
        public Player(string name)
        {
            Name = name;
            Score = 0;
        }
        public string Name { get; set; }
        public int Score { get; set; }
    }
}
