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
    public class TaxContext
    {
        /// <summary>
        /// An array of DateTimes with the start of each new tax period
        /// </summary>
        public static DateTime[] TaxPeriods
        {
            get
            {
                DateTime[] periods = { 
                    new DateTime(2012, 4, 1), 
                    new DateTime(2012, 1, 1), 
                    new DateTime(2011, 10, 1), 
                    new DateTime(2011, 1, 1) 
                };

                return periods;
            }
        }

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
        /// The constant tax rate the that always goes to Transittax
        /// </summary>
        public static double TransitTaxRate
        {
            get
            {
                return .5;
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
            total += CountyTransitTaxRate(county);
            total += StateTaxRate;
            return total;
        }

        /// <summary>
        /// Return the Rate of tax that is paid for Transit Tax for a county
        /// </summary>
        /// <param name="county"></param>
        /// <returns></returns>
        public static double CountyTransitTaxRate(int county)
        {
            if (county == County.MECKLENBURG)
                return TransitTaxRate;
            else
                return 0;
        }

        /// <summary>
        /// Return the tax rate for a scpecific County for a specific Time
        /// E.g. 2.0 or 2.25 etc
        /// </summary>
        /// <param name="county"></param>
        /// <param name="dateOfSale"></param>
        public static double CountyTaxRate(int county, DateTime dateOfSale)
        {
            if (county < 1 || county > 101)
            {
                throw new ArgumentOutOfRangeException("County");
            }
            		
		    //Period beginning April 1, 2012
		    if(dateOfSale >= TaxPeriods[0]) {
			    if( county == County.ALEXANDER || 
				    county == County.BUNCOMBE || 
				    county == County.CARBARRUS || 
				    county == County.CATAWBA || 
				    county == County.CUMBERLAND || 
				    county == County.DUPLIN ||  
				    county == County.DURHAM || 
				    county == County.HALIFAX || 
				    county == County.HAYWOOD || 
				    county == County.HERTFORD || 
				    county == County.LEE || 
				    county == County.MARTIN || 
				    county == County.MONTGOMERY || 
				    county == County.NEW_HANOVER || 
				    county == County.ONSLOW || 
				    county == County.ORANGE || 
				    county == County.PITT || 
				    county == County.RANDOLPH || 
				    county == County.ROBESON || 
				    county == County.ROWAN ||  
				    county == County.SAMPSON ||  
				    county == County.SURRY ||  
				    county == County.WILKES ) {
				    return 2.25;
			    } 	
			
			    //All other counties
			    return 2.0;			
			
		    //Period beginning January 1, 2012
		    } else if(dateOfSale >= TaxPeriods[1]) {
			    if( county == County.ALEXANDER || 
				    county == County.CARBARRUS || 
				    county == County.CATAWBA || 
				    county == County.CUMBERLAND || 
				    county == County.DUPLIN || 
				    county == County.HALIFAX || 
				    county == County.HAYWOOD || 
				    county == County.HERTFORD || 
				    county == County.LEE || 
				    county == County.MARTIN || 
				    county == County.NEW_HANOVER || 
				    county == County.ONSLOW || 
				    county == County.PITT || 
				    county == County.RANDOLPH || 
				    county == County.ROBESON || 
				    county == County.ROWAN ||  
				    county == County.SAMPSON ||  
				    county == County.SURRY ||  
				    county == County.WILKES ) {
				    return 2.25;
			    } 		
			
			    //All other counties
			    return 2.0;
		
		    //Period beginning October 1, 2011
		    } else if(dateOfSale >= TaxPeriods[2]) {
			    if( county == County.ALEXANDER || 
				    county == County.CARBARRUS || 
				    county == County.CATAWBA || 
				    county == County.CUMBERLAND || 
				    county == County.DUPLIN || 
				    county == County.HAYWOOD || 
				    county == County.HERTFORD || 
				    county == County.LEE || 
				    county == County.MARTIN || 
				    county == County.NEW_HANOVER || 
				    county == County.ONSLOW || 
				    county == County.PITT || 
				    county == County.RANDOLPH || 
				    county == County.ROBESON || 
				    county == County.ROWAN ||  
				    county == County.SAMPSON ||  
				    county == County.SURRY ||  
				    county == County.WILKES ) {
				    return 2.25;
			    } 		
			
			    //All other counties
			    return 2.0;
			
		    //Period beginning January 1, 2011
            }
            else if (dateOfSale >= TaxPeriods[3])
            {
                if (county == County.ALEXANDER ||
                    county == County.CATAWBA ||
                    county == County.CUMBERLAND ||
                    county == County.DUPLIN ||
                    county == County.HAYWOOD ||
                    county == County.HERTFORD ||
                    county == County.LEE ||
                    county == County.MARTIN ||
                    county == County.NEW_HANOVER ||
                    county == County.ONSLOW ||
                    county == County.PITT ||
                    county == County.RANDOLPH ||
                    county == County.ROBESON ||
                    county == County.ROWAN ||
                    county == County.SAMPSON ||
                    county == County.SURRY ||
                    county == County.WILKES)
                {
                    return 2.25;
                }

                //All other counties
                return 2.0;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Date");
            }

            //We have NOOO Idea.... :/
            throw new Exception("Something is wrong here..");
        }

    }
}
