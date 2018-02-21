using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using WpfApplication.Utils;
using System.Windows.Input;

namespace WpfApplication.ViewModel
{
    class StartPageViewModel:ViewModelBase
    {
        private const string NonRegister= "к сожелению,\n мы еще не знакомы";
        public bool NewPlayer { get; set; } = true;
        public string PlayerName { get; set; }
        public StartPageViewModel()
        {
           var confName= ConfigurationManager.AppSettings["PlayerName"];
            if (confName.Length > 1)
            {
                PlayerName = confName;
                NewPlayer = false;
            }
            else
            {
                PlayerName = NonRegister;
            }
        }

        #region SetName
        RelayCommand _setName;
        public ICommand SetNameCommand
        {
            get
            {
                if (_setName == null)
                    _setName = new RelayCommand(SetName);
                return _setName;
            }
        }
        private void SetName(object parameter)
        {
            if (PlayerName.Length < 5)
            {
                return;
            }
            NewPlayer = false;
            OnPropertyChanged("PlayerName");
            OnPropertyChanged("NewPlayer");
        }

        #endregion
        #region ResetName
        RelayCommand _resetName;
        public ICommand ResetNameCommand
        {
            get
            {
                if (_resetName == null)
                    _resetName = new RelayCommand(ResetName);
                return _resetName;
            }
        }
        private void ResetName(object parameter)
        {
            PlayerName = NonRegister;
            NewPlayer = true;
            OnPropertyChanged("PlayerName");
            OnPropertyChanged("NewPlayer");
        }
        #endregion

        #region StartGame
        RelayCommand _startGame;
        public ICommand StartGameCommand
        {
            get
            {
                if (_startGame == null)
                    _startGame = new RelayCommand(ResetName);
                return _startGame;
            }
        }
        private void StartGame(object parameter)
        {

        }
        #endregion
    }
}
