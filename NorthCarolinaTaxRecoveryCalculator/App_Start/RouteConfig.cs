using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NorthCarolinaTaxRecoveryCalculator
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Reciepts Must Have A Project ID When Creating A New Reciept",
                url: "Reciept/Create/{ProjectID}",
                defaults: new { controller = "Reciept", action = "Create", ProjectID = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Reciepts Must Have A Project ID When Listing All Reciepts",
                url: "Reciept/List/{ProjectID}",
                defaults: new { controller = "Reciept", action = "List", ProjectID = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Reciepts Must Have A Project ID",
                url: "Reciept/{ProjectID}",
                defaults: new { controller = "Reciept", action = "Index", ProjectID = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Reciepts Must Have A Project ID when deleting",
                url: "Reciept/Delete/{RecieptID}",
                defaults: new { controller = "Reciept", action = "Delete", RecieptID = UrlParameter.Optional }
            );

            //When all else fails, route to the HomePage
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}