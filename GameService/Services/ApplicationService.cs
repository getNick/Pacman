using Autofac;
using GameCore.Interfaces;
using GameCore.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataLayer;

namespace GameService.Services
{
    public class ApplicationService
    {
        public static IContainer Container { get; private set; }
        public IMaze Maze { get;}
        public IPacman Pacman { get; }
        public IPlayer Player { get; }
        public ApplicationService()
        {

            var builder = new ContainerBuilder();
            builder.RegisterType<MazeService>().As<IMaze>()
                .WithParameters(new[] {
                    new TypedParameter(typeof(int), 24),
                    new TypedParameter(typeof(int), 24)
                }).SingleInstance() ;

            builder.RegisterType<DataLayerService>().AsSelf();

            builder.RegisterType<Player>().As<IPlayer>().AsSelf().SingleInstance();
           
            builder.RegisterType<PacmanService>().As<IPacman>().SingleInstance();

            builder.RegisterType<EnemyService>().WithParameter(new TypedParameter(typeof(int), 3)).SingleInstance();

            builder.RegisterType<AStarAlgo>().As<IPursueAlgo>();

            builder.RegisterType<TimeService>().AsSelf().SingleInstance();

            Container = builder.Build();

            GiftsService gifts = new GiftsService(Container.Resolve<IMaze>(), Container.Resolve<IPlayer>());

            Pacman = Container.Resolve<IPacman>();
            var timeServ = Container.Resolve<TimeService>();
            //timeServ.StepEvent += new TimeService.TimeToStep(Pacman.Step);
            var enemies = Container.Resolve<EnemyService>();
            var en = enemies.ListEnemies[0];
            //timeServ.StepEvent += new TimeService.TimeToStep(en.Step);
            for (int i=0;i<enemies.ListEnemies.Count;i++)
            {
                timeServ.StepEvent += new TimeService.TimeToStep(enemies.ListEnemies[i].Step);
            }


        }
       
    }
}
