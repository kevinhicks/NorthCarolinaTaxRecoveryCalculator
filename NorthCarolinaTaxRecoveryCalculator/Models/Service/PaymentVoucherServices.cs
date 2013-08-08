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

namespace NorthCarolinaTaxRecoveryCalculator.Models
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
    public class PaymentVoucherManager : IPaymentVoucherRepository
    {
        protected ApplicationDBContext db = null;
        public PaymentVoucherManager()
        {
            db = new ApplicationDBContext();
        }

        public PaymentVoucher Get(Guid PaymentVoucherID)
        {
            return db.PaymentVouchers.Where(col => col.ID == PaymentVoucherID)
                                                      .Include(v => v.Project)
                                                      .Include(v => v.Entries)
                                                      .FirstOrDefault();
        }

        public void Update(PaymentVoucher Voucher)
        {
            if (Voucher == null)
                return;

            Voucher.RemoveBlankEntries();

            //Get fresh copy from db
            var org = Get(Voucher.ID);
            db.Entry(org).CurrentValues.SetValues(Voucher);

            //remove previous entries
            foreach (var entry in org.Entries.ToList())
            {
                db.PaymentVouchersEntries.Remove(entry);
            }

            //add new entries
            foreach (var entry in Voucher.Entries)
            {
                db.PaymentVouchersEntries.Add(entry);
            }
            
            //commit            
            db.SaveChanges();
        }


        public void Create(PaymentVoucher Voucher)
        {
            if (Voucher == null)
                return;

            Voucher.RemoveBlankEntries();
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