using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthCarolinaTaxRecoveryCalculator;
using NorthCarolinaTaxRecoveryCalculator.Controllers;
using NorthCarolinaTaxRecoveryCalculator.Models;

namespace NorthCarolinaTaxRecoveryCalculator.Tests.Models
{
    
    public class TestProjectsData
    {
        public List<Project> Projects { get; set; }
        public List<Reciept> Reciepts { get; set; }

        private TestProjectsData() 
        {
            //Create the lists
            Projects = new List<Project>();
            Reciepts = new List<Reciept>();            

            //Create the data
            Project project = new Project();
            project.Name = "Salisbury - Old Concord Rd";
            project.DateStarted = DateTime.Now;
            project.IsDeleted = false;
            project.OwnerID = 0;
            Projects.Add(project);
            generateData(project);

            project = new Project();
            project.Name = "Another Identical Example Project";
            project.DateStarted = DateTime.Now;
            project.IsDeleted = false;
            project.OwnerID = 0;
            Projects.Add(project);
            generateData(project);

            project = new Project();
            project.Name = "Project with multiple tax periods";
            project.DateStarted = DateTime.Now;
            project.IsDeleted = false;
            project.OwnerID = 0;
            Projects.Add(project);
            generateMultiTaxPeriodData(project);

            project = new Project();
            project.Name = "Project to test every period boundry";
            project.DateStarted = DateTime.Now;
            project.IsDeleted = false;
            project.OwnerID = 0;
            Projects.Add(project);
            generateTaxDataToTestEveryTaxPeriodBoundry(project);
        }

        private static TestProjectsData _instance = null;
        public static TestProjectsData GetInstance()
        {
            if (_instance == null)
            {
                _instance = new TestProjectsData();
            }

            return _instance;
        }

