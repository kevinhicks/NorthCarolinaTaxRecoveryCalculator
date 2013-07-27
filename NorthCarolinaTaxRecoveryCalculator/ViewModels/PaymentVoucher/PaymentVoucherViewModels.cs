using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NorthCarolinaTaxRecoveryCalculator.Models.Data;
using System.ComponentModel.DataAnnotations;

namespace NorthCarolinaTaxRecoveryCalculator.ViewModels.PaymentVoucher
{
    public class PaymentVoucherViewModel
    {
        public PaymentVoucherViewModel()
        {
            Voucher = new Models.Data.PaymentVoucher();
            Vouchers = new List<Models.Data.PaymentVoucher>();
        }

        public List<Models.Data.PaymentVoucher> Vouchers { get; set; }
        public Models.Data.PaymentVoucher Voucher { get; set; }

        public void AddPaymentVoucher() {
            Vouchers.Add(new Models.Data.PaymentVoucher());
        }

        public void AddPaymentVoucherEntry()
        {
        }

        public void RemovePaymentVoucherEntry(int voucherIndex, int entryIndex)
        {
            Vouchers.ElementAt(voucherIndex).Entries.RemoveAt(entryIndex);
        }
    }
}
