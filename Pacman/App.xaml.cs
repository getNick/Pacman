using Autofac;
using Pacman.Models;
using Pacman.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Pacman
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Create your builder.
            var builder = new ContainerBuilder();

            // Usually you're only interested in exposing the type
            // via its interface:
            builder.RegisterInstance(new Maze()).As<IMaze>().SingleInstance();


            // Scan an assembly for components
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
            .Where(t => t.Name.EndsWith("Repository"))
            .AsImplementedInterfaces()
            .InstancePerRequest();

            builder.RegisterType<MainWindowViewModel>().AsSelf();

            var container = builder.Build();
            
            var model = container.Resolve<MainWindowViewModel>();
            var view = new MainWindow { DataContext = model };
            view.Show();

           
        }
    }
}
