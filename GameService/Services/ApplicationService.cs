using Autofac;
using GameCore.Interfaces;
using GameCore.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Services
{
    public class ApplicationService
    {
        IContainer Container { get;}
        public ApplicationService()
        {
            MazeService mazeService = new MazeService(30,30);
            var builder = new ContainerBuilder();
            //builder.RegisterControllers(typeof().Assembly);
            builder.RegisterType<PlayerService>();
            //builder.RegisterInstance(mazeService.Maze).As<IMaze>();
            builder.RegisterType<PacmanService>().As<IPacman>();
            Container=builder.Build();
            
            //builder.RegisterType<AStarAlgo>().As<IPursueAlgo>().WithParameter(new TypedParameter(typeof(IMaze), Container.Resolve<IMaze>()));

            //EnemyService enemyService = new EnemyService(3, Container.Resolve<IMaze>(), Container.Resolve<IPacman>(), Container.Resolve<IPursueAlgo>());
            
            //GiftsService giftsService = new GiftsService(Container.Resolve<IMaze>(), Container.Resolve<IPlayer>());
        }
       
    }
}
