using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models.ViewModels.Report
{
    public class TrainingReportViewModel
    {
        [Required]
        [Display(Name = "Training Titles")]
        public List<int> SelectedTrainingTitles { get; set; }

        public List<KeyValuePair<int, string>> TrainingTitles { get; set; }
    }
}