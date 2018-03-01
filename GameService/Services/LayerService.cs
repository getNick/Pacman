using Autofac;
using GameCore.Classes;
using GameCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Services
{
    public class LayerService: INotifyPropertyChanged
    {
        public static Autofac.IContainer Container { get; private set; }
        public  int LayerNumber { get;private set; }
        private  int GiftsToNextLayer;
        private IPlayer Player;
        private ApplicationService applicationService;
        public EventHandler LoadNewLayerEvent;
        public bool LoadNewGame = true;
        List<IPlayer> _listRecords;
        public List<IPlayer> ListRecords
        {
            get
            {
                return _listRecords;
            }
            private set
            {
                _listRecords = value;
                OnPropertyChanged("ListRecords");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public LayerService(){
            applicationService = new ApplicationService();
            Player = ApplicationService.Container.Resolve<IPlayer>();
            Task t = GetRecordsAsync();

        }
        async Task GetRecordsAsync()
        {
             await Task.Run(() =>
            {
                var dataLayer = ApplicationService.Container.Resolve<DataLayerService>();
                ListRecords = dataLayer.GetTop(GameCore.EnumsAndConstant.GameConstants.CountRowsInRecords).ToList();
            });

        }
        public Autofac.IContainer LoadLayer()
        {
            if (LoadNewGame)
            {
                Player.ResetScore();
                LayerNumber = 0;
                GiftsToNextLayer = 0;
                LoadNewGame = false;
            }
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
            var timeServ = Container.Resolve<TimeService>();
            LoadNewLayerEvent += timeServ.StopWorking;
            var Pacman = Container.Resolve<IPacman>();
            Pacman.PacmenDead += PacmanDead;
            Pacman.PacmenDead+= timeServ.StopWorking;
            
            return Container;
        }

        private void PacmanDead(object sender, EventArgs e)
        {

            var DataLayer = ApplicationService.Container.Resolve<DataLayerService>();
            DataLayer.AddRecord(Player);
            if (ListRecords.Count == GameCore.EnumsAndConstant.GameConstants.CountRowsInRecords)
            {
                int minRecord = ListRecords.Min(x => x.Score);
                if (minRecord < Player.Score)
                {
                    ListRecords.Add(Player);
                    ListRecords.Remove(ListRecords.First(x => x.Score == minRecord));
                    ListRecords = ListRecords.OrderByDescending(x => x.Score).ToList();
                }
            }
            else
            {
                ListRecords.Add(Player);
                ListRecords = ListRecords.OrderByDescending(x => x.Score).ToList();
            }
            LoadNewGame = true;
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
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
