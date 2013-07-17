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
    /// <summary>
    /// This singleton is used to load, and hold, all tax period 
    /// information for each county for each tax period.
    /// 
    /// It loads its information from the TaxPeriods.xml file.
    /// </summary>
    public static class TaxPeriods
    {
        /// <summary>
        /// Every tax period, with every county and its tax rated in 
        /// each period.
        /// </summary>
        public static List<TaxPeriod> Periods { get; private set; }

        static TaxPeriods()
        {
            //Load the data from teh XML file before anyone tries to access it
            Load();
        }

        /// <summary>
        /// Load all the tax rates, for all tax periods from 
        /// the TaxPeriods.xml file
        /// </summary>
        public static void Load()
        {
            //initialize/clear any previous information
            Periods = new List<TaxPeriod>();

            var settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;
            var xmlIn = XmlReader.Create( "/TaxPeriods.xml", settings);

            //get XML header stuff out of the way
            xmlIn.Read();
            xmlIn.ReadStartElement("TaxPeriods");

            //Read in every individual tax period
            while (xmlIn.Name == "TaxPeriod")
            {
                Periods.Add(readTaxPeriod(xmlIn));
            }

            //Close the file, for good measure
            xmlIn.Close();

            //Order the Tax Periods by date
            sortTaxPeriods();
        }

        /// <summary>
        /// Sort the tax periods by date.
        /// 
        /// It would be *nice* if we could be sure the XML 
        /// file is already sorted, but we want to make sure.
        /// 
        /// This will allow us to find the data by:
        /// if(recipet_date >= tax_period1_date)
        /// else if(recipet_date >= tax_period2_date)
        /// etc...
        /// </summary>
        private static void sortTaxPeriods()
        {
            Periods.Sort((x, y) => y.StartOfPeriod.CompareTo(x.StartOfPeriod));
        }

        /// <summary>
        /// Find which tax Period the specified date falls in
        /// </summary>
        /// <param name="when">The date in question</param>
        /// <returns>The Tax Period with all the countes, and their tax rates</returns>
        public static TaxPeriod GetPeriodByDate(DateTime when)
        {
            foreach (var period in Periods)
            {
                if (when.CompareTo(period.StartOfPeriod) >= 0)
                {
                    return period;
                }
            }

            return null;
        }

        /// <summary>
        /// Read in a single tax period from the xml file
        /// </summary>
        /// <param name="xmlIn"></param>
        private static TaxPeriod readTaxPeriod(XmlReader xmlIn)
        {
            //What we we later return
            TaxPeriod period = null;

            //The TaxPeriod element shoud have a 'starting' attribute
            if (xmlIn.HasAttributes)
            {
                xmlIn.MoveToNextAttribute();
                if (xmlIn.Name == "starting")
                {
                    //Create the new period with its starting date
                    period = new TaxPeriod(DateTime.Parse(xmlIn.Value));
                }

                xmlIn.MoveToElement();
            }

            //Give us a list to stick all these Counties in
            period.CountyRates = new List<CountyRate>();

            //Move on to the first County element. There should be ~100 of them
            xmlIn.ReadStartElement("TaxPeriod");

            //For every individual county     
            while (xmlIn.Name == "County")
            {
                //Create a new Rate for this county
                var rate = new CountyRate();

                //Tax rates are held in attributes of the County xml element
                if (xmlIn.HasAttributes)
                {
                    while (xmlIn.MoveToNextAttribute())
                    {
                        //It should have a TaxRate attribute
                        if (xmlIn.Name == "TaxRate")
                        {
                            rate.TaxRate = float.Parse(xmlIn.Value);
                        }
                        //It might have a Transit attribute
                        else if (xmlIn.Name == "Transit")
                        {
                            rate.TransitTax = float.Parse(xmlIn.Value);
                        }
                        //It should have a Name attribute
                        else if (xmlIn.Name == "Name")
                        {
                            rate.Name = xmlIn.Value;
                        }
                    }
                }

                //Save the new County to the list of Counties in this Tax Period
                period.CountyRates.Add(rate);
                xmlIn.MoveToElement();

                //We're done with this County, move on to the next
                xmlIn.ReadStartElement("County");

            }

            //This should eat the closing TaxPeriod element
            xmlIn.Read();

            //this is the new Tax Period with all its counties
            return period;
        }
    }

    /// <summary>
    /// This is used to hold information about a single tax period, 
    /// with all its county rates for that period
    /// </summary>
    public class TaxPeriod
    {
        /// <summary>
        /// This is when the tax period went into effect
        /// </summary>
        public DateTime StartOfPeriod { get; set; }

        /// <summary>
        /// All the rate for every county in this period
        /// </summary>
        public List<CountyRate> CountyRates { get; set; }

        public TaxPeriod(DateTime startOfPeriod)
        {
            this.StartOfPeriod = startOfPeriod;
            this.CountyRates = new List<CountyRate>();
        }

        /// <summary>
        /// Pretty printing
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string output = "Tax Period Starting: " + StartOfPeriod.ToShortDateString() + "\n";

            foreach (var taxrate in CountyRates)
            {
                output += taxrate.ToString();
            }

            return output;
        }

        /// <summary>
        /// Find the county rate by an County Index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public CountyRate GetCountyRateByCountyIndex(int index)
        {
            string name = County.Counties[index].Name.ToLower();
            foreach (var county in CountyRates)
            {
                if (county.Name.ToLower() == name)
                    return county;
            }

            return null;
        }
    }

    /// <summary>
    /// Hold all the information of all tax rates for a single county
    /// </summary>
    public class CountyRate
    {
        public string Name { get; set; }
        public float TaxRate { get; set; }
        public float TransitTax { get; set; }

        public CountyRate()
        {
            TaxRate = 0;
            TransitTax = 0;
        }

        /// <summary>
        /// Pretty printing
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string output = "\t" + Name + "\n";
            output += "\t\t" + TaxRate + "\n";
            output += "\t\t" + TransitTax + "\n";

            return output;
        }
    }
}
