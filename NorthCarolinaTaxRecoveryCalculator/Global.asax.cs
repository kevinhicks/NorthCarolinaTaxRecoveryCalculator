using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using NorthCarolinaTaxRecoveryCalculator.Models;
using NorthCarolinaTaxRecoveryCalculator.Migrations;
using WebMatrix.WebData;

namespace NorthCarolinaTaxRecoveryCalculator
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ApplicationDBContext db = new ApplicationDBContext();

            //Init Security
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("ApplicationDBContext", "UserProfile", "UserId", "UserName", autoCreateTables: true);

            }

            //Init the datbase
            var updateDBInit = new MigrateDatabaseToLatestVersion<ApplicationDBContext, Configuration>();

            updateDBInit.InitializeDatabase(db);

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            //Remove all but the Razor View Engine for some extra Perf
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            Microsoft.AspNet.SignalR.GlobalHost.Configuration.KeepAlive = TimeSpan.FromSeconds(15);
        }

        void Seed()
        {

        }
    }
}