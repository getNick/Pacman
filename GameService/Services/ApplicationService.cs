using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Services
{
    class ApplicationService
    {
        IContainer Container { get;}
        public ApplicationService()
        {
            MazeService mazeService = new MazeService(30,30);
            var builder = new ContainerBuilder();
            builder.RegisterType<>().As<>();
        }
       
    }
}
