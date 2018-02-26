using Autofac;
using WpfApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using WpfApplication.Utils;
using WpfApplication.Views;
using GameCore.Classes;
using GameCore.Interfaces;

namespace WpfApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Autofac.IContainer ViewContainer;
        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<MainWindowViewModel>().AsSelf().SingleInstance();
            ViewContainer = builder.Build();
            var model = ViewContainer.Resolve<MainWindowViewModel>();
            var view = new StartWindow { DataContext = model };
            view.Show();
        }
    }
}
