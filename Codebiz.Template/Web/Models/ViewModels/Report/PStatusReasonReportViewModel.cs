using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.ViewModels.Report
{
    [Serializable]
    public class PStatusReasonReportViewModel
    {
        public List<KeyValuePair<string, string>> PStatusReason { get; set; }
        public string SelectedPStatusReason { get; set; }
    }
}