        /// <summary>
        /// Generates the data from the previous database
        /// </summary>
        /// <param name="project"></param>
        private void generateData(Project project)
        {
            Reciept reciept = null;

            reciept = new Reciept();
            reciept.RIF = "1";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-06-16");
            reciept.SalesTax = 7.29f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 111.43f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "2";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-06-16");
            reciept.SalesTax = 1.91f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 29.13f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "3";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-06-18");
            reciept.SalesTax = 16.76f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 256.12f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "4";
            reciept.StoreName = "DESCO Inc.";
            reciept.DateOfSale = DateTime.Parse("2012-06-20");
            reciept.SalesTax = 5.36f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 82.00f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "5";
            reciept.StoreName = "KnoxBox";
            reciept.DateOfSale = DateTime.Parse("2012-07-05");
            reciept.SalesTax = 27.68f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 437.68f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "6";
            reciept.StoreName = "Quickscrews International";
            reciept.DateOfSale = DateTime.Parse("2012-06-22");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 110.76f;
            reciept.County = 101 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "7";
            reciept.StoreName = "ABC Supply Company";
            reciept.DateOfSale = DateTime.Parse("2012-06-22");
            reciept.SalesTax = 248.83f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 3803.55f;
            reciept.County = 60 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "8";
            reciept.StoreName = "Concrete Supply Company";
            reciept.DateOfSale = DateTime.Parse("2012-07-03");
            reciept.SalesTax = 191.52f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 2927.52f;
            reciept.County = 60 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "9";
            reciept.StoreName = "Concrete Supply Company";
            reciept.DateOfSale = DateTime.Parse("2012-07-07");
            reciept.SalesTax = 60.27f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 921.27f;
            reciept.County = 60 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "10";
            reciept.StoreName = "Concrete Supply Company";
            reciept.DateOfSale = DateTime.Parse("2012-07-07");
            reciept.SalesTax = 1620.21f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 24762.21f;
            reciept.County = 60 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "11";
            reciept.StoreName = "Guaranteed Supply Company";
            reciept.DateOfSale = DateTime.Parse("2012-07-05");
            reciept.SalesTax = 79.38f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 1213.38f;
            reciept.County = 41 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "12";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-07-02");
            reciept.SalesTax = 3.57f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 54.57f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "13";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-07-02");
            reciept.SalesTax = 12.29f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 187.87f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "14";
            reciept.StoreName = "United Rentals";
            reciept.DateOfSale = DateTime.Parse("2012-07-03");
            reciept.SalesTax = 18.71f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 293.09f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "15";
            reciept.StoreName = "United Rentals";
            reciept.DateOfSale = DateTime.Parse("2012-07-13");
            reciept.SalesTax = 3.81f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 65.29f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "16";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-07-13");
            reciept.SalesTax = 10.69f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 163.38f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "17";
            reciept.StoreName = "United Rentals";
            reciept.DateOfSale = DateTime.Parse("2012-07-16");
            reciept.SalesTax = 21.11f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 322.71f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "18";
            reciept.StoreName = "Concrete Supply Company";
            reciept.DateOfSale = DateTime.Parse("2012-07-14");
            reciept.SalesTax = 1972.96f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 30157.96f;
            reciept.County = 60 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "19";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-07-14");
            reciept.SalesTax = 13.48f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 206.08f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "20";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-07-21");
            reciept.SalesTax = 11.43f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 174.77f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "21";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-07-21");
            reciept.SalesTax = 11.91f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 182.10f;
            reciept.County = 13 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "22";
            reciept.StoreName = "Hoffman & Hoffman";
            reciept.DateOfSale = DateTime.Parse("2012-07-18");
            reciept.SalesTax = 625.45f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 9560.45f;
            reciept.County = 41 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "23";
            reciept.StoreName = "Office Depot";
            reciept.DateOfSale = DateTime.Parse("2012-07-11");
            reciept.SalesTax = 0.80f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 11.79f;
            reciept.County = 60 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "24";
            reciept.StoreName = "Suburban Propane";
            reciept.DateOfSale = DateTime.Parse("2012-07-10");
            reciept.SalesTax = 107.92f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 1634.52f;
            reciept.County = 101 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "25";
            reciept.StoreName = "Concrete Supply Company";
            reciept.DateOfSale = DateTime.Parse("2012-06-28");
            reciept.SalesTax = 33.22f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 507.72f;
            reciept.County = 60 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "26";
            reciept.StoreName = "Concrete Supply Company";
            reciept.DateOfSale = DateTime.Parse("2012-06-28");
            reciept.SalesTax = 357.21f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 5460.24f;
            reciept.County = 60 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "27";
            reciept.StoreName = "Concrete Supply Company";
            reciept.DateOfSale = DateTime.Parse("2012-07-02");
            reciept.SalesTax = 50.44f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 720.64f;
            reciept.County = 60 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "28";
            reciept.StoreName = "Concrete Supply Company";
            reciept.DateOfSale = DateTime.Parse("2012-07-21");
            reciept.SalesTax = 987.85f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 15100.00f;
            reciept.County = 60 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "29";
            reciept.StoreName = "DESCO Inc.";
            reciept.DateOfSale = DateTime.Parse("2012-07-20");
            reciept.SalesTax = 0.65f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 9.87f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "30";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-07-20");
            reciept.SalesTax = 16.17f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 247.15f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "31";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-11");
            reciept.SalesTax = 13.15f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 201.01f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "32";
            reciept.StoreName = "Sheetz";
            reciept.DateOfSale = DateTime.Parse("2012-06-30");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 56.59f;
            reciept.County = 101 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "33";
            reciept.StoreName = "Office Depot";
            reciept.DateOfSale = DateTime.Parse("2012-08-11");
            reciept.SalesTax = 20.16f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 308.12f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "34";
            reciept.StoreName = "Office Depot";
            reciept.DateOfSale = DateTime.Parse("2012-08-11");
            reciept.SalesTax = -16.31f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = -249.27f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "35";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-10");
            reciept.SalesTax = 1.40f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 21.38f;
            reciept.County = 13 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "36";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-10");
            reciept.SalesTax = 1.77f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 27.11f;
            reciept.County = 13 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "37";
            reciept.StoreName = "Sam's Club";
            reciept.DateOfSale = DateTime.Parse("2012-08-10");
            reciept.SalesTax = 25.88f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 392.56f;
            reciept.County = 13 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "38";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-10");
            reciept.SalesTax = 8.01f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 122.37f;
            reciept.County = 13 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "39";
            reciept.StoreName = "Office Depot";
            reciept.DateOfSale = DateTime.Parse("2012-08-11");
            reciept.SalesTax = 16.31f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 249.27f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "40";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-11");
            reciept.SalesTax = 31.54f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 482.15f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "41";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-11");
            reciept.SalesTax = 1.94f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 29.66f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "42";
            reciept.StoreName = "Sherwin-Williams";
            reciept.DateOfSale = DateTime.Parse("2012-08-11");
            reciept.SalesTax = 26.45f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 404.35f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "43";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-12");
            reciept.SalesTax = 10.62f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 162.27f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "44";
            reciept.StoreName = "Walmart";
            reciept.DateOfSale = DateTime.Parse("2012-08-12");
            reciept.SalesTax = 0.69f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 10.57f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "45";
            reciept.StoreName = "Sherwin-Williams";
            reciept.DateOfSale = DateTime.Parse("2012-08-12");
            reciept.SalesTax = 10.58f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 161.74f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "46";
            reciept.StoreName = "Sheetz";
            reciept.DateOfSale = DateTime.Parse("2012-08-12");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 30.00f;
            reciept.County = 101 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "47";
            reciept.StoreName = "Sheetz";
            reciept.DateOfSale = DateTime.Parse("2012-08-12");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 67.21f;
            reciept.County = 101 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "48";
            reciept.StoreName = "Office Depot";
            reciept.DateOfSale = DateTime.Parse("2012-08-12");
            reciept.SalesTax = 0.59f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 8.97f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "49";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-12");
            reciept.SalesTax = 23.92f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 365.62f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "50";
            reciept.StoreName = "Walmart";
            reciept.DateOfSale = DateTime.Parse("2012-08-12");
            reciept.SalesTax = 2.76f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 42.16f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "51";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-14");
            reciept.SalesTax = 10.33f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 157.83f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "52";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-12");
            reciept.SalesTax = 0.76f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 11.60f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "53";
            reciept.StoreName = "Southeastern Concrete Products";
            reciept.DateOfSale = DateTime.Parse("2012-08-13");
            reciept.SalesTax = 14.89f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 235.54f;
            reciept.County = 49 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "54";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-17");
            reciept.SalesTax = 3.38f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 51.60f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "55";
            reciept.StoreName = "Earp's";
            reciept.DateOfSale = DateTime.Parse("2012-08-17");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 45.67f;
            reciept.County = 101 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "56";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-17");
            reciept.SalesTax = 9.84f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 150.42f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "57";
            reciept.StoreName = "Home Depot";
            reciept.DateOfSale = DateTime.Parse("2012-08-17");
            reciept.SalesTax = 6.06f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 92.63f;
            reciept.County = 13 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "58";
            reciept.StoreName = "DESCO Inc.";
            reciept.DateOfSale = DateTime.Parse("2012-08-17");
            reciept.SalesTax = 13.66f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 208.78f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "59";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-13");
            reciept.SalesTax = 1.49f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 25.36f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "60";
            reciept.StoreName = "Sheetz";
            reciept.DateOfSale = DateTime.Parse("2012-08-11");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 25.04f;
            reciept.County = 101 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "61";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-13");
            reciept.SalesTax = 3.83f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 58.53f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "62";
            reciept.StoreName = "Walmart";
            reciept.DateOfSale = DateTime.Parse("2012-08-13");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 0.78f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 39.98f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "63";
            reciept.StoreName = "Walmart";
            reciept.DateOfSale = DateTime.Parse("2012-08-17");
            reciept.SalesTax = 1.57f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 24.02f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "64";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-16");
            reciept.SalesTax = 8.56f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 130.89f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "65";
            reciept.StoreName = "Walmart";
            reciept.DateOfSale = DateTime.Parse("2012-08-16");
            reciept.SalesTax = 0.75f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 11.51f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "66";
            reciept.StoreName = "WilcoHess";
            reciept.DateOfSale = DateTime.Parse("2012-08-15");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 24.99f;
            reciept.County = 101 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "67";
            reciept.StoreName = "Sheetz";
            reciept.DateOfSale = DateTime.Parse("2012-08-16");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 68.83f;
            reciept.County = 101 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "68";
            reciept.StoreName = "Walmart";
            reciept.DateOfSale = DateTime.Parse("2012-08-16");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 1.85f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 94.45f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "69";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-17");
            reciept.SalesTax = 10.70f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 163.57f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "70";
            reciept.StoreName = "Sheetz";
            reciept.DateOfSale = DateTime.Parse("2012-08-16");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 55.28f;
            reciept.County = 101 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "71";
            reciept.StoreName = "Walmart";
            reciept.DateOfSale = DateTime.Parse("2012-08-16");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 0.80f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 40.60f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "72";
            reciept.StoreName = "Sheetz";
            reciept.DateOfSale = DateTime.Parse("2012-08-16");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 29.17f;
            reciept.County = 101 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "73";
            reciept.StoreName = "United Rentals";
            reciept.DateOfSale = DateTime.Parse("2012-08-15");
            reciept.SalesTax = 6.64f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 105.11f;
            reciept.County = 49 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "74";
            reciept.StoreName = "Pop Shoppe";
            reciept.DateOfSale = DateTime.Parse("2012-08-15");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 62.24f;
            reciept.County = 101 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "75";
            reciept.StoreName = "Swicegoods Paper";
            reciept.DateOfSale = DateTime.Parse("2012-08-15");
            reciept.SalesTax = 6.78f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 103.58f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "76";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-15");
            reciept.SalesTax = 11.44f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 174.89f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "77";
            reciept.StoreName = "Fastenal";
            reciept.DateOfSale = DateTime.Parse("2012-08-15");
            reciept.SalesTax = 9.61f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 146.89f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "78";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-14");
            reciept.SalesTax = 4.30f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 65.74f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "79";
            reciept.StoreName = "Walmart";
            reciept.DateOfSale = DateTime.Parse("2012-08-14");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 0.39f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 19.99f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "80";
            reciept.StoreName = "Food Lion";
            reciept.DateOfSale = DateTime.Parse("2012-08-14");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 0.86f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 43.76f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "81";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-14");
            reciept.SalesTax = 4.16f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 63.59f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "82";
            reciept.StoreName = "Earp's";
            reciept.DateOfSale = DateTime.Parse("2012-08-14");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 44.31f;
            reciept.County = 101 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "83";
            reciept.StoreName = "Pops Country Store";
            reciept.DateOfSale = DateTime.Parse("2012-08-11");
            reciept.SalesTax = 5.60f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 85.55f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "84";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-12");
            reciept.SalesTax = 21.03f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 321.41f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "85";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-13");
            reciept.SalesTax = 10.82f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 165.40f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "86";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-12");
            reciept.SalesTax = -23.92f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = -365.62f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "87";
            reciept.StoreName = "Food Lion";
            reciept.DateOfSale = DateTime.Parse("2012-08-18");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 144.05f;
            reciept.County = 101 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "88";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-17");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 2.24f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 114.14f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "89";
            reciept.StoreName = "Sheetz";
            reciept.DateOfSale = DateTime.Parse("2012-08-18");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 58.78f;
            reciept.County = 101 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "90";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-18");
            reciept.SalesTax = 7.11f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 108.63f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "91";
            reciept.StoreName = "Concrete Supply Company";
            reciept.DateOfSale = DateTime.Parse("2012-07-21");
            reciept.SalesTax = 562.83f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 8599.68f;
            reciept.County = 60 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "92";
            reciept.StoreName = "GrandinRoad";
            reciept.DateOfSale = DateTime.Parse("2012-08-11");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 373.32f;
            reciept.County = 101 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "93";
            reciept.StoreName = "Sunbelt Rentals";
            reciept.DateOfSale = DateTime.Parse("2012-08-14");
            reciept.SalesTax = 71.61f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 1094.61f;
            reciept.County = 13 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "94";
            reciept.StoreName = "Thermo King Central";
            reciept.DateOfSale = DateTime.Parse("2012-07-14");
            reciept.SalesTax = 12.20f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 180.47f;
            reciept.County = 60 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "95";
            reciept.StoreName = "Hertz Equipment Rental";
            reciept.DateOfSale = DateTime.Parse("2012-08-16");
            reciept.SalesTax = 28.07f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 429.07f;
            reciept.County = 60 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "96";
            reciept.StoreName = "WeGotLites";
            reciept.DateOfSale = DateTime.Parse("2012-07-23");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 678.00f;
            reciept.County = 101 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "97";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-18");
            reciept.SalesTax = -9.15f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = -139.81f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "98";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-19");
            reciept.SalesTax = -10.10f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = -154.40f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "99";
            reciept.StoreName = "Sheetz";
            reciept.DateOfSale = DateTime.Parse("2012-08-17");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 50.31f;
            reciept.County = 101 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "100";
            reciept.StoreName = "Sheetz";
            reciept.DateOfSale = DateTime.Parse("2012-08-17");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 17.95f;
            reciept.County = 101 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "101";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-19");
            reciept.SalesTax = 1.82f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 27.79f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "102";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-18");
            reciept.SalesTax = 4.66f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 71.23f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "103";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-18");
            reciept.SalesTax = 9.15f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 139.81f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "104";
            reciept.StoreName = "Sunnyside Ice and Fuel Co., Inc";
            reciept.DateOfSale = DateTime.Parse("2012-08-14");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 930.00f;
            reciept.County = 101 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "105";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-18");
            reciept.SalesTax = 9.15f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 139.81f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "106";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-18");
            reciept.SalesTax = 0.65f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 9.87f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "107";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-19");
            reciept.SalesTax = 12.88f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 196.95f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "108";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-19");
            reciept.SalesTax = 3.67f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 77.43f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "109";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-19");
            reciept.SalesTax = 12.12f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 185.28f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "110";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-18");
            reciept.SalesTax = 7.37f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 112.71f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "111";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-18");
            reciept.SalesTax = 2.71f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 41.41f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "112";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-18");
            reciept.SalesTax = 13.27f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 202.87f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "113";
            reciept.StoreName = "Walmart";
            reciept.DateOfSale = DateTime.Parse("2012-08-18");
            reciept.SalesTax = 0.92f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 14.04f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "114";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-18");
            reciept.SalesTax = 2.67f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 40.80f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "115";
            reciept.StoreName = "Target.com";
            reciept.DateOfSale = DateTime.Parse("2012-08-24");
            reciept.SalesTax = 6.06f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 92.58f;
            reciept.County = 34 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "116";
            reciept.StoreName = "Home Depot";
            reciept.DateOfSale = DateTime.Parse("2012-08-19");
            reciept.SalesTax = 1.40f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 21.36f;
            reciept.County = 13 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "117";
            reciept.StoreName = "Home Depot";
            reciept.DateOfSale = DateTime.Parse("2012-08-19");
            reciept.SalesTax = 3.24f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 49.56f;
            reciept.County = 13 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "118";
            reciept.StoreName = "Marathon";
            reciept.DateOfSale = DateTime.Parse("2012-08-19");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 34.23f;
            reciept.County = 101 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "119";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-19");
            reciept.SalesTax = 2.22f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 33.91f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "120";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-20");
            reciept.SalesTax = 25.29f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 386.57f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "121";
            reciept.StoreName = "Subway";
            reciept.DateOfSale = DateTime.Parse("2012-08-20");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 36.27f;
            reciept.County = 101 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "122";
            reciept.StoreName = "Chckfila";
            reciept.DateOfSale = DateTime.Parse("2012-08-20");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 7.37f;
            reciept.County = 101 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "123";
            reciept.StoreName = "Fastenal";
            reciept.DateOfSale = DateTime.Parse("2012-08-20");
            reciept.SalesTax = 3.84f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 58.75f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "124";
            reciept.StoreName = "Ruehlen Supply Co";
            reciept.DateOfSale = DateTime.Parse("2012-08-20");
            reciept.SalesTax = 2.39f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 36.57f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "125";
            reciept.StoreName = "Derrick Travel Plaza";
            reciept.DateOfSale = DateTime.Parse("2012-08-21");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 30.01f;
            reciept.County = 101 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "126";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-21");
            reciept.SalesTax = 1.04f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 15.94f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "127";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-22");
            reciept.SalesTax = 2.64f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 40.38f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "128";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-22");
            reciept.SalesTax = 1.39f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 21.31f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "129";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-22");
            reciept.SalesTax = 0.73f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 11.11f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "130";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-23");
            reciept.SalesTax = 5.44f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 83.20f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "131";
            reciept.StoreName = "Staples";
            reciept.DateOfSale = DateTime.Parse("2012-08-23");
            reciept.SalesTax = 4.90f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 74.88f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "132";
            reciept.StoreName = "Staples";
            reciept.DateOfSale = DateTime.Parse("2012-08-23");
            reciept.SalesTax = 2.13f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 30.49f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "133";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-23");
            reciept.SalesTax = 6.69f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 102.33f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "134";
            reciept.StoreName = "Rushco Food";
            reciept.DateOfSale = DateTime.Parse("2012-08-23");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 39.52f;
            reciept.County = 101 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "135";
            reciept.StoreName = "WilcoHess";
            reciept.DateOfSale = DateTime.Parse("2012-08-23");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 75.00f;
            reciept.County = 101 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "136";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-22");
            reciept.SalesTax = -0.46f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = -7.07f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "137";
            reciept.StoreName = "Sheetz";
            reciept.DateOfSale = DateTime.Parse("2012-08-24");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 50.37f;
            reciept.County = 101 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "138";
            reciept.StoreName = "Sheetz";
            reciept.DateOfSale = DateTime.Parse("2012-08-24");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 36.01f;
            reciept.County = 101 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "139";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-24");
            reciept.SalesTax = 1.81f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 27.69f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "140";
            reciept.StoreName = "Sherwin-Williams";
            reciept.DateOfSale = DateTime.Parse("2012-08-23");
            reciept.SalesTax = 19.99f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 305.51f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "141";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-19");
            reciept.SalesTax = 6.96f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 106.33f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "142";
            reciept.StoreName = "Sam's Club";
            reciept.DateOfSale = DateTime.Parse("2012-08-18");
            reciept.SalesTax = 12.04f;
            reciept.FoodTax = 2.46f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 307.29f;
            reciept.County = 13 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "143";
            reciept.StoreName = "Walmart";
            reciept.DateOfSale = DateTime.Parse("2012-08-17");
            reciept.SalesTax = 2.94f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 150.02f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "144";
            reciept.StoreName = "Walmart";
            reciept.DateOfSale = DateTime.Parse("2012-08-14");
            reciept.SalesTax = 1.33f;
            reciept.FoodTax = 1.51f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 97.19f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "145";
            reciept.StoreName = "Sam's Club";
            reciept.DateOfSale = DateTime.Parse("2012-08-15");
            reciept.SalesTax = 2.16f;
            reciept.FoodTax = 0.78f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 72.92f;
            reciept.County = 13 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "146";
            reciept.StoreName = "Rastaurant Depot";
            reciept.DateOfSale = DateTime.Parse("2012-08-13");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 138.26f;
            reciept.County = 101 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "147";
            reciept.StoreName = "WilcoHess";
            reciept.DateOfSale = DateTime.Parse("2012-08-18");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 1.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 49.80f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "148";
            reciept.StoreName = "Auto Zone";
            reciept.DateOfSale = DateTime.Parse("2012-08-14");
            reciept.SalesTax = 1.36f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 20.83f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "149";
            reciept.StoreName = "Walmart";
            reciept.DateOfSale = DateTime.Parse("2012-08-18");
            reciept.SalesTax = 1.94f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 29.64f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "150";
            reciept.StoreName = "Walmart";
            reciept.DateOfSale = DateTime.Parse("2012-08-18");
            reciept.SalesTax = 0.83f;
            reciept.FoodTax = 0.43f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 34.80f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "151";
            reciept.StoreName = "Walmart";
            reciept.DateOfSale = DateTime.Parse("2012-08-19");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 2.04f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 103.80f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "152";
            reciept.StoreName = "Walmart";
            reciept.DateOfSale = DateTime.Parse("2012-08-17");
            reciept.SalesTax = 1.52f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 77.58f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "153";
            reciept.StoreName = "Walmart";
            reciept.DateOfSale = DateTime.Parse("2012-08-18");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 1.06f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 53.96f;
            reciept.County = 13 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "154";
            reciept.StoreName = "Sandy's One Stop";
            reciept.DateOfSale = DateTime.Parse("2012-08-19");
            reciept.SalesTax = 0.40f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 20.30f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "155";
            reciept.StoreName = "Subway";
            reciept.DateOfSale = DateTime.Parse("2012-08-21");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 65.81f;
            reciept.County = 101 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "156";
            reciept.StoreName = "Sunnyside Ice and Fuel Co., Inc";
            reciept.DateOfSale = DateTime.Parse("2012-08-18");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 69.50f;
            reciept.County = 101 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "157";
            reciept.StoreName = "Sam's Club";
            reciept.DateOfSale = DateTime.Parse("2012-08-13");
            reciept.SalesTax = 4.83f;
            reciept.FoodTax = 5.80f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 369.91f;
            reciept.County = 13 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "158";
            reciept.StoreName = "Trane";
            reciept.DateOfSale = DateTime.Parse("2012-08-10");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 16481.00f;
            reciept.County = 101 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "159";
            reciept.StoreName = "The Glass Shop";
            reciept.DateOfSale = DateTime.Parse("2012-07-16");
            reciept.SalesTax = 0.00f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 1752.00f;
            reciept.County = 101 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "160";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-24");
            reciept.SalesTax = 3.57f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 54.64f;
            reciept.County = 13 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "161";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-23");
            reciept.SalesTax = 3.53f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 53.90f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);

