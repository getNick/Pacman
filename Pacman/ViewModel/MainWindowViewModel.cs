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
    }
}
