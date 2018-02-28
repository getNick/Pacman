using Autofac;
using GameService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApplication.Utils;
using WpfApplication.Views;

namespace WpfApplication.ViewModel
{
    class SelectPluginViewModel:ViewModelBase
    {
        private PluginsService PluginsService { get; set; }
        public List<string> ListPluginsName { get; set; }
        public string SelectedPlugin { get; set; }

        public SelectPluginViewModel()
        {
            PluginsService = new PluginsService();
            ListPluginsName = new List<string>();
            foreach (var plugin in PluginsService.ListPlugins)
            {
                ListPluginsName.Add(plugin.Name);
            }
            SelectedPlugin = ListPluginsName.First();
        }
        RelayCommand _selectPlugin;
        public ICommand SelectPluginCommand
        {
            get
            {
                if (_selectPlugin == null)
                    _selectPlugin = new RelayCommand(SelectPlugin);
                return _selectPlugin;
            }
        }
        private void SelectPlugin(object parameter)
        {
            var plug= PluginsService.ListPlugins.FirstOrDefault(x => x.Name == SelectedPlugin);
            PluginsService.SelectedType = plug;
            var currentPage = App.ViewContainer.Resolve<MainWindowViewModel>();
            currentPage.CurrentPage = new MainGamePage();
        }
    }
}
