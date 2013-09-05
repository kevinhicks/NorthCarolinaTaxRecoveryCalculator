using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NorthCarolinaTaxRecoveryCalculator.Models;
using System.ComponentModel.DataAnnotations;

namespace NorthCarolinaTaxRecoveryCalculator.ViewModels.Project
{
    public class OwnedAndSharedProjectViewModels
    {
        public IEnumerable<NorthCarolinaTaxRecoveryCalculator.Models.Project> MyProjects { get; set; }
        public IEnumerable<NorthCarolinaTaxRecoveryCalculator.Models.Project> SharedProjects { get; set; }
    }

    public class ProjectOverviewAndCollaboratorsViewModels
    {
        public NorthCarolinaTaxRecoveryCalculator.Models.Project Project { get; set; }
        public IEnumerable<NorthCarolinaTaxRecoveryCalculator.Models.UsersAccessProjects> UsersAccessProjects { get; set; }

        [Display(Name = "Filter By Recipets After")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yy}")]
        public DateTime? FilterStartDate { get; set; }

        [Display(Name = "Filter By Recipets Before")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yy}")]
        public DateTime? FilterEndDate { get; set; }

        public IEnumerable<CountyTotals> CountyTotals { get; set; }

        public double TotalCountyTaxForProject { get; set; }
        public double TotalStateTaxForProject { get; set; }
        public double TotalFoodTaxForProject { get; set; }
        public double TotalTransitTaxForProject { get; set; }
        public double TotalSpentOnProject { get; set; }

        public string InvitationEmail { get; set; }
        public UserType UserType { get; set; }
    }

    public class CountyTotals
    {
        public string Name { get; set; }
        public double CountyTax { get; set; }
        public double StateTax { get; set; }
        public double FoodTax { get; set; }
        public double TransitTax { get; set; }
    }
}
