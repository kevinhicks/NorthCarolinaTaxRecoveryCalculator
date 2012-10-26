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
    public class Project
    {
        private Guid _guid = Guid.NewGuid();    

        [Key]
        //[DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]        
        public Guid ID
        {
            get
            {
                return _guid;
            }
            set
            {
                _guid = value;
            }
        }

        [Required]
        [Display(Name = "Project Name")]
        public string Name { get; set; }

        /// <summary>
        /// Default value for DateStarted is today
        /// </summary>
        private DateTime _DateStarted = DateTime.Now;

        [Required]
        [Display(Name = "Date Started")]
        [DataType(DataType.Date)]
        public DateTime DateStarted 
        { 
            get
            {
                return _DateStarted;
            }
            set
            {
                _DateStarted = value;
            }

        }

        [Required]
        [Display(Name = "Deleted")]
        public bool IsDeleted { get; set; }
        
        public int OwnerID { get; set; }
        public virtual UserProfile Owner { get; set;}
    }
}
