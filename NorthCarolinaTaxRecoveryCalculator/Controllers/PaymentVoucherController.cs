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
using NorthCarolinaTaxRecoveryCalculator.Models.Service;

namespace NorthCarolinaTaxRecoveryCalculator.Controllers
{
    public class PaymentVoucherController : Controller
    {

        private IProjectRepository ProjectRepository;
        private IPaymentVoucherRepository PaymentVoucherRepository;
        public PaymentVoucherController(IProjectRepository ProjectRepository, IPaymentVoucherRepository PaymentVoucherRepository)
        {
            this.ProjectRepository = ProjectRepository;
            this.PaymentVoucherRepository = PaymentVoucherRepository;
        }

        //
        // GET: /PaymentVoucher/
        /// <summary>
        /// When we go to /PaymentVouchers we want to see a list of all our payment vouchers
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult Index(Guid ProjectID)
        {
            var vm = new PaymentVouchersViewModel();

            vm.Project = ProjectRepository.FindProjectByID(ProjectID);
            vm.Vouchers = PaymentVoucherRepository.GetAllForProject(ProjectID).ToList();            

            //Send it to the view
            return View(vm);
        }

        /// <summary>
        /// When we go to /PaymentVouchers/Edit we want to see a payment voucher. which we can edit
        /// </summary>
        /// <param name="VoucherID"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public ActionResult Edit(Guid VoucherID)
        {
            var voucher = new PaymentVoucherRepository().Get(VoucherID);

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
        [Authorize]
        [HttpPost]
        public ActionResult Edit(PaymentVoucher model)
        {
            var vouchers = new PaymentVoucherRepository();

            if (ModelState.IsValid)
            {
                vouchers.Update(model);
                return RedirectToAction("Index", new { ProjectID = model.ProjectID });
            }
            else
            {
                var v = vouchers.Get(model.ID);            
                model.Project = v.Project;
                return View("Edit", model);
            }
        }

        /// <summary>
        /// When we go to /Paymentvoucher/Create we want to be able to add a new payment voucher
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        [Authorize]        
        [HttpGet]
        public ActionResult Create(Guid ProjectID)
        {
            var v = new PaymentVoucher();
            v.Project = ProjectRepository.FindProjectByID(ProjectID);
            
            return View("Edit",v);
        }

        /// <summary>
        /// When we are posting to /Paymentvoucher/Create we want to save the new voucher in the store
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult Create(Guid ProjectID, PaymentVoucher model)
        {
            if (ModelState.IsValid)
            {
                PaymentVoucherRepository.Create(model);
                return RedirectToAction("Index");
            }
            else
            {
                model.Project = ProjectRepository.FindProjectByID(ProjectID);
                return View("Edit", model);
            }
        }

        /// <summary>
        /// When we are posting to /Paymentvoucher/Create we want to save the new voucher in the store
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <param name="VoucherID"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult Delete(Guid VoucherID)
        {            
            var vouchers = new PaymentVoucherRepository();
            var voucher = vouchers.Get(VoucherID);
            var projID = voucher.ProjectID;

            vouchers.Delete(voucher);

            return RedirectToAction("Index", new { ProjectID = projID });
        }
        
        /// <summary>
        /// We want to view the payment voucher as a pdf for printing
        /// </summary>
        /// <param name="VoucherID"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult Print(Guid VoucherID)
        {
            var voucher = new PaymentVoucherRepository().Get(VoucherID);
            
            return File(voucher.Print(), "application/pdf");
        }
    }
}
