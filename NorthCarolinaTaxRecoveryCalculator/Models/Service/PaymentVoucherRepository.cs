using NorthCarolinaTaxRecoveryCalculator.Models.Data;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml;
using System.Data;
using Omu.ValueInjecter;

namespace NorthCarolinaTaxRecoveryCalculator.Models.Service
{
    public interface IPaymentVoucherRepository
    {
        /// <summary>
        /// Return a single PaymentVoucher by its ID
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        PaymentVoucher Get(Guid PaymentVoucherID);

        IEnumerable<PaymentVoucher> GetAllForProject(Guid ProjectID);

        /// <summary>
        /// Update & Save the changes of  a PaymentVoucher
        /// </summary>
        /// <param name="voucher"></param>
        void Update(PaymentVoucher voucher);

        /// <summary>
        /// Create a new PaymentVoucher and save it into the store
        /// </summary>
        /// <param name="voucher"></param>
        void Create(PaymentVoucher voucher);

        /// <summary>
        /// Remove a PaymentVoucher form the store
        /// </summary>
        /// <param name="voucher"></param>
        void Delete(PaymentVoucher voucher);
    }
    /// <summary>
    /// Manages my Paymnet Vouchers
    /// </summary>
    public class PaymentVoucherRepository : IPaymentVoucherRepository
    {
        protected ApplicationDBContext db = null;
        public PaymentVoucherRepository()
        {
            db = new ApplicationDBContext();
        }

        public PaymentVoucher Get(Guid PaymentVoucherID)
        {
            var voucher = db.PaymentVouchers.Where(col => col.ID == PaymentVoucherID)
                                                             .Include(v => v.Project)
                                                             .FirstOrDefault();

            //doing this as a subquery helps
            voucher.Entries = db.PaymentVouchersEntries.Where(col => col.PaymentVoucherID == voucher.ID)
                                                       .OrderBy(col => col.Index)
                                                       .ToList();
                                                             

            //make sure that there are aloways the neccesary number of rows.
            if (voucher != null)
            {
                int numberOfEntriesNeededToFillOutVoucher = PaymentVoucher.NumberOfEntriesInAVoucher - voucher.Entries.Count;
                voucher.AddBlankRows(numberOfEntriesNeededToFillOutVoucher);
            }

            return voucher;
        }

        public void Update(PaymentVoucher Voucher)
        {
            if (Voucher == null)
                return;

            //Delete the previosu entries
            var org = db.PaymentVouchersEntries.Where(col => col.PaymentVoucherID == Voucher.ID).ToList();
            foreach (var entry in org)
            {
                db.PaymentVouchersEntries.Remove(entry);
            }
            db.SaveChanges();

            //Load the original Voucher into our db context
            var voucher = Get(Voucher.ID);

            //update the voucher
            db.Entry(voucher).CurrentValues.SetValues(Voucher);
            //and each entry
            for (int i = 0; i < Voucher.Entries.Count; i++)
            {
                db.Entry(voucher.Entries[i]).CurrentValues.SetValues(Voucher.Entries[i]);
            }

            //commit changes
            db.SaveChanges();            
        }


        public void Create(PaymentVoucher Voucher)
        {
            if (Voucher == null)
                return;

            foreach (var entry in Voucher.Entries)
            { entry.PaymentVoucherID = Voucher.ID; };

            //Voucher.RemoveBlankEntries();
            db.PaymentVouchers.Add(Voucher);
            db.SaveChanges();
        }

        public void Delete(PaymentVoucher voucher)
        {
            db.PaymentVouchers.Remove(voucher);
            db.SaveChanges();
        }

        public IEnumerable<PaymentVoucher> GetAllForProject(Guid ProjectID)
        {
            return db.PaymentVouchers.Where(col => col.Project.ID == ProjectID)
                                     .Include(v => v.Project)
                                     .Include(v => v.Entries)
                                     .ToList();
        }
    }
}