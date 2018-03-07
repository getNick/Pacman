using GameCore.Classes;
using GameCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GameService.Services
{
    public class MazeService : IMaze
    {
       
        public int Height { get; set; }
        public int Width { get; set; }
        public IEnumerable<Wall> Walls { get; set; }
        public IEnumerable<Path> Paths { get; set; }
        public MazeService(int height, int width)
        {
            Height = height;
            Width = width;
            GameObject[,] _maze = new GameObject[height, width];
            int tetrisHeight = height;
            int tetrisWidth = width / 2;
            GameObject[,] _halfMaze = new GameObject[tetrisHeight, tetrisWidth];
            bool canInstallYet = true;
            int countFals = 0;
            while (countFals < GameCore.EnumsAndConstant.GameConstants.MaxCountUnsuccessfulInstall)
            {
                countFals++;
                RandomBlock randomBlock = new RandomBlock();
                for (int i = 1; i < tetrisHeight; i++)
                {
                    for (int j = 0; j < tetrisWidth; j++)
                    {
                        if ((_halfMaze[i, j] == null) & (tetrisWidth - j > randomBlock.Width) & (tetrisHeight - i > randomBlock.Height))
                        {
                            canInstallYet = TrySetBlock(_halfMaze, i, j, randomBlock);
                            if (canInstallYet)
                            {
                                countFals--;
                                SetPath(_halfMaze);
                                i = tetrisHeight;//return from double cycle
                                break;//return from double cycle
                            }
                        }
                    }
                }
            }
            SetBorder(_halfMaze);
            MirrorReflect(_halfMaze, _maze);
            FullMaze(_maze);
            Walls = GetWalls(_maze);
            Paths = GetPaths(_maze);
        }
        #region MazeGeneration
        /// <summary>
        /// Full all empty maze tails
        /// </summary>
        /// <param name="maze"></param>
        private void FullMaze(GameObject[,] maze)
        {
            int rows = maze.GetUpperBound(0) + 1;
            int columns = maze.Length / rows;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (maze[i, j] == null)
                    {
                        maze[i, j] = new Path(i, j);
                    }
                }
            }
        }/// <summary>
         /// Symmetrical reflection of the labyrinth
         /// </summary>
         /// <param name="halfMaze"></param>
         /// <param name="maze"></param>
        private void MirrorReflect(GameObject[,] halfMaze, GameObject[,] maze)
        {
            int rows = halfMaze.GetUpperBound(0) + 1;
            int columns = halfMaze.Length / rows;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (halfMaze[i, j] is Wall)
                    {
                        maze[i, columns - j - 1] = new Wall(i, columns - j - 1);
                        maze[i, columns + j] = halfMaze[i, j];
                        maze[i, columns + j].Row = i;
                        maze[i, columns + j].Cell = columns + j;
                    }
                }
            }
        }
        /// <summary>
        /// set Path around block
        /// </summary>
        /// <param name="maze"></param>
        private void SetPath(GameObject[,] maze)
        {
            int rows = maze.GetUpperBound(0) + 1;
            int columns = maze.Length / rows;
            for (int i = 0; i < rows - 1; i++)
            {
                for (int j = 0; j < columns - 1; j++)
                {
                    if (maze[i, j] is Wall)
                    {
                        if (maze[i, j + 1] == null)
                        {
                            maze[i, j + 1] = new Path(i, j + 1);
                        }
                        if (maze[i + 1, j] == null)
                        {
                            maze[i + 1, j] = new Path(i + 1, j);
                        }
                        if (maze[i + 1, j + 1] == null)
                        {
                            maze[i + 1, j + 1] = new Path(i + 1, j + 1);
                        }
                        if ((j > 1) && (maze[i + 1, j - 1] == null))
                        {
                            maze[i + 1, j - 1] = new Path(i + 1, j);
                        }

                    }
                }
            }
        }
        /// <summary>
        /// Try set block to Maze
        /// </summary>
        /// <param name="maze"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="randomBlock"></param>
        /// <returns>false if non succesfull</returns>
        private bool TrySetBlock(GameObject[,] maze, int i, int j, RandomBlock randomBlock)
        {
            foreach (var wall in randomBlock.Walls)
            {
                if (maze[i + wall.Row, j + wall.Cell] != null)
                {
                    return false;
                }
            }
            foreach (var wall in randomBlock.Walls)
            {
                maze[i + wall.Row, j + wall.Cell] = wall;
            }
            foreach (var wall in randomBlock.Walls)
            {
                wall.Row += i;
                wall.Cell += j;
            }
            return true;
        }
        /// <summary>
        /// set maze border
        /// </summary>
        /// <param name="maze"></param>
        private void SetBorder(GameObject[,] maze)
        {
            int rows = maze.GetUpperBound(0) + 1;
            int columns = maze.Length / rows;
            for (int i = 0; i < columns; i++)
            {
                maze[0, i] = new Wall(0, i);
                maze[rows - 1, i] = new Wall(rows - 1, i);
                if (columns - i > 2)
                {
                    maze[1, i] = new Path(1, i);
                    maze[rows - 2, i] = new Path(rows - 2, i);
                }

            }
            for (int i = 1; i < rows - 1; i++)
            {
                maze[i, columns - 1] = new Wall(i, columns - 1);
                maze[i, columns - 2] = new Path(i, columns - 2);
            }

        }
        /// <summary>
        /// Return all walls on maze
        /// </summary>
        /// <param name="maze"></param>
        /// <returns></returns>
        private IEnumerable<Wall> GetWalls(GameObject[,] maze)
        {
            List<Wall> listWall = new List<Wall>();
            int rows = maze.GetUpperBound(0) + 1;
            int columns = maze.Length / rows;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (maze[i, j] is Wall wall)
                    {
                        listWall.Add(wall);
                    }
                }
            }
            return listWall;
        }
        /// <summary>
        /// Return All Paths on maze
        /// </summary>
        /// <param name="maze"></param>
        /// <returns></returns>
        private IEnumerable<Path> GetPaths(GameObject[,] maze)
        {
            List<Path> listPath = new List<Path>();
            int rows = maze.GetUpperBound(0) + 1;
            int columns = maze.Length / rows;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (maze[i, j] is Path path)
                    {
                        listPath.Add(path);
                    }
                }
            }
            return listPath;
        }
        #endregion

        /// <summary>
        /// Can step at this position
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns>false if non succesfull</returns>
        public bool StepTo(int i, int j)
        {

            if ((i < 0) || (i >= Height) || (j < 0) || (j >= Width))
            {
                return false;
            }
            var path = Paths.FirstOrDefault((x) => x.Row == i && x.Cell == j);
            if (path != null)
            {
                return true;
            }
            return false;
        }
    }
}
