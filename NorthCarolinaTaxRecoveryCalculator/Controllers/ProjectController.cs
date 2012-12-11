using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthCarolinaTaxRecoveryCalculator.Models;
using System.Web.Security;
using WebMatrix.WebData;
using NorthCarolinaTaxRecoveryCalculator.ViewModels.Project;
using NorthCarolinaTaxRecoveryCalculator.Misc;

namespace NorthCarolinaTaxRecoveryCalculator.Controllers
{
    public class ProjectController : Controller
    {
        private ApplicationDBContext db = new ApplicationDBContext();

        //
        // GET: /Project/
        [Authorize]
        public ActionResult Index()
        {
            int userID = WebSecurity.CurrentUserId;

            //We should ONLY show MY Projects & the Projects SHARED with me
            var ViewModel = new OwnedAndSharedProjectViewModels();

            //My Projects
            var myProjects = db.Projects
                .Where(proj => proj.OwnerID == userID)
                .ToList();

            ViewModel.MyProjects = myProjects;


            //Shared Projects
            var acls = db.UsersAccessProjects
                .Where(acl => acl.UserID == userID)
                .Select(acl => acl.ProjectID)
                .ToList();

            var sharedProjects = db.Projects
                .Where(proj => acls.Contains(proj.ID))
                .ToList();

            ViewModel.SharedProjects = sharedProjects;


            return View(ViewModel);
        }

