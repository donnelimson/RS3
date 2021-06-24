using Codebiz.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models.ViewModels.UserGroup
{
    public class UserGroupEditViewModel
    {
        [Key]
        public int UserGroupId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Permissions")]
        public List<int> SelectedPermissions { get; set; }
        //public List<KeyValuePair<int, string>> PermissionsLookUp { get; set; }

        public List<int> SelectedPermissionGroups { get; set; }
        public List<PermissionGroup> PermissionGroupsLookUp { get; set; }

        //[Display(Name = "Navigation Links")]
        //public List<int> SelectedNavlinks { get; set; }
        //public List<KeyValuePair<int, string>> NavlinksLookUp { get; set; }
    }
}