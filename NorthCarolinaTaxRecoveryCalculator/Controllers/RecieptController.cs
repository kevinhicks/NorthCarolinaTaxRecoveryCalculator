using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using NorthCarolinaTaxRecoveryCalculator.Models;

namespace NorthCarolinaTaxRecoveryCalculator.Controllers
{
    public class RecieptController : Controller
    {
        //
        // GET: /Reciept/

        public ActionResult Index()
        {
            List<RecieptModal> modal = new List<RecieptModal>();
            modal.Add(new RecieptModal() { RIF = "1", DateOfSale = "12/12/12" });
            modal.Add(new RecieptModal() { RIF = "2" });
            return View(modal);
        }

        //
        // GET: /Reciept/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Reciept/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Reciept/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Reciept/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Reciept/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Reciept/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Reciept/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
