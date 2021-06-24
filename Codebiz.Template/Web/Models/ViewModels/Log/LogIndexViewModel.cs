using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models.ViewModels.Log
{
    public class LogIndexViewModel
    {
        [Display(Name = "Activity ID")]
        public string ActivityId { get; set; }

        public string User { get; set; }

        public string Thread { get; set; }

        public List<string> Level { get; set; }

        public string Logger { get; set; }

        public int? LogEventTitle { get; set; }
        public List<KeyValuePair<int, string>> LogEventTitleLookup { get; set; }

        public string Message { get; set; }

        public string Exception { get; set; }

        public string DateFrom { get; set; }

        public string DateTo { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string SortBy { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public IPagedList<Codebiz.Domain.Common.Model.Log> Logs { get; set; }
    }
}