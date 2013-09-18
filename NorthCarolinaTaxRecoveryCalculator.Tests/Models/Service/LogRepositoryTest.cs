using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthCarolinaTaxRecoveryCalculator;
using NorthCarolinaTaxRecoveryCalculator.Controllers;
using NorthCarolinaTaxRecoveryCalculator.Models;
using NorthCarolinaTaxRecoveryCalculator.Models.Service;
using NorthCarolinaTaxRecoveryCalculator.Misc;
using FakeItEasy;
using NorthCarolinaTaxRecoveryCalculator.Models.Data;

namespace NorthCarolinaTaxRecoveryCalculator.Tests.Models
{
    [TestClass]
    public class AzureLogRepositoryTest
    {
        private UserProfile user;
        public AzureLogRepositoryTest()
        {
            user = new UserProfile();
            user.UserId = 1;
            user.UserName = "test user";
        }

        [TestMethod]
        public void AzureLogRepository_Create_ShouldCreateNewLogEntry()
        {
            var log = new Log();
            log.UserID = "1";
            log.UserName = "Kevin";
            log.Message = "A Test Log Message";

            var logRepository = new AzureLogRepository();
            logRepository.Create(log);
        }

        [TestMethod]
        public void AzureLogRepository_FindAll_ShouldReturnAllTheLogEntries()
        {
            var log = new Log();
            log.UserID = "1";
            log.UserName = "Kevin";
            log.Message = "A Test Log Message";

            var logRepository = new AzureLogRepository();
            logRepository.Create(log);

            var logs = logRepository.FindAll();

            //Make sure its there
            foreach (var entry in logs)
            {
                if (entry.ID == log.ID &&
                    entry.UserName == log.UserName &&
                    entry.UserID == log.UserID &&
                    entry.Message == entry.Message)
                {
                    Assert.IsTrue(true);
                    return;
                }
            }

            Assert.Fail();

        }
    }
}
