using Autofac;
using GameCore.Classes;
using GameCore.EnumsAndConstant;
using GameCore.Interfaces;
using GameService.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using WpfApplication.Utils;
using WpfApplication.Views;

namespace WpfApplication.ViewModel
{
    class MainGameViewModel : ViewModelBase
    {
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
        private bool newLayerLoaded = false;
        public bool ViewResult { get; set; }
        private int _layerNumber = 0;
        public int LayerNumber {
            get
            {
                return _layerNumber;
            }
            set
            {
                _layerNumber = value;
                OnPropertyChanged("LayerNumber");
            }
        }
        public string AssemblyPath
        {/// <summary>
         /// find current directory path
         /// </summary>
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return  System.IO.Path.GetDirectoryName(path);
            }
        }

        public MainGameViewModel()
        {
            var layerServ = App.ViewContainer.Resolve<LayerService>();
            var LayerContainer = layerServ.LoadLayer();
            Maze = LayerContainer.Resolve<IMaze>();
            Pacman = LayerContainer.Resolve<IPacman>();
            Player = LayerContainer.Resolve<IPlayer>();
            Player.ChangeName(Properties.Settings.Default.UserName);//set to player instance,saved on settings value
            ListEnemies = LayerContainer.Resolve<EnemyService>().ListEnemies;
            Pacman.PacmenDead += Pacman_PacmenDead;
            layerServ.LoadNewLayerEvent += LoadNewLayer;
            LayerNumber = layerServ.LayerNumber;
        }/// <summary>
        /// End layer event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadNewLayer(object sender, EventArgs e)
        {
            if (newLayerLoaded == false)
            {
                var currentPage = App.ViewContainer.Resolve<MainWindowViewModel>();
                currentPage.CurrentPage = new MainGamePage();
                newLayerLoaded = true;
            }
        }
        /// <summary>
        /// Method PacmenDead event what display result
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pacman_PacmenDead(object sender, EventArgs e)
        {
            ViewResult = true;
            OnPropertyChanged("ViewResult");
        }


        #region WellToBreaks
        /// <summary>
        /// Convertor walls to break
        /// </summary>
        /// <param name="walls"></param>
        /// <returns>List<Vector></returns>
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

        RelayCommand _newGameCommand;
        public ICommand NewGameCommand
        {
            get
            {
                if (_newGameCommand == null)
                    _newGameCommand = new RelayCommand(LoadNewGame);
                return _newGameCommand;
            }
        }
        private void LoadNewGame(object parameter)
        {
            if (newLayerLoaded == false)
            {
                var currentPage = App.ViewContainer.Resolve<MainWindowViewModel>();
                currentPage.CurrentPage = new MainGamePage();
                newLayerLoaded = true;
            }
        }
        
    }
}
