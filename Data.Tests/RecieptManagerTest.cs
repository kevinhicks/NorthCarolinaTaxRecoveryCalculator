using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeItEasy;

namespace Data.Tests
{
    [TestClass]
    public class RecieptManagerTest
    {
        RecieptManager recieptManager = null;

        [TestInitialize]
        public void TestStartUp()
        {
            recieptManager = new RecieptManager();
        }

        [TestMethod]
        public void FindAllRecieptsByProjectID_ReturnsNotNull()
        {
            Assert.IsNotNull(recieptManager.FindAllRecieptsByProjectID(Guid.NewGuid()));
        }
    }
}
