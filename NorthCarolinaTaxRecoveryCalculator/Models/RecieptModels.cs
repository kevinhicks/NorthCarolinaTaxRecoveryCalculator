using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace NorthCarolinaTaxRecoveryCalculator.Models
{
    public class Reciept
    {
        private Guid _guid = Guid.NewGuid();

        [Key]     
        public Guid ID
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

        public Guid ProjectID { get; set; }
        public virtual Project Project { get; set; }

        [Required]
        [Display(Name = "RIF")]
        public string RIF { get; set; }

        [Required]
        [Display(Name = "Date On Reciept")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime DateOfSale { get; set; }

        [Required]
        [Display(Name = "Store Name")]
        public string StoreName { get; set; }

        [Required]
        [Display(Name = "County")]
        public int County { get; set; }

        [Display(Name = "Sales Tax")]
        [DataType(DataType.Currency)]
        public float SalesTax { get; set; }

        [Display(Name = "Food Tax")]
        [DataType(DataType.Currency)]
        public float FoodTax { get; set; }

        [Required]
        [Display(Name = "Sales Amount")]
        [DataType(DataType.Currency)]
        public float SalesAmount { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }

        [Display(Name = "On Bill Detail")]
        public bool OnBillDetail { get; set; }


        /// <summary>
        /// Calculate the dollar amount of tax that went to the state
        /// </summary>
        /// <returns></returns>
        public double StateTaxPortion()
        {
            if (County < 1 || County > 101)
                throw new ArgumentOutOfRangeException("County");


            double totalTaxRates = TaxContext.TotalTaxRate(County, DateOfSale);

            return Math.Round(SalesTax * (TaxContext.StateTaxRate / totalTaxRates),2);
        }
        
        /// <summary>
        /// Calculate the dollar amount of tax that went to the county
        /// </summary>
        /// <returns></returns>
        public double CountyTaxPortion()
        {
            if (County < 1 || County > 101)
                throw new ArgumentOutOfRangeException("County");


            double totalTaxRates = TaxContext.TotalTaxRate(County, DateOfSale);
            double countyRate = TaxContext.CountyTaxRate(County, DateOfSale);

            return Math.Round(SalesTax * (countyRate / totalTaxRates), 2);
        }

        /// <summary>
        /// Calculate the dollar amount of tax that went to transit Tax
        /// </summary>
        /// <returns></returns>
        public double TransitTaxPortion()
        {
            //Only Mecklenbur has any transit tax
            if (County != NorthCarolinaTaxRecoveryCalculator.Models.County.MECKLENBURG)
            {
                return 0;
            }


            double totalTaxRates = TaxContext.TotalTaxRate(County, DateOfSale);

            return Math.Round(SalesTax * (TaxContext.TransitTaxRate / totalTaxRates), 2);
        }
    }
}
