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
        public LayerService LayerService { get;private set; }
        public IPlayer Player { get; set; }
        public RecordsViewModel()
        {
            LayerService=App.ViewContainer.Resolve<LayerService>();
            Player = LayerService.Container.Resolve<IPlayer>();
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
