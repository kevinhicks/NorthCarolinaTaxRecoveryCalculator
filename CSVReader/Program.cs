using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic.FileIO;

namespace CSVReader
{
    class Program
    {
        static void Main(string[] args)
        {
            var reader = new TextFieldParser("C:\\sql2.sql");
            reader.TextFieldType = FieldType.Delimited;

            string[] delimeters = { "," };
            reader.Delimiters = delimeters;

            Console.WriteLine("public class DataInit");
            Console.WriteLine("{");
            Console.WriteLine("public void Seed()");
            Console.WriteLine("{");
            
            Console.WriteLine();
            Console.WriteLine("ApplicationDBContext db = new ApplicationDBContext();");
            Console.WriteLine("Project project = new Project();");
            Console.WriteLine("project.Name = \"Salisbury - Old Concord Rd\";");
            Console.WriteLine("project.DateStarted = DateTime.Now;");
            Console.WriteLine("project.IsDeleted = false;");
            Console.WriteLine("project.OwnerID = 0;");
            Console.WriteLine("db.Projects.Add(project);");
            Console.WriteLine("db.SaveChanges();");

            Console.WriteLine();
            Console.WriteLine("Reciept reciept = null;");

            while (!reader.EndOfData)
            {
                try
                {
                    string[] row = reader.ReadFields();

                    Console.WriteLine();
                    //reciept = new Reciept();
                    Console.WriteLine("reciept = new Reciept();");
                    //reciept.RIF = ?;
                    Console.WriteLine("reciept.RIF = \"" + row[1] + "\";");
                    //reciept.StoreName = ?;
                    Console.WriteLine("reciept.StoreName = \"" + row[2] + "\";");
                    //reciept.Date = ?;
                    Console.WriteLine("reciept.DateOfSale = DateTime.Parse(\"" + row[3] + "\");");
                    //reciept.SalesTax = ?;
                    Console.WriteLine("reciept.SalesTax = " + row[4] + "f;");
                    //reciept.FoodTax = ?;
                    Console.WriteLine("reciept.FoodTax = " + row[5] + "f;");
                    //reciept.ProjectID = ?;
                    Console.WriteLine("reciept.ProjectID = project.ID;");
                    //reciept.SalesAmount = ?;
                    Console.WriteLine("reciept.SalesAmount = " + row[7] + "f;");
                    //reciept.County = ?;
                    Console.WriteLine("reciept.County = " + row[8] + " - 1;");
                    //reciept.Notes = ?;
                    Console.WriteLine("reciept.Notes = \"\";");
                    //db.Reciepts.Add(reciept);
                    Console.WriteLine("db.Reciepts.Add(reciept);");
                }

                catch (MalformedLineException e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }
            Console.WriteLine("db.SaveChanges();");
            Console.WriteLine("}\n}");

            //Console.ReadKey();
        }
    }
}
