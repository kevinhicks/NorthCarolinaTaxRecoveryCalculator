using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthCarolinaTaxRecoveryCalculator;
using NorthCarolinaTaxRecoveryCalculator.Controllers;
using NorthCarolinaTaxRecoveryCalculator.Models;
using Microsoft.Practices.Unity;
using NorthCarolinaTaxRecoveryCalculator.Misc;
using FakeItEasy;

namespace NorthCarolinaTaxRecoveryCalculator.Tests.Models
{
    [TestClass]
    public class ACLManagerTest
    {
        IEmailSender emailSender = null;
        public ACLManagerTest()
        {
            emailSender = A.Fake<IEmailSender>();
        }

        [TestMethod]
        public void ACLManager_SendInvitation_ShouldSendAnEmailViaTheIEmailSender()
        {
            var manager = new ACLManager();
            manager.SendInvitation("test" + new Random().NextDouble(), Guid.NewGuid(), UserType.DataEntry, emailSender);

            A.CallTo(() => emailSender.SendMail("", "", "")).WithAnyArguments().MustHaveHappened();
        }

        [TestMethod]
        public void ACLManager_SendInvitation_ShouldNotSentDuplicateEmails()
        {
            var manager = new ACLManager();
            //Use a guid to kep the test unique
            Guid r = Guid.NewGuid();

            //First time
            manager.SendInvitation("test1" + r.ToString(), r, UserType.DataEntry, emailSender);
            A.CallTo(() => emailSender.SendMail("", "", "")).WithAnyArguments().MustHaveHappened();

            //second time should fail
            manager.SendInvitation("test1" + r.ToString(), r, UserType.DataEntry, emailSender);
            A.CallTo(() => emailSender.SendMail("", "", "")).WithAnyArguments().MustHaveHappened(Repeated.Exactly.Once);
        }        
    }
}
