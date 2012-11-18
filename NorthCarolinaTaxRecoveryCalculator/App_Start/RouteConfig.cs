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

            routes.MapRoute(
                name: "TO: Project",
                url: "Project/",
                defaults: new { controller = "Project", action = "Index" }
            );

            routes.MapRoute(
                name: "TO: Details of Project",
                url: "Project/Details/{ProjectID}",
                defaults: new { controller = "Project", action = "Details", ProjectID = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "TO: Edit a Project",
                url: "Project/Edit/{ProjectID}",
                defaults: new { controller = "Project", action = "Edit", ProjectID = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "TO: Accept an invitaion to a project",
                url: "Project/AcceptInvite/{ProjectID}",
                defaults: new { controller = "Project", action = "AcceptInvite", ProjectID = UrlParameter.Optional }
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