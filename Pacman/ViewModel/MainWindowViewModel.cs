using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using WpfApplication.Utils;
using WpfApplication.Views;
using GameCore.EnumsAndConstant;
using System.Configuration;
using WpfApplication.Resources.Models.Enums_and_Constants;

namespace WpfApplication.ViewModel
{
    class MainWindowViewModel:ViewModelBase
    {
        private  Page _curentPage;
        public Page CurrentPage
        {
            get
            {
                if (_curentPage == null)
                {
                    _curentPage = new StartPage();
                }
                return _curentPage;
            }
            set
            {
                _curentPage = value;
                OnPropertyChanged("CurrentPage");
            }

        }
        public MainWindowViewModel()
        {
            SetGameConstants();
        }
        void SetGameConstants()
        {
            GameConstants.PacmanLifes = int.Parse(ConfigurationManager.AppSettings.Get("PacmanLifes"));
            GameConstants.MaxCountUnsuccessfulInstall = int.Parse(ConfigurationManager.AppSettings.Get("MaxCountUnsuccessfulInstall"));
            GameConstants.MazeHeight = int.Parse(ConfigurationManager.AppSettings.Get("MazeHeight"));
            GameConstants.MazeWidth = int.Parse(ConfigurationManager.AppSettings.Get("MazeWidth"));
            GameConstants.PacmanCatchPause = int.Parse(ConfigurationManager.AppSettings.Get("PacmanCatchPause"));
            GameConstants.EatingTime = int.Parse(ConfigurationManager.AppSettings.Get("EatingTime"));
            GameConstants.CountRowsInRecords = int.Parse(ConfigurationManager.AppSettings.Get("CountRowsInRecords"));
            GameConstants.PauseBetweenSteps = int.Parse(ConfigurationManager.AppSettings.Get("PauseBetweenSteps"));
            GameConstants.PacmanRespointRow = int.Parse(ConfigurationManager.AppSettings.Get("PacmanRespointRow"));
            GameConstants.PacmanRespointCell = int.Parse(ConfigurationManager.AppSettings.Get("PacmanRespointCell"));
            GameConstants.MinRandomBlockLength = int.Parse(ConfigurationManager.AppSettings.Get("MinRandomBlockLength"));
            GameConstants.MaxRandomBlockLength = int.Parse(ConfigurationManager.AppSettings.Get("MaxRandomBlockLength"));
            GameConstants.MinRandomBlockBranchLength = int.Parse(ConfigurationManager.AppSettings.Get("MinRandomBlockBranchLength"));
            GameConstants.MaxRandomBlockBranchLength = int.Parse(ConfigurationManager.AppSettings.Get("MaxRandomBlockBranchLength"));
            GameConstants.ConnectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
        }
        
    }
}
