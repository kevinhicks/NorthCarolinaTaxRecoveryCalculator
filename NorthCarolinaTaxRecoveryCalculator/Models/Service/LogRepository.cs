using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using NorthCarolinaTaxRecoveryCalculator.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace NorthCarolinaTaxRecoveryCalculator.Models.Service
{
    /// <summary>
    /// Describes what any User Repository must have
    /// </summary>
    public interface ILogRepository : IBaseRepository<Log, Guid>    
    {
        /// <summary>
        /// Return ALL the Log entries for this application
        /// </summary>
        /// <returns></returns>
        IEnumerable<Log> FindAll();        
    }

    /// <summary>
    /// Save a long in azure table storage
    /// </summary>
    public class AzureLogRepository : ILogRepository
    {
        private CloudStorageAccount storageAccount;
        private CloudTableClient tableClient;
        private CloudTable table;

#if DEBUG
        private readonly string tableName = "TESTNorthCarolinaTaxRecoveryLogs";
#else
        private readonly string tableName = "NorthCarolinaTaxRecoveryLogs";
#endif
        
        public AzureLogRepository()
        {
#if DEBUG
            storageAccount = CloudStorageAccount.DevelopmentStorageAccount;
#else
            storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("AZURE_STORAGE_ACCOUNT"));
#endif
            tableClient = storageAccount.CreateCloudTableClient();
            table = tableClient.GetTableReference(tableName);
            table.CreateIfNotExists();
        }

        public Log FindByID(Guid ID)
        {
            throw new NotImplementedException();
        }

        public void Update(Log obj)
        {
            throw new NotImplementedException();
        }

        public void Create(Log obj)
        {
            var insertOperation = TableOperation.Insert(obj);
            table.Execute(insertOperation);
        }

        public void Delete(Log obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Log> FindAll()
        {
           var query = new TableQuery<Log>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "North Carolina Tax Recovery Calculator"));

           return table.ExecuteQuery(query);
        }
    }
}