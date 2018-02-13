using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.Models
{
    class Maze
    {
        private int[,] _maze;
        private int[,] _likeTetrisMaze;
        private int height;
        private int width;

        public Maze():this(20,18)
        {
           
        }
        public Maze(int height,int width)
        {
            int tetrisHeight = height;
            int tetrisWidth = width / 2;
            _likeTetrisMaze = new TetrisMaze(tetrisHeight, tetrisWidth).GetMaze();
            _maze = new int[height, width];
            int current;
            for(int i = 0; i < tetrisHeight; i++)
            {
                int coll = 0;
                current = _likeTetrisMaze[i, 0];
                for (int j = 0; j < tetrisWidth; j++,coll++)
                {
                    if (_likeTetrisMaze[i, j] != current)
                    {
                        _maze[i, ++coll] = 1;
                        current = _likeTetrisMaze[i, j];
                    }
                    else {
                        _maze[i, coll] = 1;
                    }
                }
            }
            /*_maze[0, 1] = 1;
            _maze[1, 1] = 1;
            _maze[2, 1] = 1;
            _maze[3, 1] = 1;
            _maze[4, 1] = 1;
            _maze[4, 2] = 1;
            _maze[4, 3] = 1;
            _maze[4, 4] = 1;
            _maze[4, 5] = 1;
            _maze[4, 6] = 1;
            _maze[4, 7] = 1;
            _maze[3, 7] = 1;
            _maze[2, 7] = 1;
            _maze[1, 7] = 1;
            _maze[0, 7] = 1;*/

        }
        private class TetrisMaze 
        {
            private int[,] _maze;
            private int height;
            private int width;
            public TetrisMaze(int height, int width)
            {
                this.height = height;
                this.width = width;
                _maze = new int[height, width];
                FillMaze();
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        Console.Write(_maze[i, j]+" ");
                    }
                    Console.WriteLine();
                }
            }
            public int[,] GetMaze()
            {
                return _maze;
            }
            public void FillMaze()
            {
                Random rnd = new Random();
                int marker = 1;
                for(int i=0;i< height; i++)
                {
                    for(int j = 0; j < width; j++)
                    {
                        if (_maze[i, j] == 0)
                        {
                            switch (rnd.Next(2))
                            {
                                case 0:
                                    {
                                        if (!TrySetBlock2(i, j, marker))
                                        {
                                            TrySetBlock1(i, j, marker);
                                        }
                                    }break;
                                case 1:
                                    {
                                        TrySetBlock1(i, j, marker);
                                    }
                                    break;
                            }
                            marker++;
                        }
                    }
                }

            }
            private bool TrySetBlock1(int i,int j,int marker)
            {
                // --- | block
                if ((j + 2) < width)
                {
                    if((_maze[i, j+1] == 0)&& (_maze[i, j + 2] == 0))
                    {
                        _maze[i, j] =_maze[i, j+1] =_maze[i, j+2] = marker;
                        return true;
                    }
                }
                else if ((i + 2) < height)
                {
                    if ((_maze[i+1, j] == 0) && (_maze[i+2, j] == 0))
                    {
                        _maze[i, j] =_maze[i+1, j] =_maze[i+2, j] = marker;
                        return true;
                    }
                }
                return false;
            }
            private bool TrySetBlock2(int i, int j, int marker)
            {
                // L block
                if (((j + 2) < width) && ((i + 2) < height))
                {
                    if ((_maze[i, j + 1] == 0) && (_maze[i, j + 2] == 0))
                    {
                        if ((_maze[i + 1, j] == 0) && (_maze[i + 2, j] == 0))
                        {
                            _maze[i, j] = _maze[i, j + 1] = _maze[i, j + 2] = _maze[i + 1, j] = _maze[i + 2, j] = marker;
                            return true;
                        } else if ((_maze[i + 1, j + 2] == 0) && (_maze[i + 2, j + 2] == 0))
                        {
                            _maze[i, j] = _maze[i, j + 1] = _maze[i, j + 2] = _maze[i + 1, j + 2] = _maze[i + 2, j + 2] = marker;
                            return true;
                        }
                    } else if ((_maze[i + 1, j] == 0) && (_maze[i + 2, j] == 0))
                    {
                        if ((j >= 2) && (_maze[i + 2, j - 1] == 0) && (_maze[i + 2, j - 2] == 0))
                        {
                            _maze[i, j] = _maze[i + 1, j] = _maze[i + 2, j] = _maze[i + 2, j - 1] = _maze[i + 2, j - 2] = marker;
                            return true;
                        }
                        else if ((_maze[i + 2, j + 1] == 0) && (_maze[i + 2, j + 2] == 0))
                        {
                            _maze[i, j] = _maze[i + 1, j] = _maze[i + 2, j] = _maze[i + 2, j + 1] = _maze[i + 2, j + 2] = marker;
                            return true;
                        }
                    }

                } 
                return false;
            }

        }
        public IEnumerable<Wall> Walls
        {
            get
            {
                if (listWall == null)
                {
                    listWall = GetWall();
                }
                return listWall;
            }
        }
        private List<Wall> listWall;
        public List<Wall> GetWall()
        {
            List<Wall> listWall = new List<Wall>();
            int rows = _maze.GetUpperBound(0) + 1;
            int columns = _maze.Length / rows;
            for (int i = 0; i < rows; i++)
            {
                for(int j = 0; j < columns; j++)
                {
                    Console.Write(_maze[i, j]+" ");
                    if (_maze[i, j] == 1)
                    {
                        listWall.Add(new Wall(i*5, j*5));
                    } 
                }
                Console.WriteLine();
            }
            return listWall;
        }
    }
}
