﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalR.Hubs;
using NorthCarolinaTaxRecoveryCalculator.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace NorthCarolinaTaxRecoveryCalculator.Hubs
{

    [HubName("RecieptHub")]
    public class RecieptHub : Hub
    {

        ApplicationDBContext db = new ApplicationDBContext();

        /// <summary>
        /// Add a new recipet to the database, unless its invalid.
        /// If it is invlaid, then report the validation errors to the user,
        /// so that they can fix them.
        /// </summary>
        /// <param name="reciept"></param>
        public void AddReciept(Reciept reciept)
        {

            var context = new ValidationContext(reciept, null, null);
            var validationErrors = new List<ValidationResult>();

            //Is it already in the database?
            var reciepts = db.Reciepts;
            int queryForOriginal = reciepts.Where(rec =>
                rec.RIF == reciept.RIF &&
                rec.ProjectID == reciept.ProjectID).Count();
            bool alreadyExists = (queryForOriginal > 0);

            //If it is valid, then save it to the DB, and alert all the clients of a new reocrd
            if (Validator.TryValidateObject(reciept, context, validationErrors) && !alreadyExists)
            {
                //Save it
                db.Reciepts.Add(reciept);
                db.SaveChanges();

                //ALL clients should get the new record
                Clients.RecieveReciept(reciept);

                //Let the client who just submitted the information know that it was succesful
                Caller.OnNewRecieptSaved();
            }
            //Otherwise, report all validation errors
            else
            {
                //If the RIF already existed in the DB, the add it to the list of validation errors
                if (alreadyExists)
                {
                    var memberNames = new List<String>();
                    memberNames.Add("RIF");
                    validationErrors.Add(new ValidationResult("RIF Already Exists", memberNames));
                }

                //Only the offending client shoud get the validation errors
                Caller.OnInvalidReciept(validationErrors);
            }
        }

        /// <summary>
        /// Updates an existing reciept.
        /// If the recipet dosnt exist, then go ahead and create it
        /// </summary>
        /// <param name="reciept"></param>
        public void UpdateReciept(Reciept reciept)
        {

            var context = new ValidationContext(reciept, null, null);
            var validationErrors = new List<ValidationResult>();

            //Is it already in the database?
            var reciepts = db.Reciepts;
            int queryForOriginal = reciepts.Where(rec =>
                rec.RIF == reciept.RIF &&
                rec.Project.ID == reciept.ProjectID).Count();
            bool alreadyExists = (queryForOriginal > 0);

            //Go ahead and add it
            if (!alreadyExists)
            {
                AddReciept(reciept);
                return;
            }

            //If it is valid, then save it to the DB, and alert all the clients of a new reocrd
            if (Validator.TryValidateObject(reciept, context, validationErrors))
            {
                //Find the ID of the original in the database
                var dbReciept = db.Reciepts.Where(rec => rec.RIF== reciept.RIF && rec.ProjectID == reciept.ProjectID).Single();
                reciept.ID = dbReciept.ID;

                //Update it
                db.Entry(dbReciept).CurrentValues.SetValues(reciept);
                db.SaveChanges();

                //ALL clients should get the new updated record
                Clients.OnRecieptDeleted(reciept.ID);
                Clients.RecieveReciept(reciept);

                //Let the client who just submitted the information know that it was succesful
                Caller.OnNewRecieptSaved();
            }
            //Otherwise, report all validation errors
            else
            {
                //Only the offending client shoud get the validation errors
                Caller.OnInvalidReciept(validationErrors);
            }
        }

        
        /// <summary>
        /// Delete a reciept
        /// </summary>
        /// <param name="RecieptID"></param>
        public void DeleteReciept(Guid RecieptID)
        {
            Reciept reciept = db.Reciepts.Find(RecieptID);

            db.Reciepts.Remove(reciept);
            db.SaveChanges();

            Clients.OnRecieptDeleted(RecieptID);
        }


    }
}