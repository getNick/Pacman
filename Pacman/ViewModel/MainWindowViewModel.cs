using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using Pacman.Models;

namespace Pacman.ViewModel
{
    class MainWindowViewModel: ViewModelBase
    {
        public Maze Maze = new Maze();
        public int PacmanRowPos { get; set; } = 0;
        public int PacmanCallPos { get; set; } = 0;
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
        private void PacmanGoRightCommand()
        {
            PacmanCallPos++;
            RaisePropertyChanged("PacmanCallPos");
        }
        #region Score
        public int Score { get; private set;}
        RelayCommand _scoreInc;
        public ICommand ScoreInc
        {
            get
            {
                if (_scoreInc == null)
                    _scoreInc = new RelayCommand(ScoreIncCommand, CanExecuteScoreIncCommand);
                return _scoreInc;
            }
        }
        private void ScoreIncCommand()
        {
            Score++;
            RaisePropertyChanged("Score");
        }
        private bool CanExecuteScoreIncCommand()
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
