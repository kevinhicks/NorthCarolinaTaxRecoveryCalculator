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
    public class TaxPeriods
    {
        /// <summary>
        /// Return an array of DateTimes with the start of each new tax period
        /// </summary>
        /// <returns></returns>
        public DateTime[] GetTaxPeriods()
        {
            DateTime[] periods = { 
                    new DateTime(2012, 4, 4), 
                    new DateTime(2012, 1, 1), 
                    new DateTime(2011, 10, 1), 
                    new DateTime(2011, 1, 1) 
            };

            return periods;
        }

        /// <summary>
        /// Return the tax rate for a scpecific County for a specific Time
        /// E.g. 2.0 or 2.25 etc
        /// </summary>
        /// <param name="county"></param>
        /// <param name="DateOfSale"></param>
        public double getTaxRate(int county, DateTime DateOfSale)
        {
            if (county < 1 || county > 101)
            {
                throw new ArgumentOutOfRangeException("County");
            }

            DateTime[] periods = GetTaxPeriods();
		
		    //Period beginning April 1, 2012
		    if(DateOfSale >= periods[0]) {
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
		    } else if(DateOfSale >= periods[1]) {
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
		    } else if(DateOfSale >= periods[2]) {
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
		    } else if(DateOfSale >= periods[3]) {
			    if( county == County.ALEXANDER || 
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
		    }

            //We have NOOO Idea.... :/
            throw new Exception("Something is wrong here..");
        }

        
    }
}
