using Autofac;
using GameCore.Interfaces;
using GameCore.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameDataLayer;
using GameCore.Plagins;

namespace GameService.Services
{
    public class ApplicationService
    {
        public static IContainer Container { get; private set; }
        public ApplicationService()
        {
            #region builder setup
            var builder = new ContainerBuilder();
            var maze = new MazeService(24, 24);
            builder.RegisterInstance(maze).As<IMaze>().SingleInstance();
            /*builder.RegisterType<MazeService>().As<IMaze>()
                .WithParameters(new[] {
                    new TypedParameter(typeof(int), 24),
                    new TypedParameter(typeof(int), 24)
                }).SingleInstance() ;
            */
            builder.RegisterType<DataLayerService>().AsSelf();

            builder.RegisterType<Player>().As<IPlayer>().AsSelf().SingleInstance();
           
            builder.RegisterType<PacmanService>().As<IPacman>().SingleInstance();

            builder.RegisterType<EnemyService>().WithParameter(new TypedParameter(typeof(int), 3)).SingleInstance();

            var ins = Activator.CreateInstance(PluginsService.SelectedType, new object[] { maze });

            builder.RegisterInstance(ins).As<IPursueAlgo>();


            builder.RegisterType<TimeService>().AsSelf().SingleInstance();

            Container = builder.Build();
            #endregion

            GiftsService gifts = new GiftsService(Container.Resolve<IMaze>(), Container.Resolve<IPlayer>());

            var timeServ = Container.Resolve<TimeService>();
            //timeServ.StepEvent += new TimeService.TimeToStep(Pacman.Step);
            var enemies = Container.Resolve<EnemyService>();

            for (int i=0;i<enemies.ListEnemies.Count;i++)
            {
                timeServ.StepEvent += new TimeService.TimeToStep(enemies.ListEnemies[i].Step);
            }


        }
       
    }
}