        //
        // GET: /Project/Details/5
        [Authorize]
        public ActionResult Details(Guid ProjectID)
        {
            var ViewModel = new ProjectOverviewAndCollaboratorsViewModels();
            
            ViewModel.Project = db.Projects.Find(ProjectID);

            //Only the Project owner whould see the overview page
            if (ViewModel.Project.BelongsTo(WebSecurity.CurrentUserId))
            {
                if (ViewModel.Project == null)
                {
                    return HttpNotFound();
                }


                //Used to save confusion in the following query
                Guid _projID = ProjectID;

                //Find all the collaborators on this project
                var collaborators = db.UsersAccessProjects
                    .Where(acl => acl.ProjectID == _projID)
                    .ToList();

                ViewModel.UsersAccessProjects = collaborators;

                return View(ViewModel);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        
        //
        // POST: /Project/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                project.OwnerID = WebSecurity.CurrentUserId;
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Index", db.Projects.ToList());
        }

        //
        // GET: /Project/Edit/5
        [Authorize]
        public ActionResult Edit(Guid ProjectID)
        {
            Project project = db.Projects.Find(ProjectID);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        //
        // POST: /Project/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        //
        // GET: /Project/Delete/5
        [Authorize]
        public ActionResult Delete(Guid ProjectID)
        {
            Project project = db.Projects.Find(ProjectID);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        //
        // POST: /Project/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid ProjectID)
        {
            Project project = db.Projects.Find(ProjectID);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // POST: /Project/SendInvitation
        [Authorize]
        [HttpPost]
        public ActionResult SendInvitation(FormCollection inputs, Guid ProjectID)
        {
            string email = inputs["emailAddress"];

            //as long as there isnt already an invitaion sent
            if (db.UsersAccessProjects.Where(acl => acl.Email == email).Count() == 0)
            {

                var acl = new UsersAccessProjects();
                acl.Email = email;
                acl.invitationAccepted = false;
                acl.ProjectID = ProjectID;
                acl.UserID = null;
                db.UsersAccessProjects.Add(acl);
                db.SaveChanges();

                string body;
                body = "You have been invited to a new project.\n";
                body += "Click the link to accept the invitation.\n";
                body += "http://northcarolinataxrecoverycalculator.apphb.com/Project/AcceptInvite/" + acl.ID;

                EmailSender.SendEmail(email, "You have been invited to a project", body);
            }

            return RedirectToAction("Details", new { ProjectID = ProjectID });
        }

        //
        // GET: /Project/ResendInvitation/{aclID}
        [Authorize]
        public ActionResult ResendInvitation(int AclID)
        {
            //Find the original entry
            var acl = db.UsersAccessProjects.Find(AclID);

            //if it is valid 
            if (acl != null)
            {
                string body;
                body = "You have been invited to a new project.\n";
                body += "Click the link to accept the invitation.\n";
                body += "http://northcarolinataxrecoverycalculator.apphb.com/Project/AcceptInvite/" + acl.ID;

                EmailSender.SendEmail(acl.Email, "You have been invited to a project", body);
            }

            return RedirectToAction("Details", new { ProjectID = acl.ProjectID });
        }

        //
        // GET: /Project/RevokeInvitation/{aclID}
        [Authorize]
        public ActionResult RevokeInvitation(int AclID)
        {
            //Find the original entry
            var acl = db.UsersAccessProjects.Find(AclID);

            Guid projectId = acl.ProjectID;

            if (acl != null)
            {
                db.UsersAccessProjects.Remove(acl);
                db.SaveChanges();
            }

            return RedirectToAction("Details", new { ProjectID = projectId });
        }

        //
        // GET: /Project/AcceptInvite/{aclID}
        [Authorize]
        public ActionResult AcceptInvite(int AclID)
        {
            UsersAccessProjects acl = db.UsersAccessProjects.Find(AclID);

            //is the inviation still valid? maeby it was revoked?
            if (acl != null)
            {
                acl.UserID = WebSecurity.CurrentUserId;
                acl.invitationAccepted = true;

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        [ChildActionOnly]
        public ActionResult ProjectTotals(Guid ProjectID)
        {
            //Find all the reciepts for this project
            var reciepts = db.Reciepts.Where(rec => rec.Project.ID == ProjectID).ToList();
            
            //Load all related infomation to this project
            var project = db.Projects.Find(ProjectID);
            ViewBag.Project = project;

            //used to hold all the tax periods
            ViewBag.TaxPeriods = new IEnumerable<RecieptDTO>[TaxContext.TaxPeriods.Count()];

            //save each reciept for each tax period 
            for (int taxPeriod = 0; taxPeriod < TaxContext.TaxPeriods.Count(); taxPeriod++)
            {
                ViewBag.TaxPeriods[taxPeriod] = CalcProjectTotalsByTaxPeriodForAllCounties(project, taxPeriod);
            }

            return PartialView("_ProjectTotals", reciepts);
        }

        /// <summary>
        /// Calculate the taxes for All reciepts => in a project => in a single tax period
        /// </summary>
        /// <param name="project"></param>
        /// <param name="taxPeriod"></param>
        /// <returns></returns>
        private IEnumerable<RecieptDTO> CalcProjectTotalsByTaxPeriodForAllCounties(Project project, int taxPeriod)
        {
            //Invalid Tax Period?
            if (taxPeriod < 0 || taxPeriod > TaxContext.TaxPeriods.Count())
            {
                throw new ArgumentOutOfRangeException("taxPeriod");
            }

            //We want all the recipets AFTER the Start Date, And BEFORE the END DATE (or END-Of-TIME)
            DateTime start;
            DateTime end;

            start = TaxContext.TaxPeriods[taxPeriod];
            //If the taxperiod is the first one, then we want to calc all taex from the start untill the end of time
            if (taxPeriod == 0)
            {
                end = DateTime.MaxValue;
            }
            //Otherwise, get all the taxes untill the start of the next tax period
            else
            {
                end = TaxContext.TaxPeriods[taxPeriod - 1];
            }

            //This will hold, and be use to retunr our list of reciepts.
            //One for each county in this tax period
            var reciepts = new List<RecieptDTO>();

            //Get the reciepts from the data source
            reciepts = db.Reciepts
                            .Where(rec => rec.ProjectID == project.ID && rec.DateOfSale >= start && rec.DateOfSale < end)
                            .GroupBy(rec => rec.County)
	                        .Select(counties => new RecieptDTO {
		                        County = counties.Key,
                                DateOfSale = start,
		                        SalesTax = counties.Sum(rec => rec.SalesTax),
		                        FoodTax = counties.Sum(rec => rec.FoodTax),
		                        SalesAmount = counties.Sum(rec => rec.SalesAmount)
	                        })
                            .ToList();

            return reciepts;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}