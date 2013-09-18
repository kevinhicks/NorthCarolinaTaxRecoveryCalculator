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
    public class LoggerTest
    {
        private ILogger firstExampleLogger;
        private ILogger secondExampleLogger;
        private ILogger erroringLogger;
        private Logger logger;

        public LoggerTest()
        {
            //Give us some loggers to work with. This will give us 2 valid loggers, and 1 logger that will throw an exception
            firstExampleLogger = A.Fake<ILogger>();
            secondExampleLogger = A.Fake<ILogger>();
            erroringLogger = A.Fake<ILogger>();


            A.CallTo(() => firstExampleLogger.Log("", "", "", LogMessageType.Debug)).WithAnyArguments().Returns(true);
            A.CallTo(() => secondExampleLogger.Log("", "", "", LogMessageType.Debug)).WithAnyArguments().Returns(true);
            A.CallTo(() => erroringLogger.Log("", "", "", LogMessageType.Debug)).WithAnyArguments().Returns(false);

            logger = Logger.GetLogger();
            logger.ClearAllLoggers();
        }

        [TestMethod]
        public void Logger_Log_ShouldSendALogAndReturnTrue()
        {
            logger.AddLogger(firstExampleLogger);
            A.CallTo(() => firstExampleLogger.Log("", "", "", LogMessageType.Debug)).WithAnyArguments().MustNotHaveHappened();
            Assert.IsTrue(logger.Log("123", "123", "message", LogMessageType.Information));
            A.CallTo(() => firstExampleLogger.Log("", "", "", LogMessageType.Debug)).WithAnyArguments().MustHaveHappened();
        }
        
        [TestMethod]
        public void Logger_Log_ShouldReturnFalseIfNoLoggerWasAbleToSave()
        {
            logger.AddLogger(erroringLogger);
            A.CallTo(() => erroringLogger.Log("", "", "", LogMessageType.Debug)).WithAnyArguments().MustNotHaveHappened();
            Assert.IsFalse(logger.Log("123", "123", "message", LogMessageType.Information));
            A.CallTo(() => erroringLogger.Log("", "", "", LogMessageType.Debug)).WithAnyArguments().MustHaveHappened();
        }

        [TestMethod]
        public void Logger_Log_ShouldReturnFalseIfNoLoggersAreAvailable()
        {
            Assert.IsFalse(logger.Log("123", "123", "message", LogMessageType.Information));         
        }

        [TestMethod]
        public void Logger_Log_ShouldCascadeToTheNextAvailableLoggerIfOneFails()
        {
            logger.AddLogger(erroringLogger);
            logger.AddLogger(firstExampleLogger);

            A.CallTo(() => erroringLogger.Log("", "", "", LogMessageType.Debug)).WithAnyArguments().MustNotHaveHappened();
            A.CallTo(() => firstExampleLogger.Log("", "", "", LogMessageType.Debug)).WithAnyArguments().MustNotHaveHappened();

            Assert.IsTrue(logger.Log("123", "123", "message", LogMessageType.Information));

            A.CallTo(() => erroringLogger.Log("", "", "", LogMessageType.Debug)).WithAnyArguments().MustHaveHappened();
            A.CallTo(() => firstExampleLogger.Log("", "", "", LogMessageType.Debug)).WithAnyArguments().MustHaveHappened();
        }

        [TestMethod]
        public void Logger_Log_ShouldNotCascadeToTheNextAvailableLoggerIfNothingFails()
        {
            logger.AddLogger(firstExampleLogger);
            logger.AddLogger(erroringLogger);

            A.CallTo(() => erroringLogger.Log("", "", "", LogMessageType.Debug)).WithAnyArguments().MustNotHaveHappened();
            A.CallTo(() => firstExampleLogger.Log("", "", "", LogMessageType.Debug)).WithAnyArguments().MustNotHaveHappened();

            Assert.IsTrue(logger.Log("123", "123", "message", LogMessageType.Information));

            A.CallTo(() => erroringLogger.Log("", "", "", LogMessageType.Debug)).WithAnyArguments().MustNotHaveHappened();
            A.CallTo(() => firstExampleLogger.Log("", "", "", LogMessageType.Debug)).WithAnyArguments().MustHaveHappened();
        }
    }
}
