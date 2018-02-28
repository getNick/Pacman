using Autofac;
using GameCore.Classes;
using GameCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Services
{
    public class LayerService
    {
        public static IContainer Container { get; private set; }
        public static int LayerNumber { get;private set; } = 0;
        private static int GiftsToNextLayer;
        private IPlayer Player;
        private ApplicationService applicationService;
        public EventHandler LoadNewLayerEvent;
        public LayerService(){
            applicationService = new ApplicationService();
            Player = ApplicationService.Container.Resolve<IPlayer>();  

        }
        public static void  NewGame()
        {
            LayerNumber = 0;
        }
        public IContainer LoadLayer()
        {
            LayerNumber++;
            var builder = new ContainerBuilder();

            builder.RegisterInstance(Player).As<IPlayer>();

            builder.RegisterType<MazeService>().As<IMaze>()
                    .WithParameter(new NamedParameter("height", GameCore.EnumsAndConstant.GameConstants.MazeHeight))
                    .WithParameter(new NamedParameter("width", GameCore.EnumsAndConstant.GameConstants.MazeWidth))
                    .SingleInstance();
            builder.RegisterType<PacmanService>().As<IPacman>().WithParameter(new TypedParameter(typeof(int), GameCore.EnumsAndConstant.GameConstants.PacmanLifes)).SingleInstance();

            builder.RegisterType<Enemy>().As<IEnemy>();

            if (PluginsService.SelectedType == null)
            {
                PluginsService.SelectedType = PluginsService.GetRandomSelectedType();
            }
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            builder.RegisterAssemblyTypes(assemblies).As<IPursueAlgo>()
                .Where(t => t.Name == PluginsService.SelectedType.Name);

            builder.RegisterType<TimeService>().AsSelf().SingleInstance();

            builder.RegisterType<EnemyService>().WithParameter(new TypedParameter(typeof(int), LayerNumber)).SingleInstance();


            builder.RegisterType<GiftsService>()
                .WithParameter(new NamedParameter("player", Player));

            Container = builder.Build();

            var gifts = Container.Resolve<GiftsService>();
            GiftsToNextLayer+= gifts.GiftsCount;
            Player.PropertyChanged += Player_PropertyChanged;
            
            return Container;
        }

        private void Player_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Score")
            {
                if (Player.Score == GiftsToNextLayer)
                {
                    LoadNewLayerEvent?.Invoke(this, new EventArgs());
                }
            }
        }
    }
}
