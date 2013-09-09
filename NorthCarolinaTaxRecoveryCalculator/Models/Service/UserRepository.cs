using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace NorthCarolinaTaxRecoveryCalculator.Security
{
    /// <summary>
    /// Describes what any User Repository must have
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Get the ID of the currnt user
        /// </summary>
        int CurrentUserId { get; }
    }

    /// <summary>
    /// Standard user repository. This will be used to access the user information from the web-server
    /// </summary>
    public class UserRepository : IUserRepository
    {
        public int CurrentUserId
        {
            get
            {
                return WebSecurity.CurrentUserId;
            }
        }
    }
}