using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace NorthCarolinaTaxRecoveryCalculator.Models.Data
{
    public class PaymentVoucher
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        public string ProjectName { get; set; }

        [Required]
        public string CheckNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required]
        public string PaidTo { get; set; }

        public List<PaymentVoucherEntry> Entries {get;set;}

        public string PreparedBy { get; set; }
        public string ApprovedBy { get; set; }
        public string RBCApproval { get; set; }
        
        public PaymentVoucher()
        {
            ID = Guid.NewGuid();
            Date = DateTime.Now;

            Entries = new List<PaymentVoucherEntry>();
        }
    }

    public class PaymentVoucherEntry
    {
        [Key]
        public Guid ID { get; set; }

        public Guid PaymentVoucherID { get; set; }

        public string Item { get; set; }
        public string CostElement { get; set; }
        public float Amount { get; set; }

        public PaymentVoucherEntry()
        {
            ID = Guid.NewGuid();
        }
    }
}
