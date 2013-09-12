using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using System.Linq;
using NorthCarolinaTaxRecoveryCalculator.Misc;
using System.ComponentModel;

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

        public List<PaymentVoucherEntry> Entries { get; set; }

        public string PreparedBy { get; set; }
        public string ApprovedBy { get; set; }
        public string RBCApproval { get; set; }

        public Guid ProjectID { get; set; }
        public virtual Project Project { get; set; }

        [Required]
        public string TaxCostElement { get; set; }
        public double TaxAmount { get; set; }

        public PaymentVoucher()
        {
            ID = Guid.NewGuid();
            Date = DateTime.Now;

            Entries = new List<PaymentVoucherEntry>();

            AddBlankRows(NumberOfEntriesInAVoucher);            
        }

        /// <summary>
        /// How mant rows(entries) are on a payment voucher
        /// </summary>
        public static int NumberOfEntriesInAVoucher
        {
            get { return 20; }
        }

        /// <summary>
        /// Remove any entry that does not contain at least 1 value
        /// </summary>
        public void RemoveBlankEntries()
        {
            //Entries = Entries.Where(v => !v.IsBlankEntry()).ToList();
        }

        /// <summary>
        /// Add new rows to the list of entries
        /// </summary>
        /// <param name="number"></param>
        public void AddBlankRows(int number)
        {
            for (int i = 0; i < number; i++)
            {
                var entry = new PaymentVoucherEntry();
                entry.PaymentVoucherID = ID;
                this.Entries.Add(entry);
            }
        }

        /// <summary>
        /// Print this vouhcer to PDF, and return the bytes
        /// </summary>
        /// <returns></returns>
        public byte[] Print()
        {
            //We do not do anythign if there is not a project associated with this
            if (this.Project == null)
                return null;

            //Or, if ther are no entires
            if (this.Entries == null)
                return null;

            //Generate the report, and retun the bytes
            return new PaymentVoucherReportGenerator().GeneratePDFForVoucher(this);
        }
    }

    public class PaymentVoucherEntry
    {
        [Key]
        public int ID { get; set; }

        public Guid PaymentVoucherID { get; set; }

        public string Item { get; set; }
        public string CostElement { get; set; }
        public double Amount { get; set; }

        public PaymentVoucherEntry()
        {
            ID = 0;// Guid.NewGuid();
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
