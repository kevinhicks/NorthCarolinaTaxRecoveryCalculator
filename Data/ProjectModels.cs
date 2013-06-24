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

        public virtual IEnumerable<Reciept> Reciepts { get; set; }

        /// <summary>
        /// Return the total dollar amount that went to all counties during all time periods
        /// </summary>
        /// <param name="Reciepts"></param>
        /// <returns></returns>
        public double GetTotalCountyTax(IEnumerable<Reciept> Reciepts)
        {
            double totalSalesTax = 0;

            //Loop thru all the reciepts in the project
            foreach (Reciept reciept in Reciepts)
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
        public double GetTotalStateTax(IEnumerable<Reciept> Reciepts)
        {
            double totalSalesTax = 0;

            //Loop thru all the reciepts in the project
            foreach (Reciept reciept in Reciepts)
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
        public double GetTotalTransitTax(IEnumerable<Reciept> Reciepts)
        {
            double totalSalesTax = 0;

            //Loop thru all the reciepts in the project
            foreach (Reciept reciept in Reciepts)
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
        public double GetTotalFoodTax(IEnumerable<Reciept> Reciepts)
        {
            double totalFoodTax = 0;

            //Loop thru all the reciepts in the project
            foreach (Reciept reciept in Reciepts)
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

    public class Project
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public Guid ID { get; set; }
        public string Name { get; set; }
        public DateTime DateStarted { get; set; }
        public bool IsDeleted { get; set; }
        public int OwnerID { get; set; }
        public virtual UserProfile Owner { get; set; }
        public virtual IEnumerable<Reciept> Reciepts { get; set; }

        public Project() {
            //Sensible Defaults
            ID = Guid.NewGuid();
            DateStarted = DateTime.Now;
            IsDeleted = false;
        }
    }
}
