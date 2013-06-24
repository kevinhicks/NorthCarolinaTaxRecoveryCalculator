using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;

namespace Data
{
    public class ProjectDTO
    {
        public virtual Guid ID { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime DateStarted { get; set; }
        public virtual bool IsDeleted { get; set; }

        public int OwnerID { get; set; }
        public virtual UserProfile Owner { get; set; }

        public virtual IEnumerable<RecieptEntity> Reciepts { get; set; }

        /// <summary>
        /// Return the total dollar amount that went to all counties during all time periods
        /// </summary>
        /// <param name="Reciepts"></param>
        /// <returns></returns>
        public double GetTotalCountyTax(IEnumerable<RecieptEntity> Reciepts)
        {
            double totalSalesTax = 0;

            //Loop thru all the reciepts in the project
            foreach (RecieptEntity reciept in Reciepts)
            {
                //First, make sure that it belongs to this project!
                if (reciept.Project.ID != ID)
                {
                    continue;
                }

                totalSalesTax += reciept.CountyTaxPortion();
            }
            return totalSalesTax;
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
                //First, make sure that it belongs to this project!
                if (reciept.Project.ID != ID)
                {
                    continue;
                }

                totalSalesTax += reciept.StateTaxPortion();
            }
            return totalSalesTax;
        }

        /// <summary>
        /// Return the total dollar amount that went to transit tax during all time periods
        /// </summary>
        /// <param name="Reciepts"></param>
        /// <returns></returns>
        public double GetTotalTransitTax(IEnumerable<RecieptEntity> Reciepts)
        {
            double totalSalesTax = 0;

            //Loop thru all the reciepts in the project
            foreach (RecieptEntity reciept in Reciepts)
            {
                //First, make sure that it belongs to this project!
                if (reciept.Project.ID != ID)
                {
                    continue;
                }

                totalSalesTax += reciept.TransitTaxPortion();
            }
            return totalSalesTax;
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
                //First, make sure that it belongs to this project!
                if (reciept.Project.ID != ID)
                {
                    continue;
                }

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

    public class Project : ProjectDTO
    {
        private Guid _guid = Guid.NewGuid();

        [Key]
        public override Guid ID
        {
            get
            {
                return _guid;
            }
            set
            {
                _guid = value;
            }
        }

        [Required]
        [Display(Name = "Project Name")]
        public override string Name { get; set; }

        /// <summary>
        /// Default value for DateStarted is today
        /// </summary>
        private DateTime _DateStarted = DateTime.Now;

        [Required]
        [Display(Name = "Date Started")]
        [DataType(DataType.Date)]
        public override DateTime DateStarted
        {
            get
            {
                return _DateStarted;
            }
            set
            {
                _DateStarted = value;
            }

        }

        [Required]
        [Display(Name = "Deleted")]
        public override bool IsDeleted { get; set; }
    }
}
