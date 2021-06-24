using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.ViewModels.NavLink
{
    public class NavLinkIndexViewModel
    {
        public string SearchTerm { get; set; }

        public string SortBy { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public IPagedList<Codebiz.Domain.Common.Model.NavLink> NavLinks { get; set; }
    }
}