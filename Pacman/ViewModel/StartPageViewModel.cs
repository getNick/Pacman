using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using WpfApplication.Utils;
using System.Windows.Input;
using System.Windows.Controls;
using WpfApplication.Views;
using Autofac;
using GameCore.Interfaces;
using System.IO;
using GameService.Services;
using System.Globalization;

namespace WpfApplication.ViewModel
{
    class StartPageViewModel : ViewModelBase
    {
        public IEnumerable<CultureInfo> Languages { get; set; }
        private bool _newPlayer = true;
        public bool NewPlayer
        {
            get
            {
                return _newPlayer;
            }
            set
            {
                _newPlayer = value;
                OnPropertyChanged("NewPlayer");
            }
        }

        private string _playerName;
        public string PlayerName
        {
            get
            {
                return _playerName;
            }
            set
            {
                _playerName = value;
                OnPropertyChanged("PlayerName");
            }
        }
        public StartPageViewModel()
        {
            PlayerName = Properties.Settings.Default.UserName;
            if (PlayerName.Length > 1)
            {
                NewPlayer = false;
            }
            else
            {
                NewPlayer = true;
            }
            Languages = App.Languages;
            
        }

        RelayCommand _changeLanguage;
        public ICommand ChangeLanguageCommand
        {
            get
            {
                if (_changeLanguage == null)
                    _changeLanguage = new RelayCommand(ChangeLanguage);
                return _changeLanguage;
            }
        }
        private void ChangeLanguage(object parameter)
        {
            var lang = parameter as CultureInfo;
            App.Language = lang;
            Console.WriteLine(lang.DisplayName);
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
            if (PlayerName.Length < 3)
            {
                return;
            }
            NewPlayer = false;
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
            PlayerName = "";
            NewPlayer = true;
        }
        #endregion

        #region StartGame
        RelayCommand _startGame;
        public ICommand StartGameCommand
        {
            get
            {
                if (_startGame == null)
                    _startGame = new RelayCommand(StartGame);
                return _startGame;
            }
        }
        private void StartGame(object parameter)
        {
            if (PlayerName != "")
            {
                Properties.Settings.Default.UserName = PlayerName;
                Properties.Settings.Default.Save();
            }
           
            var currentPage = App.ViewContainer.Resolve<MainWindowViewModel>();
            currentPage.CurrentPage = new SelectPluginPage();
            
        }
        #endregion
    }
}
