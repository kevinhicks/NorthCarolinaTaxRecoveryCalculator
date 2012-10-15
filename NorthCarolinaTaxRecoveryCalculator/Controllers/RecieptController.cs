using System;
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
            var modal = new Reciept();
            modal.Project = db.Projects.Find(ProjectID);
            return View(modal);
        }

        [ChildActionOnly]
        public ActionResult List(int ProjectID)
        {
            var reciepts = db.Reciepts.Where(rec => rec.Project.ID == ProjectID);
            return PartialView("_ListReciepts", reciepts);
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
        public ActionResult Create(int ProjectID, Reciept reciept)
        {
            
            if (ModelState.IsValid)
            {
                //Is it already in the database?
                var queryForOriginal = db.Reciepts.Where(rec =>
                    rec.RIF == reciept.RIF &&
                    rec.Project.ID == reciept.Project.ID);


                //Then remove it
                if (queryForOriginal.Count() > 0)
                {
                    var originalReciept = queryForOriginal.First();

                    db.Reciepts.Remove(originalReciept);
                    db.SaveChanges();
                }

                //replace  it with the new values
                reciept.Project = db.Projects.Find(ProjectID);
                db.Reciepts.Add(reciept);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Index", reciept);
        }
        
        //
        // POST: /Reciept/Edit/

        [HttpPost]
        public ActionResult Edit(Reciept reciept)
        {/*
            if (ModelState.IsValid)
            {
                var original = db.Reciepts.Where(rec => 
                    rec.RIF == reciept.RIF && 
                    rec.Project.ID == reciept.Project.ID).First();
                db.Entry(reciept).State = EntityState.Modified;
                db.Entry(original).CurrentValues.SetValues(reciept);
                //db.SaveChanges();
                return RedirectToAction("Index", new { ProjectID = reciept.Project.ID } );
            }
            */
            return View("Index", reciept);
        }

        //
        // POST: /Reciept/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int RecieptID)
        {
            Reciept reciept = db.Reciepts.Find(RecieptID);
            db.Entry(reciept).Reference("Project").Load();
            int rid = reciept.Project.ID;

            db.Reciepts.Remove(reciept);
            db.SaveChanges();

            return RedirectToAction("Index", new { ProjectId = rid });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}