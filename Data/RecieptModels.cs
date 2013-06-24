using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;

namespace Data
{
    public class RecieptDTO
    {
        public virtual Guid ID { get; set; }
        public virtual string RIF { get; set; }
        public virtual DateTime DateOfSale { get; set; }
        public virtual string StoreName { get; set; }
        public virtual int County { get; set; }

        private float _salesTax = 0;
        public virtual float SalesTax
        {
            get
            {
                if (County == Data.County.NON_TAXABLE)
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
        public virtual float FoodTax
        {
            get
            {
                if (County == Data.County.NON_TAXABLE)
                    return 0;
                else
                    return _foodTax;
            }
            set
            {
                _foodTax = value;
            }
        }

        public virtual float SalesAmount { get; set; }
        public virtual string Notes { get; set; }
        public virtual bool OnBillDetail { get; set; }

        /// <summary>
        /// Calculate the dollar amount of tax that went to the state
        /// </summary>
        /// <returns></returns>
        public double StateTaxPortion()
        {
            if (County < 1 || County > 101)
                throw new ArgumentOutOfRangeException("County");
            if (County == Data.County.NON_TAXABLE)
                return 0;

            double totalTaxRates = TaxContext.TotalTaxRate(County, DateOfSale);

            return (SalesTax * (TaxContext.StateTaxRate / totalTaxRates));
        }

        /// <summary>
        /// Calculate the dollar amount of tax that went to the county
        /// </summary>
        /// <returns></returns>
        public double CountyTaxPortion()
        {
            if (County < 1 || County > 101)
                throw new ArgumentOutOfRangeException("County");
            if (County == Data.County.NON_TAXABLE)
                return 0;

            double totalTaxRates = TaxContext.TotalTaxRate(County, DateOfSale);
            double countyRate = TaxContext.CountyTaxRate(County, DateOfSale);

            return (SalesTax * (countyRate / totalTaxRates));
        }

        /// <summary>
        /// Calculate the dollar amount of tax that went to transit Tax
        /// </summary>
        /// <returns></returns>
        public double TransitTaxPortion()
        {
            //Only Mecklenbur has any transit tax
            if (County != Data.County.MECKLENBURG)
            {
                return 0;
            }

            double totalTaxRates = TaxContext.TotalTaxRate(County, DateOfSale);

            return (SalesTax * (TaxContext.TransitTaxRate / totalTaxRates));
        }

        /// <summary>
        /// Return the Tax period that this reciept is in
        /// </summary>
        /// <returns></returns>
        public int GetTaxPeriod()
        {
            for (int i = 0; i < TaxContext.TaxPeriods.Length; i++)
            {
                if (this.DateOfSale >= TaxContext.TaxPeriods[i])
                {
                    return i;
                }
            }

            throw new Exception("We do not calculate tax on a recipet so old.");
        }
    }

    [Table("Reciepts")]
    public class RecieptEntity : RecieptDTO
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

        public Guid ProjectID { get; set; }
        public virtual Project Project { get; set; }

        [Required]
        [Display(Name = "RIF")]
        public override string RIF { get; set; }

        [Required]
        [Display(Name = "Date On Reciept")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public override DateTime DateOfSale { get; set; }

        [Required]
        [Display(Name = "Store Name")]
        public override string StoreName { get; set; }

        [Required]
        [Display(Name = "County")]
        public override int County { get; set; }

        [Display(Name = "Sales Tax")]
        [DataType(DataType.Currency)]
        public override float SalesTax { get; set; }

        [Display(Name = "Food Tax")]
        [DataType(DataType.Currency)]
        public override float FoodTax { get; set; }

        [Required]
        [Display(Name = "Sales Amount")]
        [DataType(DataType.Currency)]
        public override float SalesAmount { get; set; }

        [Display(Name = "Notes")]
        public override string Notes { get; set; }

        [Display(Name = "On Bill Detail")]
        public override bool OnBillDetail { get; set; }

    }
}
