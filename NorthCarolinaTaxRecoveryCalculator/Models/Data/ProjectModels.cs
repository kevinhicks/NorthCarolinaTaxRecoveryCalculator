using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace NorthCarolinaTaxRecoveryCalculator.Models
{
    public class Project
    {
        public Project()
        {
            ID = Guid.NewGuid();
            DateStarted = DateTime.Now;
            IsDeleted = false;
        }

        [Key]
        public Guid ID { get; set; }

        [Required]
        [Display(Name = "Project Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Date Started")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateStarted { get; set; }

        [Required]
        [Display(Name = "Deleted")]
        public bool IsDeleted { get; set; }
        public int OwnerID { get; set; }
        public virtual UserProfile Owner { get; set; }

        /// <summary>
        /// Selects all reciepts for a single project.
        /// Optionally filtered by date
        /// </summary>
        /// <param name="ProjectID">
        /// The Project we want to select from
        /// </param>
        /// <param name="filterStartDate">
        /// Only select recipets that are AFTER this date
        /// </param>
        /// <param name="filterEndDate">
        /// Only select recipets that are BEFORE this date
        /// </param>
        /// <returns></returns>
        public IEnumerable<RecieptEntity> FindReciepts(DateTime? filterStartDate = null, DateTime? filterEndDate = null)
        {
            var db = new ApplicationDBContext();

            //This will hold all our reciepts
            List<RecieptEntity> reciepts = null;

            //If there was both a start & end date 
            if (filterStartDate != null &&
               filterEndDate != null)
            {
                reciepts = db.Reciepts.Where(rec => rec.Project.ID == ID &&
                                         rec.DateOfSale >= filterStartDate &&
                                         rec.DateOfSale < filterEndDate).ToList();
            }
            //If there was only a start date 
            else if (filterStartDate != null &&
               filterEndDate == null)
            {
                reciepts = db.Reciepts.Where(rec => rec.Project.ID == ID &&
                                         rec.DateOfSale >= filterStartDate).ToList();
            }
            //If there was only a end date 
            else if (filterStartDate == null &&
               filterEndDate != null)
            {
                reciepts = db.Reciepts.Where(rec => rec.Project.ID == ID &&
                                         rec.DateOfSale < filterEndDate).ToList();
            }
            //If there was no date filter set
            else
            {
                reciepts = db.Reciepts.Where(rec => rec.Project.ID == ID).ToList();
            }

            return reciepts;
        }

        /// <summary>
        /// Return the total dollar amount that went to all counties during all time periods
        /// </summary>
        /// <returns></returns>
        public double GetTotalCountyTax()
        {
            var reciepts = new ApplicationDBContext().Reciepts.Where(col => col.ID == ID);
            return GetTotalCountyTax(reciepts);
        }

        /// <summary>
        /// Return the total dollar amount that went to all counties during all time periods
        /// </summary>
        /// <param name="Reciepts">
        /// The list of recipets to accumalate
        /// </param>
        /// <returns></returns>
        public double GetTotalCountyTax(IEnumerable<RecieptEntity> Reciepts)
        {
            double totalSalesTax = 0;

            //Loop thru all the reciepts in the project
            foreach (RecieptEntity reciept in Reciepts)
            {
                totalSalesTax += reciept.CountyTaxPortion();
            }
            return totalSalesTax;
        }

        /// <summary>
        /// Return the total dollar amount that went to all state during all time periods
        /// </summary>
        /// <returns></returns>
        public double GetTotalStateTax()
        {
            var reciepts = new ApplicationDBContext().Reciepts.Where(col => col.ID == ID);
            return GetTotalStateTax(reciepts);
        }

        /// <summary>
        /// Return the total dollar amount that went to the state during all time periods
        /// </summary>
        /// <param name="Reciepts"></param>
        /// <returns></returns>
        public double GetTotalStateTax(IEnumerable<RecieptEntity> Reciepts)
        {
            double totalSalesTax = 0;

            //Loop thru all the reciepts in the project
            foreach (RecieptEntity reciept in Reciepts)
            {
                totalSalesTax += reciept.StateTaxPortion();
            }
            return totalSalesTax;
        }

        /// <summary>
        /// Return the total dollar amount that went to transit tax during all time periods
        /// </summary>
        /// <returns></returns>
        public double GetTotalTransitTax(Guid ProjectID)
        {
            var reciepts = new ApplicationDBContext().Reciepts.Where(col => col.ID == ID);
            return GetTotalTransitTax(reciepts);
        }

        /// <summary>
        /// Return the total dollar amount that went to transit tax during all time periods
        /// </summary>
        /// <param name="Reciepts">
        /// The list of recipets to accumalate
        /// </param>
        /// <returns></returns>
        public double GetTotalTransitTax(IEnumerable<RecieptEntity> Reciepts)
        {
            double totalSalesTax = 0;

            //Loop thru all the reciepts in the project
            foreach (RecieptEntity reciept in Reciepts)
            {
                totalSalesTax += reciept.TransitTaxPortion();
            }
            return totalSalesTax;
        }

        /// <summary>
        /// Return the total dollar amount that went to food tax during all time periods
        /// </summary>
        /// <returns></returns>
        public double GetTotalFoodTax(Guid ProjectID)
        {
            var reciepts = new ApplicationDBContext().Reciepts.Where(col => col.ID == ID);
            return GetTotalFoodTax(reciepts);
        }

        /// <summary>
        /// Return the total dollar amount that went to Food tax during all time periods
        /// </summary>
        /// <param name="Reciepts"></param>
        /// <returns></returns>
        public double GetTotalFoodTax(IEnumerable<RecieptEntity> Reciepts)
        {
            double totalFoodTax = 0;

            //Loop thru all the reciepts in the project
            foreach (RecieptEntity reciept in Reciepts)
            {
                totalFoodTax += reciept.FoodTax;
            }
            return totalFoodTax;
        }


        /// <summary>
        /// Does a specifyed user OWN this project?
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public bool BelongsTo(int UserID)
        {
            if (UserID == OwnerID)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Is this project SHARED with a specifyed user?
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public bool SharedWith(int UserID)
        {
            return false;
        }
    }
}
