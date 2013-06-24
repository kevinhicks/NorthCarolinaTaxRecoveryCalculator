using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NorthCarolinaTaxRecoveryCalculator.ViewModels.Project
{
    public class ProjectViewModel
    {
        public Guid ID { get; set; }

        [Required]
        [Display(Name = "Project Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Date Started")]
        [DataType(DataType.Date)]
        public DateTime DateStarted { get; set; }

        [Required]
        [Display(Name = "Deleted")]
        public bool IsDeleted { get; set; }

        public int OwnerID { get; set; }
    }

    public class OwnedAndSharedProjectViewModel
    {
        public IEnumerable<ProjectViewModel> MyProjects { get; set; }
        public IEnumerable<ProjectViewModel> SharedProjects { get; set; }
    }

    public class ProjectOverviewAndCollaboratorsViewModel
    {
        public ProjectViewModel Project { get; set; }
        public IEnumerable<Data.UserProfile> UsersAccessProjects { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? FilterStartDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? FilterEndDate { get; set; }
    }
}
