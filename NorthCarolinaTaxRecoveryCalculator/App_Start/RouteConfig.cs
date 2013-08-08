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
                name: "Reciepts can Add, or Update A Reciept",
                url: "Reciept/AddUpdate/{ProjectID}",
                defaults: new { controller = "Reciept", action = "AddUpdate"}
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
                name: "TO: Print Reciepts of a Project As A PDF",
                url: "PrintRecieptsPDF/{ProjectID}",
                defaults: new { controller = "Project", action = "PrintRecieptsPDF", ProjectID = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "TO: Print Reciepts of a Project As An excel File",
                url: "ExportToExcel/{ProjectID}",
                defaults: new { controller = "Project", action = "ExportToExcel", ProjectID = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "TO: Print Reciepts of a Project",
                url: "PrintReciepts/{ProjectID}",
                defaults: new { controller = "Project", action = "PrintReciepts", ProjectID = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "TO: Edit a Project",
                url: "Project/Edit/{ProjectID}",
                defaults: new { controller = "Project", action = "Edit", ProjectID = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "TO: Send an invitaion to a project",
                url: "Project/SendInvitation/{ProjectID}",
                defaults: new { controller = "Project", action = "SendInvitation", ProjectID = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "TO: Resend a previous invitaion to a project",
                url: "Project/ResendInvitation/{AclID}",
                defaults: new { controller = "Project", action = "ResendInvitation", AclID = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "TO: Revoke a previous invitaion to a project",
                url: "Project/RevokeInvitation/{AclID}",
                defaults: new { controller = "Project", action = "RevokeInvitation", AclID = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "TO: Accept an invitaion to a project",
                url: "Project/AcceptInvite/{AclID}",
                defaults: new { controller = "Project", action = "AcceptInvite", AclID = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "TO: View/Edit a Payment Voucher",
                url: "PaymentVoucher/Edit/{VoucherID}",
                defaults: new { controller = "PaymentVoucher", action = "Edit", VoucherID = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "TO: Submit changes to Payment Voucher",
                url: "PaymentVoucher/EditPost/{ProjectID}",
                defaults: new { controller = "PaymentVoucher", action = "EditPost", ProjectID = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "TO: get the Create New Payment Voucher view",
                url: "PaymentVoucher/Create/{ProjectID}",
                defaults: new { controller = "PaymentVoucher", action = "Create", ProjectID = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "TO: Submit a new to Payment Voucher",
                url: "PaymentVoucher/Create/{ProjectID}",
                defaults: new { controller = "PaymentVoucher", action = "Create", ProjectID = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "TO: Print A Payment Voucher",
                url: "PaymentVoucher/Print/{VoucherID}",
                defaults: new { controller = "PaymentVoucher", action = "Print", VoucherID = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "TO: Delete A Payment Voucher",
                url: "PaymentVoucher/Delete/{VoucherID}",
                defaults: new { controller = "PaymentVoucher", action = "Delete", VoucherID = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "TO: Add Rows To A Payment Voucher",
                url: "PaymentVoucher/AddRows",
                defaults: new { controller = "PaymentVoucher", action = "AddRows" }
            );

            routes.MapRoute(
                name: "TO: Get All Payment Vouchers for a project",
                url: "PaymentVoucher/{ProjectID}",
                defaults: new { controller = "PaymentVoucher", action = "Index", ProjectID = UrlParameter.Optional }
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