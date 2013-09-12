using NorthCarolinaTaxRecoveryCalculator.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthCarolinaTaxRecoveryCalculator.Models.Service
{
    public interface IRepositoryRepository
    {
        RecieptEntity FindRecieptByID(Guid RecieptID);
        IEnumerable<RecieptEntity> FindAllRecieptsForProjectByID(Guid ProjectID, DateTime? filterStartDate = null, DateTime? filterEndDate = null);

        void Create(RecieptEntity reciept);
        void Update(RecieptEntity reciept);
        void Delete(RecieptEntity reciept);
        void InsertOrUpdate(RecieptEntity reciept);
    }

    /// <summary>
    /// Handles all data acess for all Projects
    /// </summary>
    public class RecieptRepository : IRepositoryRepository
    {
        private ApplicationDBContext db;
        public RecieptRepository()
        {
            db = new ApplicationDBContext();
        }

        public RecieptEntity FindRecieptByID(Guid RecieptID)
        {
            return db.Reciepts.Find(RecieptID);
        }

        public IEnumerable<RecieptEntity> FindAllRecieptsForProjectByID(Guid ProjectID, DateTime? filterStartDate = null, DateTime? filterEndDate = null)
        {
            throw new NotImplementedException();
        }

        public void Create(RecieptEntity reciept)
        {
            db.Reciepts.Add(reciept);
            db.SaveChanges();
        }

        public void Update(RecieptEntity reciept)
        {
            throw new NotImplementedException();
        }

        public void Delete(RecieptEntity reciepts)
        {
            throw new NotImplementedException();
        }


        public void InsertOrUpdate(RecieptEntity reciept)
        {
            //is this a new entry?
            if (db.Entry(reciept).State == System.Data.EntityState.Detached)
            {
                Create(reciept);
            }
            //this must be an existing entry, we shoudl update it
            else
            {
                Update(reciept);
            }

            return;
        }
    }

}