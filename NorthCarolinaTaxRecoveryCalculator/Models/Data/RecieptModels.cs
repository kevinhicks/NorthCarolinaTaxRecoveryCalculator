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
    [Table("Reciepts")]
    public class RecieptEntity
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

        private float _salesTax = 0;
        [Display(Name = "Sales Tax")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public float SalesTax
        {
            get
            {
                if (County == NorthCarolinaTaxRecoveryCalculator.Models.County.NON_TAXABLE)
                    return 0;
                else
                    return _salesTax;
            }
            set
            {
                _salesTax = value;
            }
        }


        private float _foodTax = 0;
        [Display(Name = "Food Tax")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public float FoodTax
        {
            get
            {
                if (County == NorthCarolinaTaxRecoveryCalculator.Models.County.NON_TAXABLE)
                    return 0;
                else
                    return _foodTax;
            }
            set
            {
                _foodTax = value;
            }
        }

        [Required]
        [Display(Name = "Sales Amount")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public float SalesAmount { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }

        [Display(Name = "On Bill Detail")]
        public bool OnBillDetail { get; set; }

        public string CountyName
        {
            get
            {
                if (County > 0 && County < 101)
                {
                    return NorthCarolinaTaxRecoveryCalculator.Models.County.Counties[County].Name;
                }
                else
                {
                    return "Unknown";
                }
            }
        }

        /// <summary>
        /// Calculate the dollar amount of tax that went to the state
        /// </summary>
        /// <returns></returns>
        public double StateTaxPortion()
        {
            if (County < 1 || County > 101)
                throw new ArgumentOutOfRangeException("County");
            if (County == NorthCarolinaTaxRecoveryCalculator.Models.County.NON_TAXABLE)
                return 0;

            double totalTaxRates = TaxCalculator.TotalTaxRate(County, DateOfSale);

            return (SalesTax * (TaxCalculator.StateTaxRate / totalTaxRates));
        }

        /// <summary>
        /// Calculate the dollar amount of tax that went to the county
        /// </summary>
        /// <returns></returns>
        public double CountyTaxPortion()
        {
            if (County < 1 || County > 101)
                throw new ArgumentOutOfRangeException("County");
            if (County == NorthCarolinaTaxRecoveryCalculator.Models.County.NON_TAXABLE)
                return 0;

            double totalTaxRates = TaxCalculator.TotalTaxRate(County, DateOfSale);
            double countyRate = TaxCalculator.CountyTaxRate(County, DateOfSale);

            return (SalesTax * (countyRate / totalTaxRates));
        }

        /// <summary>
        /// Calculate the dollar amount of tax that went to transit Tax
        /// </summary>
        /// <returns></returns>
        public double TransitTaxPortion()
        {
            if (County < 1 || County > 101)
                throw new ArgumentOutOfRangeException("County");
            if (County == NorthCarolinaTaxRecoveryCalculator.Models.County.NON_TAXABLE)
                return 0;

            double totalTaxRates = TaxCalculator.TotalTaxRate(County, DateOfSale);
            double transitRate = TaxCalculator.CountyTransitTaxRate(County, DateOfSale);

            return (SalesTax * (transitRate / totalTaxRates));
        }
    }
}
