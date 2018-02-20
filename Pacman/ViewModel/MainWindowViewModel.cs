using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using WpfApplication.Models;
using WpfApplication.Utils;

namespace WpfApplication.ViewModel
{
    class MainWindowViewModel:ViewModelBase
    {
        public IPlayer Player;
        public IMaze Maze;
        public IEnemy Enemy;
        public MainWindowViewModel(IMaze maze)
        {
           
            Maze = maze;

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
                    _scoreIncCommand = new RelayCommand(ScoreIncCommand, CanExecuteScoreIncCommand);
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
            OnPropertyChanged("IsVisible");
            OnPropertyChanged("TimeLeft");
            OnPropertyChanged("Score");
        }
        private bool CanExecuteScoreIncCommand(object parameter)
        {
            if (Score < 15)
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}
