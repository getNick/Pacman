using Autofac;
using WpfApplication.ViewModel;
using System.Windows;
using WpfApplication.Views;
using GameService.Services;
using System;
using NLog;

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
            try
            {
                var builder = new ContainerBuilder();
                builder.RegisterType<MainWindowViewModel>().AsSelf().SingleInstance();
                builder.RegisterType<LayerService>().AsSelf().SingleInstance();
                ViewContainer = builder.Build();
                var model = ViewContainer.Resolve<MainWindowViewModel>();
                var view = new StartWindow { DataContext = model };
                view.Show();
            }catch(Exception exception)
            {
                Logger logger = LogManager.GetLogger("fileLogger");
                logger.Error(exception, "Whoops!");
            }
        }
    }
}
