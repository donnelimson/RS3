using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Models.ViewModels.Report
{
    public class NoPFTReportViewModel
    {
        public int Year { get; set; }
        public IList<SelectListItem> YearLookUp { get; set; }
    }
}