using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models.ViewModels.Report
{
    public class EligibilityReportViewModel
    {
        [Required]
        [Display(Name = "Eligibility Titles")]
        public List<int> SelectedEligibilityTitles { get; set; }

        public List<KeyValuePair<int, string>> EligibilityTitles { get; set; }
    }
}