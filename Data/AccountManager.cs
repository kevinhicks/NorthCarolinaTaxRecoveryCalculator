using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Principal;
using WebMatrix.WebData;

namespace Data
{
    /// <summary>
    /// Describes the the attributes & actions that any Account Manager MUST have
    /// </summary>
    public interface IAccountManager
    {
        bool Login(string userName, string password, bool persistCookie = false);
        void Logout();
        string CreateUserAndAccount(string userName, string password, object propertyValues = null,
               bool requireConfirmationToken = false);
        int GetUserId(string userName);
        bool ChangePassword(string userName, string currentPassword, string newPassword);
        string CreateAccount(string userName, string password, bool requireConfirmationToken = false);

        IPrincipal CurrentUser { get; }
    }

    /// <summary>
    /// This will habeld any and all logic for retrieveing Project data from the Data-Access-Layer
    /// </summary>
    public class AccountManager : IAccountManager
    {
        public string CreateAccount(string userName, string password, bool requireConfirmationToken = false)
        {
            return WebSecurity.CreateAccount(userName, password);
        }

        public string CreateUserAndAccount(string userName, string password, object propertyValues = null, bool requireConfirmationToken = false)
        {
            return WebSecurity.CreateUserAndAccount(userName, password, propertyValues, requireConfirmationToken);
        }

        public bool Login(string UserName, string Password, bool persistCookie = false)
        {
            return WebSecurity.Login(UserName, Password, persistCookie);
        }

        public void Logout()
        {
            WebSecurity.Logout();
        }

        public int GetUserId(string userName)
        {
            return WebSecurity.GetUserId(userName);
        }

        public bool ChangePassword(string userName, string currentPassword, string newPassword)
        {
            return WebSecurity.ChangePassword(userName, currentPassword, newPassword);
        }

        public IPrincipal CurrentUser
        {
            get { throw new NotImplementedException(); }
        }
    }
}