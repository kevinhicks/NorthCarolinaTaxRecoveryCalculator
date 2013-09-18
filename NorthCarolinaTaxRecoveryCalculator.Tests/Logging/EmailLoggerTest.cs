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

namespace NorthCarolinaTaxRecoveryCalculator.Tests.LoggingTest
{
    [TestClass]
    public class EmailLoggerTest
    {
        private IEmailSender emailSender;   

        public EmailLoggerTest()
        {
            //Fake up a email sender to work with
            emailSender = A.Fake<IEmailSender>();            
        }

        [TestMethod]
        public void EmailLogger_Log_ShouldSendAnEmail()
        {
            var emailLogger = new EmailLogger(emailSender);

            A.CallTo(() => emailSender.SendMail("","","")).WithAnyArguments().MustNotHaveHappened();
            emailLogger.Log("123", "123", "message", LogMessageType.Information);
            A.CallTo(() => emailSender.SendMail("", "", "")).WithAnyArguments().MustHaveHappened();
        }
    }
}
