using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthCarolinaTaxRecoveryCalculator.Models.Data
{
    public class Log : TableEntity
    {
        public Log()
        {
            ID = Guid.NewGuid();
            Timestamp = DateTime.Now;

            this.RowKey = ID.ToString();
            this.PartitionKey = "North Carolina Tax Recovery Calculator";
        }

        public Guid ID { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
    }
}