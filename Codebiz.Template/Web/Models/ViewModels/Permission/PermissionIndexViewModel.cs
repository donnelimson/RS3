using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.ViewModels.Permission
{
    public class PermissionIndexViewModel
    {
        //string searchStr, string sortOrder, int maxNumberOfRows, int pageNumber

        public string SearchStr { get; set; }
        public string SortBy { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public IPagedList<Codebiz.Domain.Common.Model.Permission> Permissions { get; set; }
    }
}