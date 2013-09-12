using NorthCarolinaTaxRecoveryCalculator.Models;
using NorthCarolinaTaxRecoveryCalculator.Models.Data;
using NorthCarolinaTaxRecoveryCalculator.Models.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NorthCarolinaTaxRecoveryCalculator.Tests.Models.Mocks
{
    class MockPaymentVoucherRepository : IPaymentVoucherRepository
        {
            private List<PaymentVoucher> vouchers;
            public MockPaymentVoucherRepository()
            {
                vouchers = new List<PaymentVoucher>();
            }

            public NorthCarolinaTaxRecoveryCalculator.Models.Data.PaymentVoucher Get(Guid PaymentVoucherID)
            {
                return vouchers.Where(col => col.ID == PaymentVoucherID).FirstOrDefault();
            }

            public IEnumerable<NorthCarolinaTaxRecoveryCalculator.Models.Data.PaymentVoucher> GetAllForProject(Guid ProjectID)
            {
                return vouchers.Where(col => col.ProjectID == ProjectID).ToList();
            }

            public void Update(NorthCarolinaTaxRecoveryCalculator.Models.Data.PaymentVoucher voucher)
            {
                return;
            }

            public void Create(NorthCarolinaTaxRecoveryCalculator.Models.Data.PaymentVoucher voucher)
            {
                vouchers.Add(voucher);
            }

            public void Delete(NorthCarolinaTaxRecoveryCalculator.Models.Data.PaymentVoucher voucher)
            {
                vouchers.Remove(voucher);
            }
        }
}
