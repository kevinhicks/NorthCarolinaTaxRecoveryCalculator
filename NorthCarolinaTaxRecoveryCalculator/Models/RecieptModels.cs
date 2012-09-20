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
    public class RecieptModal
    {
        [Required]
        [Display(Name = "RIF")]
        public string RIF { get; set; }

        [Required]
        [Display(Name = "Date On Reciept")]
        [DataType(DataType.Date)]
        public string DateOfSale { get; set; }

        [Required]
        [Display(Name = "Store Name")]
        public string StoreName { get; set; }

        [Required]
        [Display(Name = "County")]
        public string County { get; set; }

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

        [Required]
        [Display(Name = "Notes")]
        public string Notes { get; set; }

        [Display(Name = "On Bill Detail")]        
        public bool OnBillDetail { get; set; }

    }
}
