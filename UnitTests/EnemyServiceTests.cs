using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using GameCore.Classes;
using GameCore.EnumsAndConstant;
using GameCore.Interfaces;
using GameService.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests
{
    [TestClass]
    public class EnemyServiceTests
    {
        TimeService TimeService;
        Mock<IMaze> Maze;
        Mock<IPacman> Pacman;
        Mock<IPursueAlgo> PursueAlgo;
        EnemyService EnemyService;
        int Count;
        [TestInitialize]
        public void Init()
        {
            TimeService = new TimeService();
            Maze = new Mock<IMaze>();
            Pacman = new Mock<IPacman>();
            PursueAlgo = new Mock<IPursueAlgo>();
            Count = 3;
        }
        [TestMethod]
        public void EnemyServiceCtr_CountEnemiris0_Exeption()
        {
            try
            {
                EnemyService = new EnemyService(0, Maze.Object, Pacman.Object, TimeService);
            }catch(ArgumentException ex)
            {
                Assert.AreEqual("Count Enemies can't be less 1", ex.Message);
            }
            catch(Exception e)
            {
                Assert.Fail(
             string.Format("Unexpected exception of type {0} caught: {1}",
                            e.GetType(), e.Message));
            }
        }
        [TestMethod]
        public void EnemyServiceCtr_CountEnemirisMinus10_ArgumentException()
        {
            try
            {
                EnemyService = new EnemyService(-10, Maze.Object, Pacman.Object, TimeService);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Count Enemies can't be less 1", ex.Message);
            }
            catch (Exception e)
            {
                Assert.Fail(
             string.Format("Unexpected exception of type {0} caught: {1}",
                            e.GetType(), e.Message));
            }
        }
        [TestMethod]
        public void EnemyService_MazeNull_ArgumentNullException() {
            IMaze maze = null;

            try
            {
                EnemyService = new EnemyService(Count, maze, Pacman.Object, TimeService);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("Maze", ex.ParamName);
            }
        }

        [TestMethod]
        public void EnemyService_PacmanNull_ArgumentNullException()
        {
            IPacman pacman = null;

            try
            {

                EnemyService = new EnemyService(Count, Maze.Object, pacman, TimeService);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("Pacman", ex.ParamName);
            }
           
        }
        [TestMethod]
        public void EnemyService_TimeServiceNull_ArgumentNullException()
        {
            TimeService timeService = null;

            try
            {

                EnemyService = new EnemyService(Count, Maze.Object, Pacman.Object, timeService);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("TimeService", ex.ParamName);
            }
        }

    }
}
