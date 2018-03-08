using System;
using GameCore.Interfaces;
using GameService.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class MazeServiceTests
    {
        static IMaze  Maze;
        static IMaze NotSymmetricMaze;
        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            Maze = new MazeService(20, 20);
            NotSymmetricMaze = new MazeService(20, 19);
        }
        
        [TestInitialize]
        public void Init()
        {

        }

        [TestMethod]
        public void SymmetricMazeFull()
        {
            int expectedCountTails = Maze.Height * Maze.Width;

            int actual = 0;
            foreach(var e in Maze.Walls)
            {
                actual++;
            }
            foreach(var e in Maze.Paths)
            {
                actual++;
            }
            Assert.IsTrue(expectedCountTails == actual);
        }

        [TestMethod]
        public void SymmetricMazeDontHavaNull()
        {
            foreach (var e in Maze.Walls)
            {
                Assert.IsTrue(e != null);
            }
            foreach (var e in Maze.Paths)
            {
                Assert.IsTrue(e != null);
            }
        }

        [TestMethod]
        public void NotSymmetricMazeFull()
        {
            int expectedCountTails = NotSymmetricMaze.Height * NotSymmetricMaze.Width;

            int actual = 0;
            foreach (var e in NotSymmetricMaze.Walls)
            {
                actual++;
            }
            foreach (var e in NotSymmetricMaze.Paths)
            {
                actual++;
            }
            Assert.IsTrue(expectedCountTails == actual);

        }

        [TestMethod]
        public void NotSymmetricMazeDontHavaNull()
        {
            foreach (var e in NotSymmetricMaze.Walls)
            {
                Assert.IsTrue(e != null);
            }
            foreach (var e in NotSymmetricMaze.Paths)
            {
                Assert.IsTrue(e != null);
            }
        }
        
        [TestMethod]
        public void CheckSymmetricsInNotSymmetricSizeMaze()
        {
            foreach(var path in NotSymmetricMaze.Paths)
            {
                if (path.Cell < Maze.Width/2)
                {
                    bool find = false;
                    foreach (var symmPath in NotSymmetricMaze.Paths)
                    {
                        if ((symmPath.Row == path.Row) & (symmPath.Cell == (NotSymmetricMaze.Width-2 - path.Cell)))
                        {
                            find = true;
                        }
                    }
                    Assert.IsTrue(find);
                }
            }
        }
        [TestMethod]
        public void CheckSymmetricsInSymmetricSizeMaze()
        {
            foreach (var path in Maze.Paths)
            {
                if (path.Cell < Maze.Width)
                {
                    bool find = false;
                    foreach (var symmPath in Maze.Paths)
                    {
                        if ((symmPath.Row == path.Row) & (symmPath.Cell == (Maze.Width - 1 - path.Cell)))
                        {
                            find = true;
                        }
                    }
                    Assert.IsTrue(find);
                }
            }
        }
        [TestMethod]
        public void CanStepToAllPaths_ReturnTrue()
        {
            foreach (var path in Maze.Paths)
            {
                Assert.IsTrue(Maze.StepTo(path.Row, path.Cell));
            }
        }
        [TestMethod]
        public void CanStepToAllWalls_ReturnFalse()
        {
            foreach (var wall in Maze.Walls)
            {
                Assert.IsFalse(Maze.StepTo(wall.Row, wall.Cell));
            }
        }
    }
}
