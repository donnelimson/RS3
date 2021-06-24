using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models.ViewModels.Report
{
    public class UserReportViewModel
    {
        [Display(Name = "Status")]
        public int? Status { get; set; }
        public List<KeyValuePair<int, string>> Statuses { get; set; }

        [Display(Name = "Access Level")]
        public int? AccessLevelId { get; set; }
        public List<KeyValuePair<int, string>> AccessLevels { get; set; }

        [Display(Name = "User Group")]
        public int? UserGroupId { get; set; }
        public List<KeyValuePair<int, string>> UserGroups { get; set; }
    }
}