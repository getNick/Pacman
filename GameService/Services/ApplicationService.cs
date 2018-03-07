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
using GameDataLayer.Interfaces;
using GameDataLayer.Repositories;
using System.Configuration;
using GameCore.EnumsAndConstant;

namespace GameService.Services
{
    public class ApplicationService
    {
        public static IContainer Container { get; private set; }
        public ApplicationService()
        {

            var builder = new ContainerBuilder();
            builder.RegisterType<EFUnitOfWork>().As<IUnitOfWork>()
                .WithParameter(new NamedParameter(GameConstants.NamedParameterConnectionString,(GameConstants.ConnectionString))).SingleInstance();

            builder.RegisterType<DataLayerService>().AsSelf().SingleInstance();

            builder.RegisterType<Player>().As<IPlayer>().SingleInstance();

            Container = builder.Build();
        }       
    }
}
