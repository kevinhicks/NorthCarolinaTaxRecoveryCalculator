﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthCarolinaTaxRecoveryCalculator.Models;

namespace NorthCarolinaTaxRecoveryCalculator.Controllers
{
    public class RecieptController : Controller
    {
        private ApplicationDBContext db = new ApplicationDBContext();

        //
        // GET: /Reciept/{ProjectID}

        public ActionResult Index(int ProjectID)
        {
            var modal = new ListAndCreateReciept();
            modal.Reciepts = db.Reciepts.ToList();
            modal.Reciept = new Reciept();

            modal.Reciept.Project = db.Projects.Find(ProjectID);

            return View(modal);
        }

        //
        // GET: /Reciept/Details/5

        public ActionResult Details(int id = 0)
        {
            Reciept reciept = db.Reciepts.Find(id);
            if (reciept == null)
            {
                return HttpNotFound();
            }
            return View(reciept);
        }

        //
        // POST: /Reciept/Create

        [HttpPost]
        public ActionResult Create(int ProjectID, ListAndCreateReciept modal)
        {
            modal.Reciept.Project = db.Projects.Find(ProjectID);
            if (ModelState.IsValid)
            {
                db.Reciepts.Add(modal.Reciept);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            modal.Reciepts = db.Reciepts.ToList();
            return View("Index", modal);
        }

        //
        // GET: /Reciept/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Reciept reciept = db.Reciepts.Find(id);
            if (reciept == null)
            {
                return HttpNotFound();
            }
            return View(reciept);
        }

        //
        // POST: /Reciept/Edit/5

        [HttpPost]
        public ActionResult Edit(Reciept reciept)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reciept).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reciept);
        }

        //
        // GET: /Reciept/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Reciept reciept = db.Reciepts.Find(id);
            if (reciept == null)
            {
                return HttpNotFound();
            }
            return View(reciept);
        }

        //
        // POST: /Reciept/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Reciept reciept = db.Reciepts.Find(id);
            db.Reciepts.Remove(reciept);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}