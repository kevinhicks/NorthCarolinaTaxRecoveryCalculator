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
    public class AzureLoggerTest
    {
        private IEmailSender emailSender;

        public AzureLoggerTest()
        {
            //Fake up a email sender to work with
            emailSender = A.Fake<IEmailSender>();            
        }

        
    }
}