            reciept = new Reciept();
            reciept.RIF = "162";
            reciept.StoreName = "Lowe's";
            reciept.DateOfSale = DateTime.Parse("2012-08-24");
            reciept.SalesTax = 0.93f;
            reciept.FoodTax = 0.00f;
            reciept.ProjectID = project.ID;
            reciept.Project = project;
            reciept.SalesAmount = 14.15f;
            reciept.County = 80 - 1;
            reciept.Notes = "";
            Reciepts.Add(reciept);


        }

        /// <summary>
        /// Generates just a few records that span multiple tax periods
        /// </summary>
        /// <param name="project"></param>
        private void generateMultiTaxPeriodData(Project project)
        {
            Reciept reciept;

            for (int i = 0; i < TaxContext.TaxPeriods.Count(); i++)
            {
                reciept = new Reciept();
                reciept.RIF = "" + i;
                reciept.StoreName = "Lowe's";
                reciept.DateOfSale = TaxContext.TaxPeriods[i].AddDays(1);
                reciept.SalesTax = 100f;
                reciept.FoodTax = 100f;
                reciept.ProjectID = project.ID;
                reciept.Project = project;
                reciept.SalesAmount = 100f;
                reciept.County = County.DURHAM;
                reciept.Notes = "";
                Reciepts.Add(reciept);
            }
        }
        /// <summary>
        /// Generates records for every county for $1000 before, and after every tax period boundry for every county
        /// </summary>
        /// <param name="project"></param>
        private void generateTaxDataToTestEveryTaxPeriodBoundry(Project project)
        {
            Reciept reciept;

            //For every county
            for (int county = 1; county <= 101; county++)
            {
                //Add a reciept BEFORE and AFTER the tax period
                for (int period = 0; period < TaxContext.TaxPeriods.Count(); period++)
                {
                    //One Before
                    reciept = new Reciept();
                    reciept.RIF = county + "" + period + "" + 1;
                    reciept.StoreName = "Lowe's";
                    reciept.DateOfSale = TaxContext.TaxPeriods[period].AddMinutes(-1);
                    reciept.SalesTax = 1000f;
                    reciept.FoodTax = 1000f;
                    reciept.ProjectID = project.ID;
                    reciept.Project = project;
                    reciept.SalesAmount = 1000f;
                    reciept.County = county;
                    reciept.Notes = "";

                    //Dont add a reciept that is BEFORE the oldest tax period
                    if (period != TaxContext.TaxPeriods.Count() - 1)
                    {
                        Reciepts.Add(reciept);
                    }

                    //One After
                    reciept = new Reciept();
                    reciept.RIF = county + "" + period + "" + 2;
                    reciept.StoreName = "Lowe's";
                    reciept.DateOfSale = TaxContext.TaxPeriods[period].AddMinutes(1);
                    reciept.SalesTax = 1000f;
                    reciept.FoodTax = 1000f;
                    reciept.ProjectID = project.ID;
                    reciept.Project = project;
                    reciept.SalesAmount = 1000f;
                    reciept.County = county;
                    reciept.Notes = "";
                    Reciepts.Add(reciept);                    
                }
            }            
        }
    }
}
