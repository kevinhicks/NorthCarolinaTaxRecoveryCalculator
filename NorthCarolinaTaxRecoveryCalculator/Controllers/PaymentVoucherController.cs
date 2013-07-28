using NorthCarolinaTaxRecoveryCalculator.Models.Data;
using NorthCarolinaTaxRecoveryCalculator.ViewModels.PaymentVoucher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorthCarolinaTaxRecoveryCalculator.Controllers
{
    public class PaymentVoucherController : Controller
    {
        //
        // GET: /PaymentVoucher/

        static List<PaymentVoucher> vouchers = null;
        static PaymentVoucherController()
        {
            vouchers = new List<PaymentVoucher>();

            var paymentVoucher = new PaymentVoucher();
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
            
        }

        public ActionResult Index()
        {
            //Send it to the view
            return View(vouchers);
        }

        public ActionResult AddPaymentVoucherEntry(PaymentVoucherViewModel model)
        {
            model.Voucher.Entries.Add(new PaymentVoucherEntry());
            return Json(model);
        }

        [HttpGet]
        public ActionResult GetVoucher(Guid? VoucherID)
        {
            PaymentVoucher voucher;

            //If we are not asking to a specific voucher, then we must want a new one
            if (VoucherID == null)
            {
                voucher = new PaymentVoucher();
            }
            else
            {
                voucher = vouchers.Where(m => m.ID == VoucherID).FirstOrDefault();
                ViewBag.SaveButtonLabel = "Update";
                return PartialView("_EditVoucher", voucher);
            }
            
            ViewBag.SaveButtonLabel = "Create";
            return PartialView("_EditVoucher", voucher);
        }

        [HttpPost]
        public ActionResult SaveVoucher(PaymentVoucher Voucher)
        {
            if (ModelState.IsValid)
            {
                //Does it already exist?
                var v = vouchers.Where(m => m.ID == Voucher.ID).FirstOrDefault();
                if (v != null)
                {
                    //If it does exist, update it
                    v.ProjectName = Voucher.ProjectName;
                    v.CheckNumber = Voucher.CheckNumber;
                    v.Date = Voucher.Date;
                    v.PaidTo = Voucher.PaidTo;
                    v.PreparedBy = Voucher.PreparedBy;
                    v.ApprovedBy = Voucher.ApprovedBy;
                    v.RBCApproval = Voucher.RBCApproval;
                    v.Entries = Voucher.Entries;
                }
                else
                {
                    //Create it
                    vouchers.Add(Voucher);
                }

                return Content("Saved");
            }

            ViewBag.SaveButtonLabel = "Update";
            return PartialView("_EditVoucher", Voucher);
        }
    }
}
