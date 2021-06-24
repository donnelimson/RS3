using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models.ViewModels.NavLink
{
    public class NavLinkCreateViewModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Controller { get; set; }

        [MaxLength(100)]
        public string Action { get; set; }

        [MaxLength(100)]
        [Display(Name = "Icon Class")]
        public string IconClass { get; set; }

        [MaxLength(2000)]
        public string Parameters { get; set; }

        [Display(Name = "Is Parent")]
        public bool IsParent { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Navigation Group")]
        public int? SelectedParentNavLinkId { get; set; }
        public virtual List<KeyValuePair<int, string>> ParentNavLinkLookUp { get; set; }
    }
}