﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

            //Init the datbase, and apply any pending updates/changes 
            //NOTE: Entity Framework MUST update the database BEFORE the following WebSecurity block.
            //  If Entity Framework does not find that database thet why it left it, then it gets testy
            try
            {
                var updateDBInit = new MigrateDatabaseToLatestVersion<ApplicationDBContext, Configuration>();
                updateDBInit.InitializeDatabase(db);
            }
            catch (Exception e)
            {
            }
            //Init Security
            //NOTE: Entity Framework MUST update the database BEFORE this WebSecurity block.
            //  If Entity Framework does not find that database thet why it left it, then it gets testy

            try
            {
                if (!WebSecurity.Initialized)
                {
                    WebSecurity.InitializeDatabaseConnection("ApplicationDBContext", "UserProfile", "UserId", "UserName",
                        autoCreateTables: true);
                }
            }
            catch (Exception e)
            {
            }

            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            //Init IoC(unity)
            Bootstrapper.Initialise();

            //Remove all but the Razor View Engine for some extra Perf
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            //Load Tax Periods into globaly available static structure
            TaxPeriods.Load();
        }

        void Seed()
        {

        }
    }
}