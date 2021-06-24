using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Codebiz.Domain.Common.Model;

namespace Web.Models.ViewModels.UserProfile
{
    public class UserProfileIndexViewModel
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Lastname { get; set; }

        public string Firstname { get; set; }

        public string MiddleName { get; set; }

        public bool IsActive { get; set; }

        public string UserGroup { get; set; }

        public string AccessLevel { get; set; }

        public string Region { get; set; }

        public string Unit { get; set; }

        public string SubUnit { get; set; }

        public ContentFile ContentFile { get; set; }


    }
}