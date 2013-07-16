using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml;

namespace NorthCarolinaTaxRecoveryCalculator.Models
{
    public class TaxCalculator
    { 
        /// <summary>
        /// The constant tax rate the that State Always gets
        /// </summary>
        public static double StateTaxRate
        {
            get
            {
                return 4.75;
            }
        }

        /// <summary>
        /// Find the total RATE(or percentage) of the sales amount that was tax
        /// </summary>
        /// <param name="county"></param>
        /// <param name="dateOfSale"></param>
        /// <returns></returns>
        public static double TotalTaxRate(int county, DateTime dateOfSale)
        {
            double total = 0;
            total += CountyTaxRate(county, dateOfSale);
            total += CountyTransitTaxRate(county, dateOfSale);
            total += StateTaxRate;
            return total;
        }

        /// <summary>
        /// Return the Rate of tax that is paid for Transit Tax for a county
        /// </summary>
        /// <param name="county"></param>
        /// <returns></returns>
        public static double CountyTransitTaxRate(int county, DateTime dateOfSale)
        {
            var taxPeriod = TaxPeriods.GetPeriodByDate(dateOfSale);
            var rate = taxPeriod.GetCountyRateByCountyIndex(county);

            return rate.TransitTax;
        }

        /// <summary>
        /// Return the tax rate for a scpecific County for a specific Time
        /// E.g. 2.0 or 2.25 etc
        /// </summary>
        /// <param name="county"></param>
        /// <param name="dateOfSale"></param>
        public static double CountyTaxRate(int county, DateTime dateOfSale)
        {
            var taxPeriod = TaxPeriods.GetPeriodByDate(dateOfSale);
            var rate = taxPeriod.GetCountyRateByCountyIndex(county);

            return rate.TaxRate;
        }
    }
}
