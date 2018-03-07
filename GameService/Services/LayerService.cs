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
using GameCore.EnumsAndConstant;

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
        /// <summary>
        /// Async reques to db and set in ListRecords collection
        /// </summary>
        /// <returns></returns>
        async Task GetRecordsAsync()
        {
             await Task.Run(() =>
            {
                var dataLayer = ApplicationService.Container.Resolve<DataLayerService>();
                ListRecords = dataLayer.GetTop(GameConstants.CountRowsInRecords).ToList();
            });

        }
        /// <summary>
        /// Generate new layer
        /// </summary>
        /// <returns>Autofac.Container</returns>
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
                .WithParameter(new NamedParameter(GameConstants.NamedParameterMazeHeight, GameConstants.MazeHeight))
                .WithParameter(new NamedParameter(GameConstants.NamedParameterMazeWidth, GameConstants.MazeWidth))
                .SingleInstance();

            builder.RegisterType<PacmanService>().As<IPacman>()
                .WithParameter(new NamedParameter(GameConstants.NamedParameterPacmanCountLifes, GameConstants.PacmanLifes))
                .WithParameter(new NamedParameter(GameConstants.NamedParameterPacmanTimeInvulnerable, GameConstants.PacmanCatchPause))
                .SingleInstance();

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
                .WithParameter(new NamedParameter(GameConstants.NamedParameterPlayer, Player));

            Container = builder.Build();

            //Save layer target
            var gifts = Container.Resolve<GiftsService>();
            gifts.SetGifts();
            GiftsToNextLayer+= gifts.GiftsCount;
            //add event Score update
            Player.PropertyChanged += Player_PropertyChanged;
            //stop working backgraund worker on load next layer
            var timeServ = Container.Resolve<TimeService>();
            LoadNewLayerEvent += timeServ.StopWorking;
            //add handlers on PacmanDead event 
            var Pacman = Container.Resolve<IPacman>();
            Pacman.PacmenDead += PacmanDead;
            Pacman.PacmenDead+= timeServ.StopWorking;
            return Container;
        }
        /// <summary>
        /// PacmanDead event handler
        /// Save result to db,update ListRecords if needed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PacmanDead(object sender, EventArgs e)
        {

            var DataLayer = ApplicationService.Container.Resolve<DataLayerService>();
            DataLayer.AddRecord(Player);
            if (ListRecords.Count >= GameConstants.CountRowsInRecords)
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
        /// <summary>
        /// Check end layer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Player_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == GameConstants.PropertyScore)
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
