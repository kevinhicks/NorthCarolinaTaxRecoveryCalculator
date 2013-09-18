using NorthCarolinaTaxRecoveryCalculator.Models.Data;
using NorthCarolinaTaxRecoveryCalculator.Models.Service;
using RestSharp;
using SendGridMail;
using SendGridMail.Transport;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;

namespace NorthCarolinaTaxRecoveryCalculator.Misc
{
    public enum LogMessageType
    {
        Error,
        Warning,
        Debug,
        Information
    }

    public interface ILogger
    {
        /// <summary>
        /// Send a Log
        /// </summary>        
        /// <param name="userID">The ID of the User that triggerd the event that is being logged</param>
        /// <param name="userName">The Name of the User that triggerd the event that is being logged</param>
        /// <param name="messageType">The type of message we are trying to log</param>
        /// <param name="message"></param>
        /// <returns>True if log was saved succefulyy, false if not</returns>
        bool Log(string userID, string userName, string messageString, LogMessageType messageType);
    }

    /// <summary>
    /// Use this to send logs.
    /// 
    /// If any attemp to log something fails, it will cascade to the next logger available
    /// </summary>
    public class Logger : ILogger
    {
        private List<ILogger> loggers;

        private Logger()
        {
            loggers = new List<ILogger>();
        }

        private static Logger _instance = null;
        /// <summary>
        /// this is a singleton, we use this method to retun our instance
        /// </summary>
        /// <returns></returns>
        public static Logger GetLogger()
        {
            if (_instance == null)
            {
                _instance = new Logger();
            }

            return _instance;
        }

        /// <summary>
        /// Add a logger to the end of the cascade
        /// </summary>
        /// <param name="logger"></param>
        public void AddLogger(ILogger logger)
        {
            if (logger != null)
            {
                loggers.Add(logger);
            }
        }

        /// <summary>
        /// Removes all Loggers from the cascade. Useful if in a test environment
        /// </summary>
        public void ClearAllLoggers()
        {
            loggers.Clear();
        }

        public bool Log(string userID, string userName, string messagestring, LogMessageType messageType)
        {
            //Try sending the log using each available logger until we find one that works
            //NOTE: Homepfully the first one always works. but we have more available in case not
            foreach (var logger in loggers)
            {
                try
                {
                    if (logger.Log(userID, userName, messagestring, messageType))
                    {
                        //If the log was successful, then break out of the loop; we dont need to keep trying
                        return true;
                    }
                }
                catch (Exception e)
                {
                    //Somethign went wrong while logging. allow us to keep running, just move on to the next available logger
                }
            }

            //If we get here, then we did not find any logger available that would save our log. Return true
            return false;
        }
    }

    /// <summary>
    /// Send a long entry via email
    /// </summary>
    public class EmailLogger : ILogger
    {
        /// <summary>
        /// Email Sender is how we wew ill actually send the emails
        /// </summary>
        private IEmailSender emailSender;

        /// <summary>
        /// Create a new Email Logger, sending emails via the specifiyed IEmailSender
        /// </summary>
        /// <param name="emailSender"></param>
        public EmailLogger(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        public bool Log(string userID, string userName, string messageString, LogMessageType messageType)
        {
            string msgType = messageType.ToString();
            string body = "User ID: " + userID + "\n";
            body += "User Name: " + userName + "\n";
            body += "Message Type: " + msgType + "\n";
            body += "Message: " + messageString + "\n";

            emailSender.SendMail("keyholeserviceserrorlogs@gmail.com",
                                 "Log (" + msgType + ") North Carolina Tax Recovery Calculator",
                                 body);

            return true;
        }
    }
    
    /// <summary>
    /// Send a long entry via Azure storage tables
    /// </summary>
    public class AzureLogger : ILogger
    {
        private ILogRepository logRepository;
        public AzureLogger(ILogRepository logRepository)
        {
            this.logRepository = logRepository;
        }

        public bool Log(string userID, string userName, string messageString, LogMessageType messageType)
        {
            var log = new Log();
            log.UserID = userID;
            log.UserName = userName;            
            log.Message = messageString;

            logRepository.Create(log);

            return true;
        }
    }
}