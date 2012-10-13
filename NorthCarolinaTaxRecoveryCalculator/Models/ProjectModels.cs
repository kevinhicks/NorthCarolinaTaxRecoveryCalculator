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
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

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

        //public virtual ICollection<Reciept> Reciepts { get; set; }
    }
}
