using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using System.Linq;

namespace NorthCarolinaTaxRecoveryCalculator.Models.Data
{
    public class PaymentVoucher
    {
        [Key]
        public Guid ID { get; set; }

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

        /// <summary>
        /// Remove any entry that does not contain at least 1 value
        /// </summary>
        public void RemoveBlankEntries()
        {
            Entries = Entries.Where(v => !v.IsBlankEntry()).ToList();
            /*
            //loop backwards thru list to keep indexes in order
            for(int i = Entries.Count - 1; i >= 0; i--)
            {
                //Find at least 1 value in this entry
                if (Entries[i].IsBlankEntry())
                {
                    Entries.RemoveAt(i);
                }
            }*/
        }

        /// <summary>
        /// Add new rows to the list of entries
        /// </summary>
        /// <param name="number"></param>
        public void AddBlankRows(int number)
        {
            for (int i = 0; i < number; i++)
            {
                this.Entries.Add(new PaymentVoucherEntry());
            }
        }

        public Project Project { get; set; }
    }

    public class PaymentVoucherEntry
    {
        [Key]
        public Guid ID { get; set; }

        public Guid PaymentVoucherID { get; set; }

        public string Item { get; set; }
        public string CostElement { get; set; }
        public double Amount { get; set; }

        public PaymentVoucherEntry()
        {
            ID = Guid.NewGuid();
        }

        //Is any field, other than the ID, filled in?
        public bool IsBlankEntry()
        {
            if ((Item == null || Item.Trim() == "") &&
                   (CostElement == null || CostElement.Trim() == "") &&
                   (Amount == 0.0))
            {
                return true;
            }

            return false;
        }
    }
}
