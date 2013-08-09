using NorthCarolinaTaxRecoveryCalculator.Models.Data;
using System;
using System.Collections;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.xml;
using System.IO;
using System.Web.Hosting;

namespace NorthCarolinaTaxRecoveryCalculator.Misc
{
    public class PaymentVoucherReportGenerator
    {
        public byte[] GeneratePDFForVoucher(PaymentVoucher Voucher)
        {
            var path = FileFinder.FindFile("PaymentVoucher.pdf");
            /*HostingEnvironment.MapPath("~/PaymentVoucher.pdf");

            //Try alternate filepath. mabey we are in tesing?
            if (!File.Exists(path))
            {
                path = "PaymentVoucher.pdf";
            }

            //still no?
            if (!File.Exists(path))
            {
                path = System.AppDomain.CurrentDomain.BaseDirectory + "/PaymentVoucher.pdf";
            }

            //Did the alternate path work?
            if (!File.Exists(path))
            {
                throw new Exception("Could Not Load My PDF");
            }*/

            //Open our template pdf
            PdfReader pdfReader = new PdfReader(path);


            var stream = new MemoryStream();//(tempFileName, FileMode.Create);
            var pdfStamper = new PdfStamper(pdfReader, stream);
            AcroFields pdfFormFields = pdfStamper.AcroFields;

            //Fill in the forms
            pdfFormFields.SetField("Building Project", Voucher.Project.Name);
            pdfFormFields.SetField("Date", Voucher.Date.ToShortDateString());
            pdfFormFields.SetField("Check Number", Voucher.CheckNumber);
            pdfFormFields.SetField("Paid to:", Voucher.PaidTo);
            if (Voucher.PreparedBy != null)
                pdfFormFields.SetField("Prepared By:", Voucher.PreparedBy);
            if (Voucher.ApprovedBy != null)
                pdfFormFields.SetField("Approved By:", Voucher.ApprovedBy);
            if (Voucher.RBCApproval != null)
                pdfFormFields.SetField("Regional Building Committee Approval:", Voucher.RBCApproval);

            //List of entries
            for (int i = 0; (i < 20) && (i < Voucher.Entries.Count); i++)
            {
                var entry = Voucher.Entries[i];
                pdfFormFields.SetField("ItemRow" + (i + 1), entry.Item.ToString());
                pdfFormFields.SetField("Cost ElementRow" + (i + 1), entry.CostElement.ToString());
                pdfFormFields.SetField("AmountRow" + (i + 1), entry.Amount.ToString());
                pdfFormFields.RegenerateField("AmountRow" + (i + 1));
            }

            //IDK
            pdfStamper.FormFlattening = false;
            pdfStamper.Close();

            return stream.ToArray();
        }
    }
}
