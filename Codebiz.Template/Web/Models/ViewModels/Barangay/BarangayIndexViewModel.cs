using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.ViewModels.Barangay
{
    public class BarangayIndexViewModel
    {
        public string Name { get;  set;}

        public string SortBy { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public IPagedList <Codebiz.Domain.Common.Model.Barangay> Barangays { get; set; }
    }
}