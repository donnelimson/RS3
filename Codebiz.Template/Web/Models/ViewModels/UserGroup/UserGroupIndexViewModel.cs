using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.ViewModels.UserGroup
{
    public class UserGroupIndexViewModel
    {
        public string SearchTerm { get; set; }

        public string SortBy { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public IPagedList<Codebiz.Domain.Common.Model.UserGroup> UserGroups { get; set; }
    }
}