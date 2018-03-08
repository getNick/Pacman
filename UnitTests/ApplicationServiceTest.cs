using GameCore.Interfaces;
using GameService.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;
using GameDataLayer.Interfaces;

namespace UnitTests
{
    [TestClass]
    public class ApplicationServiceTest
    {
        [TestMethod]
        public void ApplicationServiceCtr()
        {
            var appServ = new ApplicationService();
            Assert.IsTrue(ApplicationService.Container.IsRegistered<IUnitOfWork>());
            Assert.IsTrue(ApplicationService.Container.IsRegistered<IPlayer>());
            Assert.IsTrue(ApplicationService.Container.IsRegistered<DataLayerService>());
        }
    }
}
