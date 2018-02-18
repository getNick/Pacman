using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;

namespace Pacman.Models
{
    class Maze:IMaze
    {
        private GameObject[,] _maze;
        GameObject[,] _halfMaze;
        private int height;
        private int width;

        public Maze() : this(30, 30)
        {

        }
        public Maze(int height, int width)
        {
            this.height = height;
            this.width = width;
            int tetrisHeight = height;
            int tetrisWidth = width / 2;
            _halfMaze = new GameObject[tetrisHeight, tetrisWidth];
            bool canInstallYet = true;
            int countFals = 0;
            while (countFals<100)
            {
                countFals++;
                RandomBlock randomBlock= new RandomBlock();
                for (int i = 1; i < tetrisHeight; i++)//1 line border
                {
                    for (int j = 0; j < tetrisWidth; j++)
                    {
                        if ((_halfMaze[i,j]==null)&(tetrisWidth - j > randomBlock.Width) & (tetrisHeight - i > randomBlock.Height))
                        {
                            canInstallYet = TrySetBlock(_halfMaze, i, j, randomBlock);
                            if (canInstallYet)
                            {
                                countFals--;
                                SetPath(_halfMaze);
                                i = tetrisHeight;
                                break;
                            }
                        }
                    }
                }
            }
            SetBorder(_halfMaze);
            MirrorReflect(_halfMaze);
            FullMaze();
        }
        private void FullMaze()
        {
            int rows = _maze.GetUpperBound(0) + 1;
            int columns = _maze.Length / rows;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (_maze[i, j]==null)
                    {
                        _maze[i, j] = new Path(i, j);
                    }
                }
            }
        }
        private void MirrorReflect(GameObject[,] halfMaze)
        { 
            _maze=new GameObject[height, width];
            int rows = halfMaze.GetUpperBound(0) + 1;
            int columns = halfMaze.Length / rows;
            for (int i = 0; i < rows; i++)
            {
                for(int j = 0; j < columns; j++)
                {
                    if(halfMaze[i,j] is Wall)
                    {
                        _maze[i, columns - j - 1] = new Wall(i, columns - j - 1);
                        _maze[i, columns + j] = halfMaze[i, j];
                        _maze[i, columns + j].GridPosition.X = i;
                        _maze[i, columns + j].GridPosition.Y = columns+j;
                    }
                }
            }
        }
        private void SetPath(GameObject[,] maze)
        {
            int rows = maze.GetUpperBound(0) + 1;
            int columns = maze.Length / rows;
            for(int i = 0; i < rows-1; i++)
            {
                for(int j = 0; j < columns-1; j++)
                {
                    if(maze[i,j]is Wall)
                    {
                        if (maze[i, j + 1] == null)
                        {
                            maze[i, j + 1] = new Path(i, j + 1);
                        }
                        if (maze[i + 1, j] == null)
                        {
                            maze[i + 1, j] = new Path(i + 1, j);
                        }
                        if (maze[i+1, j + 1] == null)
                        {
                            maze[i+1, j + 1] = new Path(i+1, j + 1);
                        }
                        if ((j>1)&&(maze[i + 1, j-1] == null))
                        {
                            maze[i + 1, j-1] = new Path(i + 1, j);
                        }

                    }
                }
            }
        }
        private bool TrySetBlock(GameObject[,] maze,int i,int j,RandomBlock randomBlock)
        {
            foreach(var wall in randomBlock.ListWalls)
            {
                if (maze[i + (int)wall.GridPosition.X, j + (int)wall.GridPosition.Y] != null)
                {
                    return false;
                }
            }
            foreach (var wall in randomBlock.ListWalls)
            {
                maze[i + (int)wall.GridPosition.X, j + (int)wall.GridPosition.Y] = wall;
            }
            foreach (var wall in randomBlock.ListWalls)
            {
                wall.GridPosition.X += i;
                wall.GridPosition.Y += j;
            }
            return true;
        }
        private void SetBorder(GameObject[,] maze)
        {
            int rows = maze.GetUpperBound(0) + 1;
            int columns = maze.Length / rows;
            for(int i = 0; i < columns; i++)
            {
                maze[0, i] = new Wall(0, i);
                maze[rows-1, i] = new Wall(rows-1, i);
                if (columns - i > 2)
                {
                    maze[1, i] = new Path(1, i);
                    maze[rows - 2, i] = new Path(rows - 2, i);
                }
                
            }
            for(int i = 1; i < rows-1; i++)
            {
                maze[i, columns - 1] = new Wall(i, columns - 1);
                maze[i, columns - 2] = new Path(i, columns - 2);
            }
            int halfRows = rows / 2;
            maze[halfRows - 3, 0] = new Path(halfRows - 3, 0);
            maze[halfRows - 2, 0] = new Path(halfRows - 2, 0);
            maze[halfRows - 1, 0] = new Path(halfRows - 1, 0);
            maze[halfRows - 1, 1] = new Wall(halfRows - 1, 1);
            maze[halfRows - 1, 2] = new Wall(halfRows - 1, 2);
            maze[halfRows , 0] = new Path(halfRows, 0);
            maze[halfRows, 1] = new Path(halfRows, 1);
            maze[halfRows , 2] = new Wall(halfRows, 2);
            maze[halfRows + 1, 0] = new Path(halfRows + 1, 0);
            maze[halfRows + 1, 1] = new Path(halfRows + 1, 1);
            maze[halfRows + 1, 2] = new Wall(halfRows + 1, 2);
            maze[halfRows + 2, 0] = new Wall(halfRows + 2, 0);
            maze[halfRows + 2, 1] = new Wall(halfRows + 2, 1);
            maze[halfRows + 2, 2] = new Wall(halfRows + 2, 2);
        }

        private List<Vector> listBricks;
        public List<Vector> GetWall()
        {
            List<Vector> listWall = new List<Vector>();
            int rows = _maze.GetUpperBound(0) + 1;
            int columns = _maze.Length / rows;
            for (int i = 0; i < rows; i++)
            {
                for(int j = 0; j < columns; j++)
                {
                    var wall= _maze[i, j] as Wall;
                    if(wall!=null)
                    {
                        foreach(var brick in wall.GetEnvironment(_maze))
                        {
                            listWall.Add(brick);
                        }
                    } 
                }
                Console.WriteLine();
            }
            return listWall;
        }

        public IEnumerable<Vector> Walls
        {
            get
            {
                if (listBricks == null)
                {
                    listBricks = GetWall();
                }
                return listBricks;
            }
        }
        private ObservableCollection<Path> listGifts;
        public ObservableCollection<Path> GetAllPath()
        {
            ObservableCollection<Path> listPath = new ObservableCollection<Path>();
            int rows = _maze.GetUpperBound(0) + 1;
            int columns = _maze.Length / rows;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    var path = _maze[i, j] as Path;
                    if (path != null)
                    {
                        listPath.Add(path);
                    }
                }
            }
            return listPath;
        }
        public ObservableCollection<Path> Gifts {
            get
            {
                if (listGifts == null)
                {
                    listGifts = GetAllPath();
                }
                return listGifts;
            }
        }

        public bool StepTo(int i, int j,MoveObject moveObject=null)
        {
            if ((i < 0) || (i >= height) || (j < 0) || (j >= width))
            {
                return false;
            }
            var path = _maze[i, j] as Path;
            if (path != null)
            {
                if(moveObject is Pacman)
                {
                    path.UseGift();
                }
                return true;
            }
            return false;
        }
    }
}
