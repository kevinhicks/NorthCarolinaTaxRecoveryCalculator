﻿using System;
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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]        
        public DateTime FilterStartDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FilterEndDate { get; set; }
    }
}
