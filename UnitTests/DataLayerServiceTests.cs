using System;
using GameDataLayer.Interfaces;
using GameService.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests
{
    [TestClass]
    public class DataLayerServiceTests
    {
        [TestMethod]
        public void DataLayer_UnitOfWorkNull_returnsException()
        {
            IUnitOfWork unitOfWork = null;
            try
            {
                var dataLayer = new DataLayerService(unitOfWork);
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("UnitOfWork", ex.ParamName);
            }
            
        }
    }
}
