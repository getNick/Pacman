using System;
using System.Collections.Generic;
using GameCore.Classes;
using GameCore.EnumsAndConstant;
using GameCore.Interfaces;
using GameService.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests
{
    [TestClass]
    public class PacmanServiceTests
    {
        Mock<IMaze> MazeMock;
        [TestInitialize]
        public void InitTest()
        {
            MazeMock = new Mock<IMaze>();
        }
        [TestMethod]
        public void UseAdditionalLifeTest_LifesMinus()
        {
            GameCore.EnumsAndConstant.GameConstants.PacmanRespointRow = 1;
            GameCore.EnumsAndConstant.GameConstants.PacmanRespointCell = 1;
            var pacmanServ = new PacmanService(MazeMock.Object, 3, 300);

            pacmanServ.UseAdditionalLife();

            Assert.AreEqual(pacmanServ.Lifes, 2);
        }
        [TestMethod]
        public void UseAdditionalLife_Lifes0_PacmanDeadTrue()
        {
            GameCore.EnumsAndConstant.GameConstants.PacmanRespointRow = 1;
            GameCore.EnumsAndConstant.GameConstants.PacmanRespointCell = 1;
            var pacmanServ = new PacmanService(MazeMock.Object,0 , 300);

            pacmanServ.UseAdditionalLife();

            Assert.AreEqual(pacmanServ.Lifes, 0);
            Assert.AreEqual(pacmanServ.IsDead, true);
        }
    }
}
