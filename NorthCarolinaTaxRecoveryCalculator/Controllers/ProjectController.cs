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
using Rotativa;
using System.Text.RegularExpressions;
using OfficeOpenXml;
using System.Globalization;
using NorthCarolinaTaxRecoveryCalculator.Security;
using NorthCarolinaTaxRecoveryCalculator.Models.Service;

namespace NorthCarolinaTaxRecoveryCalculator.Controllers
{
    public class ProjectController : Controller
    {
        private ApplicationDBContext db = new ApplicationDBContext();

        IEmailSender emailSender;
        IUserRepository user;
        IProjectRepository ProjectRepository;

        public ProjectController(IUserRepository user, 
                                 IEmailSender emailSender, 
                                 IProjectRepository projectRepository)
        {
            this.emailSender = emailSender;
            this.user = user;
            this.ProjectRepository = projectRepository;
        }

        //
        // GET: /Project/
        [Authorize]
        public ActionResult Index()
        {
            int userID = user.CurrentUserId;

            //We should ONLY show MY Projects & the Projects SHARED with me
            var ViewModel = new OwnedAndSharedProjectViewModels();
            
            ViewModel.MyProjects = ProjectRepository.FindProjectsOwnedByUser(userID);
            ViewModel.SharedProjects = ProjectRepository.FindProjectsSharedWithUser(userID);

            return View(ViewModel);
        }

        /// <summary>
        /// GET: /Project/Details/5
        /// This is the project overview page
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <param name="filterStartDate">
        ///     This will limit reciept totals to anything after the start-date, 
        ///     and will be passed on to the child-view
        /// </param>
        /// <param name="filterEndDate">
        ///     This will limit reciept totals to anything before the end-date, 
        ///     and will be passed on to the child-view
        /// </param>
        /// <returns></returns>
        [Authorize]
        public ActionResult Details(Guid ProjectID, DateTime? filterStartDate, DateTime? filterEndDate)
        {
            var ViewModel = new ProjectOverviewAndCollaboratorsViewModels();

            var project = ProjectRepository.FindProjectByID(ProjectID);            

            //Make sure that it is a valid ProjectID 
            if (project == null)
            {
                return HttpNotFound();
            }

            ViewModel.Project = project;

            //Only the Project owner whould see the admin dashboard page
            if (!project.BelongsTo(user.CurrentUserId))
            {
                return View("DashBoard", project);
            }

            //Find all the reciepts for this view
            var reciepts = project.FindReciepts(filterStartDate, filterEndDate);

            //Used to hold all the tax periods
            ViewModel.CountyTotals = CalcProjectTotalsByTaxPeriodForAllCounties(reciepts);


            //Find all the collaborators on this project            
            //TODO: THIS USES ACLMANAGER, AND SHOUD BE REWITEEN TO USE PROJECTREPOSITORY.
            ViewModel.UsersAccessProjects = new ACLManager().FindAllCollaborators(ProjectID);

            ViewModel.FilterStartDate = filterStartDate;
            ViewModel.FilterEndDate = filterEndDate;

            ViewModel.TotalCountyTaxForProject = project.GetTotalCountyTax(reciepts);
            ViewModel.TotalStateTaxForProject = project.GetTotalStateTax(reciepts);
            ViewModel.TotalFoodTaxForProject = project.GetTotalFoodTax(reciepts);
            ViewModel.TotalTransitTaxForProject = project.GetTotalTransitTax(reciepts);

            return View("OwnerDashboard", ViewModel);
        }

