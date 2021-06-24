using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models.ViewModels.Report
{
    public class LeaveTransactionReportViewModel
    {
        public int AppUserId { get; set; }

        [Display(Name = "Date Form")]
        public DateTime? DateFrom { get; set; }
        
        [Display(Name = "Date To")]
        public DateTime? DateTo { get; set; }

        public string SortBy { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }


    }
}