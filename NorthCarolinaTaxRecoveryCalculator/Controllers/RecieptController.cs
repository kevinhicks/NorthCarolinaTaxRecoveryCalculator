using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthCarolinaTaxRecoveryCalculator.Models;
using System.Web.Script.Serialization;
using System.Data.Objects;
using System.Web.Security;

namespace NorthCarolinaTaxRecoveryCalculator.Controllers
{
    public class RecieptController : Controller
    {
        private ApplicationDBContext db = new ApplicationDBContext();

        //
        // GET: /Reciept/{ProjectID}
        [Authorize]
        public ActionResult Index(Guid ProjectID)
        {
            var modal = new RecieptEntity();
            
            modal.Project = db.Projects.Find(ProjectID);

            //We have navigated to a Project that dosnt exist anymore
            if (modal.Project == null)
            {
                return View("Error");
            }
            
            //All the counties
            ViewBag.Counties = County.AsJsonArray();

            //All the stores in this project
            var stores = db.Reciepts.Where(rec => rec.ProjectID == ProjectID).Select(rec => rec.StoreName).Distinct().ToList();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            ViewBag.Stores = jss.Serialize(stores);

            return View(modal);
        }

        [Authorize]
        [ChildActionOnly]
        public ActionResult List(Guid ProjectID)
        {
            var reciepts = db.Reciepts.Where(rec => rec.Project.ID == ProjectID);
            return PartialView("_ListReciepts", reciepts);
        }

        //
        // GET: /Reciept/Details/5

        [Authorize]
        public ActionResult Details(int id = 0)
        {
            RecieptEntity reciept = db.Reciepts.Find(id);
            if (reciept == null)
            {
                return HttpNotFound();
            }
            return View(reciept);
        }

        //
        // POST: /Reciept/Create

        [Authorize]
        [HttpPost]
        public ActionResult Create(int ProjectID, RecieptEntity reciept)
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
        // POST: /Reciept/Delete/5

        [Authorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid RecieptID)
        {
            RecieptEntity reciept = db.Reciepts.Find(RecieptID);
            db.Entry(reciept).Reference("Project").Load();
            Guid rid = reciept.Project.ID;

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