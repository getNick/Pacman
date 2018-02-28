using Autofac;
using GameCore.Interfaces;
using GameService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApplication.Models;
using WpfApplication.Utils;
using WpfApplication.Views;

namespace WpfApplication.ViewModel
{
    class RecordsViewModel:ViewModelBase
    {
        IContainer ServiceContainer;
        DataLayerService DataLayer { get; set; }
        public int Score { get; set; } = 0;
        public IPlayer Player { get; set; }
        public List<Record> ListRecords { get; set; }
        private const int countRowsInResult = 5;
        public RecordsViewModel()
        {
            ServiceContainer = ApplicationService.Container;
            DataLayer=ServiceContainer.Resolve<DataLayerService>();
            Player = ServiceContainer.Resolve<IPlayer>();
            Score = Player.Score;
            DataLayer.AddRecord(Player);
            var listPlayer = DataLayer.GetTop(countRowsInResult);
            ListRecords = new List<Record>();
            for(int i = 0; i < listPlayer.Count(); i++)
            {
                ListRecords.Add(new Record(i, listPlayer.ElementAt(i).Name, listPlayer.ElementAt(i).Score));
            }
            ResetPlayersProgress();
        }
        void ResetPlayersProgress()
        {
            var player = LayerService.Container.Resolve<IPlayer>();
            player.ResetScore();
            LayerService.NewGame();
        }
        RelayCommand _tryAgain;
        public ICommand TryAganCommand
        {
            get
            {

                if (_tryAgain == null)
                    _tryAgain = new RelayCommand(TryAgain);
                return _tryAgain;
            }
        }
        private void TryAgain(object parameter)
        {
            var currentPage = App.ViewContainer.Resolve<MainWindowViewModel>();
            currentPage.CurrentPage = new MainGamePage();
        }

        RelayCommand _changeDiffucult;
        public ICommand ChangeDifficultCommand
        {
            get
            {

                if (_changeDiffucult == null)
                    _changeDiffucult = new RelayCommand(ChangeDifficult);
                return _changeDiffucult;
            }
        }
        private void ChangeDifficult(object parameter)
        {
            var currentPage = App.ViewContainer.Resolve<MainWindowViewModel>();
            currentPage.CurrentPage = new SelectPluginPage();
        }

        RelayCommand _exit;
        public ICommand ExitCommand
        {
            get
            {

                if (_exit == null)
                    _exit = new RelayCommand(Exit);
                return _exit;
            }
        }
        private void Exit(object parameter)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
    
}
