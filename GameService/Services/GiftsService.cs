using GameCore.Classes;
using GameCore.Interfaces;
using System;


namespace GameService.Services
{
    public class GiftsService
    {
        private IMaze Maze;
        private IPlayer Player;
        public GiftsService(IMaze maze,IPlayer player)
        {
            Maze = maze ?? throw new ArgumentNullException("Maze");
            Player = player ?? throw new ArgumentNullException("Player");
        }
        public void SetGifts()
        {
            foreach(var p in Maze.Paths)
            {
                p.SetGift(new DefaultGift(Player));
            }
        }
    }
}
