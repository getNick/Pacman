using Autofac;
using GameCore.Classes;
using GameCore.Enums;
using GameCore.Interfaces;
using GameService.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using WpfApplication.Utils;

namespace WpfApplication.ViewModel
{
    class MainGameViewModel : ViewModelBase
    {

        IContainer ServiceContainer { get; set; }
        private List<Vector> _listBricks;
        public IEnumerable<Vector> ListBricks {
            get
            {
                if (_listBricks == null)
                {
                    _listBricks = WallsToBricks(Maze.Walls);
                }
                return _listBricks;
            }
        }


        public IMaze Maze { get; set; }
        public IPlayer Player { get; set; }
        public IPacman Pacman { get; set; }
        public IEnumerable<IEnemy> ListEnemies{get;set;}
        public MainGameViewModel()
        {
            ApplicationService appserv = new ApplicationService();
            ServiceContainer = ApplicationService.Container;
            Maze = ServiceContainer.Resolve<IMaze>();
            //var data = ServiceContainer.Resolve<DataLayerService>();
            //data.AddRecord(new GameCore.Classes.Player("Oleg"));
            //foreach(var player in data.GetTop(5))
            //{
            //    Console.WriteLine(player.Name + " " + player.Score);
            //}
            Pacman = ServiceContainer.Resolve<IPacman>();
            Player = ServiceContainer.Resolve<IPlayer>();
            Player.ChangeName(ConfigurationManager.AppSettings["PlayerName"]);
            ListEnemies = ServiceContainer.Resolve<EnemyService>().ListEnemies;


        }
        public int TimeLeft { get; private set; } = 300;

        #region WellToBreaks

        private List<Vector> WallsToBricks(IEnumerable<Wall> walls)
        {
            List<Vector> list = new List<Vector>();
            double third = 1 / 3.0;
            Wall temp;
            foreach (var wall in walls)
            {
                list.Add(new Vector(wall.Row, wall.Cell));
                temp = walls.FirstOrDefault((x) => x.Row == wall.Row & x.Cell == wall.Cell + 1);
                if (temp != null)
                {
                    list.Add(new Vector(wall.Row, wall.Cell+third));
                }
                temp = walls.FirstOrDefault((x) => x.Row == wall.Row & x.Cell == wall.Cell - 1);
                if (temp != null)
                {
                    list.Add(new Vector(wall.Row, wall.Cell - third));
                }
                temp = walls.FirstOrDefault((x) => x.Row == wall.Row+1 & x.Cell == wall.Cell);
                if (temp != null)
                {
                    list.Add(new Vector(wall.Row+ third, wall.Cell));
                }
                temp = walls.FirstOrDefault((x) => x.Row == wall.Row - 1 & x.Cell == wall.Cell);
                if (temp != null)
                {
                    list.Add(new Vector(wall.Row - third, wall.Cell));
                }
            }
            return list;
        }

        #endregion

        #region MoveCommandRight
        RelayCommand _pacmanGoRight;
        public ICommand PacmanGoRight
        {
            get
            {

                if (_pacmanGoRight == null)
                    _pacmanGoRight = new RelayCommand(PacmanGoRightCommand);
                return _pacmanGoRight;
            }
        }
        private void PacmanGoRightCommand(object parameter)
        {
            Pacman.Direction = Direction.Up;
            Pacman.Step();
        }
        #endregion
        #region MoveCommandLeft
        RelayCommand _pacmanGoLeft;
        public ICommand PacmanGoLeft
        {
            get
            {

                if (_pacmanGoLeft == null)
                    _pacmanGoLeft = new RelayCommand(PacmanGoLeftCommand);
                return _pacmanGoLeft;
            }
        }
        private void PacmanGoLeftCommand(object parameter)
        {
            Pacman.Direction = Direction.Down;
            Pacman.Step();
        }
        #endregion
        #region MoveCommandUp
        RelayCommand _pacmanGoUp;
        public ICommand PacmanGoUp
        {
            get
            {

                if (_pacmanGoUp == null)
                    _pacmanGoUp = new RelayCommand(PacmanGoUpCommand);
                return _pacmanGoUp;
            }
        }
        private void PacmanGoUpCommand(object parameter)
        {
            Pacman.Direction = Direction.Right;
            Pacman.Step();
        }

        #endregion
        #region MoveCommanDown
        RelayCommand _pacmanGoDown;
        public ICommand PacmanGoDown
        {
            get
            {

                if (_pacmanGoDown == null)
                    _pacmanGoDown = new RelayCommand(PacmanGoDownCommand);
                return _pacmanGoDown;
            }
        }
        private void PacmanGoDownCommand(object parameter)
        {
            Pacman.Direction = Direction.Left;
            Pacman.Step();
        }

        #endregion

        #region Score
        public int Score { get; private set;}
        RelayCommand _scoreIncCommand;
        public ICommand ScoreInc
        {
            get
            {
                if (_scoreIncCommand == null)
                    _scoreIncCommand = new RelayCommand(ScoreIncCommand);
                return _scoreIncCommand;
            }
        }
        private void ScoreIncCommand(object parameter)
        {
            Player.AddToScore(10);
            Console.WriteLine(Pacman.Row+" " +Pacman.Cell);
            /*TimeLeft--;
            if (IsVisible == true)
            {
                IsVisible = false;
            }
            else
            {
                IsVisible = true;
            }*/


            OnPropertyChanged("HaveGift");
            OnPropertyChanged("TimeLeft");
            //OnPropertyChanged("Score");
        }
        #endregion
        
    }
    class TestObject
    {
        public TestObject(int x,int y,bool visible,string text)
        {
            X = x;
            Y = y;
            Visible = visible;
            Text = text;
        }
        public int X { get; set; }
        public int Y { get; set; }
        public bool Visible { get; set; } = true;
        public string Text { get; set; } = "lolololo";
    }
}
