using NorthCarolinaTaxRecoveryCalculator.Models.Data;
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

        public ActionResult Index()
        {
            var paymentVoucher = new PaymentVoucher();
            var entry = new PaymentVoucherEntry();
            entry.Item = "Bolt";
            entry.CostElement = "asd";
            entry.Amount = 100;

            paymentVoucher.Entries.Add(entry);
            return View(paymentVoucher);
        }

    }
}
