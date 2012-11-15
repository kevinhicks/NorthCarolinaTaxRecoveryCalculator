using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NorthCarolinaTaxRecoveryCalculator.Models;

namespace NorthCarolinaTaxRecoveryCalculator.ViewModels.Project
{
    public class OwnedAndSharedProjectViewModels
    {
        public IEnumerable<NorthCarolinaTaxRecoveryCalculator.Models.Project> MyProjects { get; set; }
        public IEnumerable<NorthCarolinaTaxRecoveryCalculator.Models.Project> SharedProjects { get; set; }
    }
}