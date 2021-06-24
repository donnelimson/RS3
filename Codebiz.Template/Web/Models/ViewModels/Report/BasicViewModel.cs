using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models.ViewModels.Report
{
    public class BasicViewModel
    {
        [Display(Name = "Status")]
        public int SelectedStatus { get; set; }
        public List<KeyValuePair<int, string>> Statuses { get; set; }
    }
}