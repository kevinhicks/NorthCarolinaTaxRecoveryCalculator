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
    public class ListAndCreateReciept
    {
        public Guid ProjectID;

        private Reciept m_Reciept = new Reciept();
        [Required]
        public Reciept Reciept
        {
            get
            {
                return m_Reciept;
            }
            set
            {
                m_Reciept = value;
            }
        }

        public IEnumerable<Reciept> Reciepts { get; set; }

        public IEnumerable<County> Counties { get; set; }
    }

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
        public string DateOfSale { get; set; }

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
    }
}
