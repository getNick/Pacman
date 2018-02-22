using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using WpfApplication.ViewModel;
using WpfApplication.Views;

namespace WpfApplication.Utils
{
    public static class IoC
    {
        public static IContainer Container { get; }
        static IoC()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<MainWindowViewModel>();
            Container = builder.Build();
           /* builder.RegisterInstance(mazeService.Maze).As<IMaze>();
            builder.RegisterType<PacmanService>().As<IPacman>();
            Container = builder.Build();*/
        }
    }
}
