using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthCarolinaTaxRecoveryCalculator;
using NorthCarolinaTaxRecoveryCalculator.Controllers;
using NorthCarolinaTaxRecoveryCalculator.Models;
using NorthCarolinaTaxRecoveryCalculator.Models.Data;
using NorthCarolinaTaxRecoveryCalculator.Misc;
using FakeItEasy;
using NorthCarolinaTaxRecoveryCalculator.Models.Service;

namespace NorthCarolinaTaxRecoveryCalculator.Tests.LoggingTest
{
    [TestClass]
    public class AzureLoggerTest
    {
        private ILogRepository mockLogRepository;

        public AzureLoggerTest()
        {
            //Fake up a email sender to work with
            mockLogRepository = A.Fake<ILogRepository>();                        
        }

        [TestMethod]
        public void AzureLogger_Log_ShouldSendALogToAzureStorage()
        {
            var logger = new AzureLogger(mockLogRepository);

            A.CallTo(() => mockLogRepository.Create(null)).WithAnyArguments().MustNotHaveHappened();
            logger.Log("123", "Kevin", "Message Test", LogMessageType.Debug);
            A.CallTo(() => mockLogRepository.Create(null)).WithAnyArguments().MustHaveHappened();

        }
    }
}
