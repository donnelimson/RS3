using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models.ViewModels.Permission
{
    public class PermissionEditViewModel
    {
        public int PermissionId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Permission Group")]
        public int SelectedPermissionGroup { get; set; }
        public List<KeyValuePair<int, string>> PermissionGroupLookUp { get; set; }

        [Display(Name = "Navigation Link")]
        public int? SelectedNavLink { get; set; }
        public List<KeyValuePair<int, string>> NavLinkLookUp { get; set; }
    }
}