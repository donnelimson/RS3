using PagedList;
using Codebiz.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.ViewModels.AppUsers
{
    public class AppUsersIndexViewModel
    {
        public string AccountNumber { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string Position { get; set; }

        public string Office { get; set; }

        public string SortBy { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public IPagedList<AppUser> AppUsers { get; set; }
    }
}