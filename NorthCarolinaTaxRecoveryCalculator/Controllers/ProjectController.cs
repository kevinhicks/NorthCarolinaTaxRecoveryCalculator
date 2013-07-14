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

namespace NorthCarolinaTaxRecoveryCalculator.Controllers
{
	public class ProjectController : Controller
	{
		private ApplicationDBContext db = new ApplicationDBContext();

        IEmailSender emailSender;

        public ProjectController(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

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
                ViewModel.FilterStartDate = filterStartDate;
                ViewModel.FilterEndDate = filterEndDate;
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
			if (db.UsersAccessProjects.Where(acl => acl.Email == email && acl.ProjectID == ProjectID).Count() == 0)
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

                emailSender.SendMail(email, "You have been invited to a project", body);
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
				acl.UserID = WebSecurity.CurrentUserId;
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
            List<RecieptEntity> reciepts = null;
            
            //If there was both a start & end date 
            if (filterStartDate != null &&
               filterEndDate != null)
            {
                reciepts = db.Reciepts.Where(rec => rec.Project.ID == ProjectID &&
                                         rec.DateOfSale >= filterStartDate &&
                                         rec.DateOfSale < filterEndDate).ToList();
            }
            //If there was only a start date 
            else if (filterStartDate != null &&
               filterEndDate == null)
            {
                reciepts = db.Reciepts.Where(rec => rec.Project.ID == ProjectID &&
                                         rec.DateOfSale >= filterStartDate).ToList();
            }
            //If there was only a end date 
            else if (filterStartDate == null &&
               filterEndDate != null)
            {
                reciepts = db.Reciepts.Where(rec => rec.Project.ID == ProjectID &&
                                         rec.DateOfSale < filterEndDate).ToList();
            }
            //If there was no date filter set
            else
            {
                reciepts = db.Reciepts.Where(rec => rec.Project.ID == ProjectID).ToList();
            }
			
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

		/// <summary>
		/// Get: /Project/PrintReciepts/{ProjectID}
		/// 
		/// Print a list of the reciepts
		/// </summary>
		/// <returns></returns>
		[Authorize]
		public ActionResult PrintReciepts(Guid ProjectID)
		{
			//All the recipets in THIS project, sorted correctly
			var reciepts = db.Reciepts.Where(rec => rec.ProjectID == ProjectID).AsEnumerable().OrderBy(rec => rec.RIF, new NaturalSortComparer<string>()).ToList();

			return View(reciepts);
		}

		/// <summary>
		/// Get: /Project/PrintReciepts/{ProjectID}
		/// 
		/// Print a list of the reciepts as a PDF
		/// </summary>
		/// <returns>A binary stream dirctly to the client (PDF)</returns>
		[Authorize]
		public ActionResult PrintRecieptsPDF(Guid ProjectID)
		{
			Guid p = ProjectID;

			//The name of the project
			ViewBag.ProjectName = db.Projects.Find(ProjectID).Name;

			//find all the reciepts in this project
			var reciepts = db.Reciepts.Where(rec => rec.ProjectID == ProjectID).AsEnumerable().OrderBy(rec => rec.RIF, new NaturalSortComparer<string>()).ToList();
			
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
		public ActionResult ExportToExcel(Guid ProjectID)
		{

			var project = db.Projects.Find(ProjectID);

			//What if the ProjectID is invalid?
			if(project == null) {
				RedirectToAction("Details", new {ProjectID = ProjectID});
			}

			//This is what will be sent back to the client
			byte[] response = null;

			//all the reciepts for this project
			var reciepts = db.Reciepts.Where(rec => rec.ProjectID == ProjectID).AsEnumerable().OrderBy(rec => rec.RIF, new NaturalSortComparer<string>()).ToList();

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