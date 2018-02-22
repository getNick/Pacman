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
    class MainGameViewModel:ViewModelBase
    {

        // ApplicationService applicationService;
        PlayerService PlayerService;
        public TestObject[] TestObjects { get; set; }
        public IMaze Maze { get; set; }
        public IPlayer Player { get; set; }
        public MainGameViewModel()
        {
            //applicationService = new ApplicationService();
            Maze = new MazeService(30, 30);
            PlayerService = new PlayerService();


            var t1 = new TestObject(10, 15, true, "text1");
            /*var t2 = new TestObject(0, -15, true, "text2");
            TestObjects = new TestObject[] {t1,t2};*/
            /*PlayerService = new PlayerService();
            PlayerService.CreateNewPlayer(ConfigurationManager.AppSettings["PlayerName"]);
            PlayerService.SaveResult();*/

        }
        Random rnd = new Random();
        public bool IsVisible { get; private set; } = true;
        

        public int TimeLeft { get; private set; } = 300;

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
            Score++;
            TimeLeft--;
            if (IsVisible == true)
            {
                IsVisible = false;
            }
            else
            {
                IsVisible = true;
            }


            OnPropertyChanged("HaveGift");
            OnPropertyChanged("TimeLeft");
            OnPropertyChanged("Score");
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
