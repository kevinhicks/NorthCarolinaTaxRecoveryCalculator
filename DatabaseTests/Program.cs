using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Dapper;
using DapperExtensions;
using NorthCarolinaTaxRecoveryCalculator.Models;

namespace DatabaseTests
{
    class Program
    {
        static void Main(string[] args)
        {
            new DatabaseThingy();
        }
    }

    class DatabaseThingy
    {
        private string connStr = "Data Source=.\\SQLEXPRESS;Initial Catalog=NCTaxCalcApp;Integrated Security=SSPI";

        public DatabaseThingy() {
            countReciepts();
            addReciept();
            countReciepts();
            
            Console.ReadKey();
        }

        private void countReciepts() {
            using(var conn = new SqlConnection(connStr)) {
                conn.Open();
                var reciepts = conn.Query<Reciept>("select * from reciepts");
                Console.WriteLine("Count: {0}", reciepts.Count());
                conn.Close();
            }
        }

        private void addReciept() {
            using(var conn = new SqlConnection(connStr)) {
                Reciept reciept = new Reciept();
                reciept.RIF = "3";
                reciept.StoreName = "Lowe's";
                reciept.DateOfSale = DateTime.Parse("2012-06-18");
                reciept.SalesTax = 16.76f;
                reciept.FoodTax = 0.00f;
                reciept.SalesAmount = 256.12f;
                reciept.County = 80 - 1;
                reciept.Notes = "";

                conn.Open();

                conn.Insert(reciept);

                conn.Close();
            }
        }        
    }
}