        //
        // POST: /Project/Create
        [Authorize]
        [HttpPost]
        public ActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                project.OwnerID = user.CurrentUserId;
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
        public ActionResult SendInvitation(ProjectOverviewAndCollaboratorsViewModels model, Guid ProjectID)
        {
            //string email = inputs["emailAddress"];

            //poor mans validation of missing email
            if (model.InvitationEmail.Trim() != "")
            {
                //TODO: THIS USES ACLMANAGER, AND SHOUD BE REWITEEN TO USE PROJECTREPOSITORY.
                var acl = new ACLManager();
                acl.SendInvitation(model.InvitationEmail, ProjectID, UserType.DataEntry, emailSender);                
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

                emailSender.SendMail(acl.Email, "You have been invited to a project", body);
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
                acl.UserID = user.CurrentUserId;
                acl.invitationAccepted = true;

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// This display all the totals, and the break down for a specific project.
        /// It shoudl ONLY be viewable to the Project Owner
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <param name="filterStartDate"></param>
        /// <param name="filterEndDate"></param>
        /// <returns></returns>
        [Authorize]
        [ChildActionOnly]
        public ActionResult ProjectTotals(Guid ProjectID, DateTime? filterStartDate, DateTime? filterEndDate)
        {
            //Find all the reciepts for this project
            List<RecieptEntity> reciepts = db.Reciepts.Where(rec => rec.Project.ID == ProjectID).ToList();

            //Load all related infomation to this project
            var project = db.Projects.Find(ProjectID);
            ViewBag.Project = project;

            //used to hold all the tax periods
            ViewBag.TaxPeriods = new IEnumerable<RecieptEntity>[TaxPeriods.Periods.Count()];

            //save each reciept for each tax period 
            for (int i = 0; i < TaxPeriods.Periods.Count(); i++)
            {
                //ViewBag.TaxPeriods[i] = CalcProjectTotalsByTaxPeriodForAllCounties(reciepts, TaxPeriods.Periods[i]);
            }

            return PartialView("_ProjectTotals", reciepts);
        }

        /// <summary>
        /// Calculate the taxes for All reciepts  in a project 
        /// </summary>
        /// <param name="project"></param>
        /// <returns>
        /// A list of reciepts. One for each county. Each county is the total of ALL reciepts 
        /// in that county
        /// </returns>
        private IEnumerable<CountyTotals> CalcProjectTotalsByTaxPeriodForAllCounties(IEnumerable<RecieptEntity> reciepts)
        {
            //Hold the totals for 100 counties + non taxable
            var countyTotals = new CountyTotals[101];

            for (int i = 0; i < 101; i++)
            {
                countyTotals[i] = new CountyTotals();

                //Get all the reciepts for a single county
                var recipetsFromACounty = reciepts.Where(rec => rec.County == i - 1);

                //Total the taex for all recitpes of this county
                foreach (var reciept in recipetsFromACounty)
                {
                    countyTotals[i].CountyTax += reciept.CountyTaxPortion();
                    countyTotals[i].StateTax += reciept.StateTaxPortion();
                    countyTotals[i].FoodTax += reciept.FoodTax;
                    countyTotals[i].TransitTax += reciept.TransitTaxPortion();
                    countyTotals[i].Name = reciept.CountyName;
                }
            }
            return countyTotals;

            //Group the list by county. and sum the totals for each county
            /*return reciepts.GroupBy(rec => rec.County)
                   .Select(counties => new RecieptEntity
                   {
                       County = counties.Key,
                       SalesTax = counties.Sum(rec => rec.SalesTax),
                       FoodTax = counties.Sum(rec => rec.FoodTax),
                       SalesAmount = counties.Sum(rec => rec.SalesAmount)
                   })
                   .ToList();*/
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        /// <summary>
        /// Get: /Project/PrintReciepts/{ProjectID}
        /// 
        /// Print a list of the reciepts
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult PrintReciepts(Guid ProjectID, DateTime? filterStartDate, DateTime? filterEndDate)
        {
            //All the recipets in THIS project, sorted correctly
            var project = db.Projects.Find(ProjectID);
            var reciepts = project.FindReciepts(filterStartDate, filterEndDate).OrderBy(rec => rec.RIF, new NaturalSortComparer<string>()).ToList();

            return View(reciepts);
        }

        /// <summary>
        /// Get: /Project/PrintReciepts/{ProjectID}
        /// 
        /// Print a list of the reciepts as a PDF
        /// </summary>
        /// <returns>A binary stream dirctly to the client (PDF)</returns>
        [Authorize]
        public ActionResult PrintRecieptsPDF(Guid ProjectID, DateTime? filterStartDate, DateTime? filterEndDate)
        {
            //find all the reciepts in this project            
            var project = db.Projects.Find(ProjectID);
            var reciepts = project.FindReciepts(filterStartDate, filterEndDate).OrderBy(rec => rec.RIF, new NaturalSortComparer<string>()).ToList();

            //create a PDF of the list of reciepts
            var pdf = new ViewAsPdf("PrintReciepts", reciepts)
            {
                FileName = "Invoice.pdf",
                CustomSwitches = "--print-media-type"
            };

            //generate a binary to stream it to the browser. e.g NOT a download, but rather opens as a page
            var bin = pdf.BuildPdf(this.ControllerContext);

            return File(bin, "application/pdf");
        }

        /// <summary>
        /// Get: /Project/ExportToExcel/{ProjectID}
        /// 
        /// Export the list of recipets the an excel file for offline viewing
        /// 
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns>A binary stream dirctly to the client (Excel)</returns>
        public ActionResult ExportToExcel(Guid ProjectID, DateTime? filterStartDate, DateTime? filterEndDate)
        {

            var project = db.Projects.Find(ProjectID);

            //What if the ProjectID is invalid?
            if (project == null)
            {
                RedirectToAction("Details", new { ProjectID = ProjectID });
            }

            //This is what will be sent back to the client
            byte[] response = null;

            //all the reciepts for this project
            var reciepts = project.FindReciepts(filterStartDate, filterEndDate).OrderBy(rec => rec.RIF, new NaturalSortComparer<string>()).ToList();

            //Create the excel file
            using (ExcelPackage excel = new ExcelPackage())
            {

                //Add a new workesheet
                ExcelWorksheet recieptsWorksheet = excel.Workbook.Worksheets.Add("Reciepts");

                //Count the rows
                int row = 1;

                //Hold the columns
                string rifCol = "A";
                string dateCol = "B";
                string storeCol = "C";
                string countyCol = "D";
                string stateTaxCol = "E";
                string foodTaxCol = "F";
                string totalAmtCol = "G";
                string clearedCol = "H";

                //For each recipet, enter a new row
                foreach (var reciept in reciepts)
                {
                    recieptsWorksheet.Cells[rifCol + row].Value = reciept.RIF;
                    recieptsWorksheet.Cells[dateCol + row].Value = reciept.DateOfSale;
                    recieptsWorksheet.Cells[storeCol + row].Value = reciept.StoreName;
                    if (reciept.County == County.NON_TAXABLE)
                    {
                        recieptsWorksheet.Cells[countyCol + row].Value = County.Counties[reciept.County].Name.ToUpper();
                    }
                    else
                    {
                        recieptsWorksheet.Cells[countyCol + row].Value = "  " + County.Counties[reciept.County].Name.ToUpper();
                    }
                    recieptsWorksheet.Cells[stateTaxCol + row].Value = Math.Round(reciept.SalesTax, 2);
                    recieptsWorksheet.Cells[foodTaxCol + row].Value = Math.Round(reciept.FoodTax, 2);
                    recieptsWorksheet.Cells[totalAmtCol + row].Value = Math.Round(reciept.SalesAmount, 2);
                    if (reciept.OnBillDetail)
                    {
                        recieptsWorksheet.Cells[clearedCol + row].Value = "x";
                    }


                    row++;
                }

                //Pretty formatting
                string currencyFormat = "@";//"$#,##0.00_);[Red]($#,##0.00)";

                //Rif
                recieptsWorksheet.Column(1).BestFit = true;

                //Date
                recieptsWorksheet.Column(2).Style.Numberformat.Format = "mm/dd/yy";
                recieptsWorksheet.Column(2).BestFit = true;

                //Store
                recieptsWorksheet.Column(3).BestFit = true;

                //County
                recieptsWorksheet.Column(3).BestFit = true;

                //State tax
                //recieptsWorksheet.Column(5).Style.Numberformat.Format = currencyFormat;
                recieptsWorksheet.Column(3).BestFit = true;

                //Food Tax
                //recieptsWorksheet.Column(6).Style.Numberformat.Format = currencyFormat;
                recieptsWorksheet.Column(3).BestFit = true;

                //Total Amount
                //recieptsWorksheet.Column(7).Style.Numberformat.Format = currencyFormat;
                recieptsWorksheet.Column(3).BestFit = true;

                //save the contents before disposing the Excel Builder
                response = excel.GetAsByteArray();
            }

            //return the file contens to the client
            return File(response, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", project.Name + " Tax Recovery");
        }
    }

    public class NaturalSortComparer<T> : IComparer<string>, IDisposable
    {
        private bool isAscending;

        public NaturalSortComparer(bool inAscendingOrder = true)
        {
            this.isAscending = inAscendingOrder;
        }

        #region IComparer<string> Members

        public int Compare(string x, string y)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IComparer<string> Members

        int IComparer<string>.Compare(string x, string y)
        {
            if (x == y)
                return 0;

            string[] x1, y1;

            if (!table.TryGetValue(x, out x1))
            {
                x1 = Regex.Split(x.Replace(" ", ""), "([0-9]+)");
                table.Add(x, x1);
            }

            if (!table.TryGetValue(y, out y1))
            {
                y1 = Regex.Split(y.Replace(" ", ""), "([0-9]+)");
                table.Add(y, y1);
            }

            int returnVal;

            for (int i = 0; i < x1.Length && i < y1.Length; i++)
            {
                if (x1[i] != y1[i])
                {
                    returnVal = PartCompare(x1[i], y1[i]);
                    return isAscending ? returnVal : -returnVal;
                }
            }

            if (y1.Length > x1.Length)
            {
                returnVal = 1;
            }
            else if (x1.Length > y1.Length)
            {
                returnVal = -1;
            }
            else
            {
                returnVal = 0;
            }

            return isAscending ? returnVal : -returnVal;
        }

        private static int PartCompare(string left, string right)
        {
            int x, y;
            if (!int.TryParse(left, out x))
                return left.CompareTo(right);

            if (!int.TryParse(right, out y))
                return left.CompareTo(right);

            return x.CompareTo(y);
        }

        #endregion

        private Dictionary<string, string[]> table = new Dictionary<string, string[]>();

        public void Dispose()
        {
            table.Clear();
            table = null;
        }
    }
}