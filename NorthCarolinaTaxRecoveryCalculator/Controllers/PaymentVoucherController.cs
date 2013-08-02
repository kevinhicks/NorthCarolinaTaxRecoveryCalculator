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

        static Project project = null;
        static List<PaymentVoucher> vouchers = null;
        static PaymentVoucherController()
        {
            project = new Project();
            project.ID = Guid.NewGuid();
            project.Name = "Test UI Proejct";
            
            vouchers = new List<PaymentVoucher>();
            var paymentVoucher = new PaymentVoucher();
            paymentVoucher.Project = project;
            paymentVoucher.CheckNumber = "123";
            paymentVoucher.PaidTo = "Kevin";
            vouchers.Add(paymentVoucher);

            var entry = new PaymentVoucherEntry();
            entry.Item = "Bolts";
            entry.CostElement = "102-2";
            entry.Amount = 100.22;
            paymentVoucher.Entries.Add(entry);
            entry = new PaymentVoucherEntry();
            entry.Item = "Screws";
            entry.CostElement = "122-2";
            entry.Amount = 10.22;
            paymentVoucher.Entries.Add(entry);
            entry = new PaymentVoucherEntry();
            entry.Item = "Nuts";
            entry.CostElement = "102.2";
            entry.Amount = 143.22;
            paymentVoucher.Entries.Add(entry);

            paymentVoucher = new PaymentVoucher();
            paymentVoucher.Project = project;
            paymentVoucher.CheckNumber = "456";
            paymentVoucher.PaidTo = "Joe";
            vouchers.Add(paymentVoucher);

            entry = new PaymentVoucherEntry();
            entry.Item = "Bolts";
            entry.CostElement = "102-2";
            entry.Amount = 100.22;
            paymentVoucher.Entries.Add(entry);
            entry = new PaymentVoucherEntry();
            entry.Item = "Screws";
            entry.CostElement = "122-2";
            entry.Amount = 10.22;
            paymentVoucher.Entries.Add(entry);
            entry = new PaymentVoucherEntry();
            entry.Item = "Nuts";
            entry.CostElement = "102.2";
            entry.Amount = 143.22;
            paymentVoucher.Entries.Add(entry);

            paymentVoucher = new PaymentVoucher();
            paymentVoucher.Project = project;
            paymentVoucher.CheckNumber = "789";
            paymentVoucher.PaidTo = "Sue";
            vouchers.Add(paymentVoucher);

            entry = new PaymentVoucherEntry();
            entry.Item = "Bolts";
            entry.CostElement = "102-2";
            entry.Amount = 100.22;
            paymentVoucher.Entries.Add(entry);
            entry = new PaymentVoucherEntry();
            entry.Item = "Screws";
            entry.CostElement = "122-2";
            entry.Amount = 10.22;
            paymentVoucher.Entries.Add(entry);
            entry = new PaymentVoucherEntry();
            entry.Item = "Nuts";
            entry.CostElement = "102.2";
            entry.Amount = 143.22;
            paymentVoucher.Entries.Add(entry);
                        
        }

        /// <summary>
        /// When we go to /PaymentVouchers we want to see a list of all our payment vouchers
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        public ActionResult Index(Guid? ProjectID)
        {
            var vm = new PaymentVouchersViewModel();
            vm.Vouchers = vouchers;
            vm.Project = project;

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
            var voucher = vouchers.Where(col => col.ID == VoucherID).FirstOrDefault();

            //If there is no such Payment Voucher,then go back to the list of Vouchers
            if (voucher == null)
            {
                return HttpNotFound();
            }

            return View(voucher);
        }
        
        /// <summary>
        /// When we are posting to /PaymentVouchers/Edit we want to update the payment voucher in the store
        /// </summary>
        /// <param name="VoucherID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(PaymentVoucher model)
        {
            var v = vouchers.Where(col => col.ID == model.ID).FirstOrDefault();
                
            if (ModelState.IsValid)
            {
                v.InjectFrom(model);
                return RedirectToAction("Index", model.Project.ID);
            }
            else
            {
                return View(model);
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
            v.Project = project;
            return View(v);
        }

        /// <summary>
        /// When we are posting to /Paymentvoucher/Create we want to save the new voucher in the store
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(PaymentVoucher model)
        {
            if (ModelState.IsValid)
            {
                vouchers.Add(model);
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }
    }
}
