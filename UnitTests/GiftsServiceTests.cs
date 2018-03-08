using System;
using System.Collections.Generic;
using GameCore.Classes;
using GameCore.Interfaces;
using GameService.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GameService.Tests
{
    [TestClass]
    public class GiftsServiceTests
    {
        GiftsService Service;
        Mock<IMaze> Maze;
        Mock<IPlayer> Player;

     [TestInitialize]
        public void Init()
        {
            Player = new Mock<IPlayer>();
            Maze = new Mock<IMaze>();
        }

        [TestMethod]
        public void GiftsService_MazeNull_ArgumentNullException()
        {
            IMaze maze = null;

            try
            {
                Service = new GiftsService(maze, Player.Object);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("Maze", ex.ParamName);
            }
        }
        [TestMethod]
        public void EnemyService_TimeServiceNull_ArgumentNullException()
        {
            IPlayer player = null;
            try
            {
                Service = new GiftsService(Maze.Object, player);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("Player", ex.ParamName);
            }
        }

        [TestMethod]
        public void SetGifts_OneNonAvailablePath33()
        {
            Service = new GiftsService(Maze.Object, Player.Object);
            List<Path> listPath = new List<Path>
            {
                new Path(1, 1),
                new Path(2, 1),
                new Path(3, 1),
                new Path(1, 2),
                new Path(1, 3),
                new Path(3, 3)///
            };
            Maze.SetupGet(x => x.Paths).Returns(listPath);
            GameCore.EnumsAndConstant.GameConstants.PacmanRespointRow = 1;
            GameCore.EnumsAndConstant.GameConstants.PacmanRespointCell = 1;
            Service.SetGifts();

            int countAvailablePath = 0;
            foreach(var p in Maze.Object.Paths)
            {
                if ((p.Row == 3) & (p.Cell == 3))
                {
                    Assert.IsTrue(p.HaveGift == false);
                }
                else
                {
                    countAvailablePath++;
                    Assert.IsTrue(p.HaveGift);
                }
            }
            Assert.IsTrue(Service.GiftsCount == countAvailablePath);
        }
    }
}
