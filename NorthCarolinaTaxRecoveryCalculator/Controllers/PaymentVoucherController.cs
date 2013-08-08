using Newtonsoft.Json;
using NorthCarolinaTaxRecoveryCalculator.Misc;
using NorthCarolinaTaxRecoveryCalculator.Models;
using NorthCarolinaTaxRecoveryCalculator.Models.Data;
using NorthCarolinaTaxRecoveryCalculator.ViewModels.PaymentVoucher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Omu.ValueInjecter;

namespace NorthCarolinaTaxRecoveryCalculator.Controllers
{
    public class PaymentVoucherController : Controller
    {
        //
        // GET: /PaymentVoucher/
        /// <summary>
        /// When we go to /PaymentVouchers we want to see a list of all our payment vouchers
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        public ActionResult Index(Guid ProjectID)
        {
            var vm = new PaymentVouchersViewModel();
            IProjectRepository projects = new ProjectManager();
            IPaymentVoucherRepository vouchers = new PaymentVoucherManager();

            vm.Vouchers = vouchers.GetAllForProject(ProjectID).ToList();
            vm.Project = projects.Get(ProjectID);

            //Send it to the view
            return View(vm);
        }

        /// <summary>
        /// When we go to /PaymentVouchers/Edit we want to see a payment voucher. which we can edit
        /// </summary>
        /// <param name="VoucherID"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(Guid VoucherID)
        {
            var voucher = new PaymentVoucherManager().Get(VoucherID);

            //If there is no such Payment Voucher,then go back to the list of Vouchers
            if (voucher == null)
            {
                return HttpNotFound();
            }

            return View("Edit", voucher);
        }
        
        /// <summary>
        /// When we are posting to /PaymentVouchers/Edit we want to update the payment voucher in the store
        /// </summary>
        /// <param name="VoucherID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(PaymentVoucher model)
        {
            var v = new PaymentVoucherManager().Get(model.ID);
            var vouchers = new PaymentVoucherManager();

            if (ModelState.IsValid)
            {
                //model.RemoveBlankEntries();
                //v.InjectFrom(model);

                vouchers.Update(model);
                return RedirectToAction("Index", new { ProjectID = model.ProjectID });
            }
            else
            {
                return View("Edit", model);
            }
        }

        /// <summary>
        /// When we go to /Paymentvoucher/Create we want to be able to add a new payment voucher
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create(Guid ProjectID)
        {
            var v = new PaymentVoucher();
            v.Project = new ProjectManager().Get(ProjectID);
            for (int i = 0; i < 30; i++)
            {
                v.Entries.Add(new PaymentVoucherEntry());
            }
            return View("Edit",v);
        }

        /// <summary>
        /// When we are posting to /Paymentvoucher/Create we want to save the new voucher in the store
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Guid ProjectID, PaymentVoucher model)
        {
            if (ModelState.IsValid)
            {
                new PaymentVoucherManager().Create(model);
                return RedirectToAction("Index");
            }
            else
            {
                return View("Edit", model);
            }
        }

        /// <summary>
        /// When we are posting to /Paymentvoucher/Create we want to save the new voucher in the store
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <param name="VoucherID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(Guid VoucherID)
        {            
            var vouchers = new PaymentVoucherManager();
            var voucher = vouchers.Get(VoucherID);
            var projID = voucher.ProjectID;

            vouchers.Delete(voucher);

            return RedirectToAction("Index", new { ProjectID = projID });
        }

        /// <summary>
        /// When we need to add moew rows to the voucher, we can click Add Rows
        /// This will simply add new blank linke to the voucher, and return it.
        /// We dont really even care about validation right now
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddRows(PaymentVoucher v)
        {
            //Remove any empty rows
            v.RemoveBlankEntries();

            //Add 30 more rows
            v.AddBlankRows(30);

            return PartialView("_EntryList", v);
        }
    }
}
