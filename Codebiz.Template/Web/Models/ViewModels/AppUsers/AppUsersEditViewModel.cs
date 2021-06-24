using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models.ViewModels.AppUsers
{
    public class AppUsersEditViewModel
    {
        [Key]
        public int AppUserId { get; set; }

        //public int PersonnelId { get; set; }

        //[Required]
        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(5)]
        public string Username { get; set; }

        [Required]
        [MaxLength(50)]
        [RegularExpression("^([ña-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        public bool? IsLocalNetworkUser { get; set; }

        [Display(Name = "User Groups")]
        public List<int> SelectedUserGroups { get; set; }
        public List<KeyValuePair<int, string>> UserGroupsLookUp { get; set; }

        [Required]
        [Display(Name = "Position")]
        public int? SelectedPositionId { get; set; }
        public virtual List<KeyValuePair<int, string>> PositionLookUp { get; set; }

        [Required]
        [Display(Name = "Office")]
        public int? SelectedOfficeId { get; set; }
        public List<KeyValuePair<int, string>> OfficeLookUp { get; set; }

        [Required]
        [Display(Name = "Department")]
        public int? SelectedDepartmentId { get; set; }
        public List<KeyValuePair<int, string>> DepartmentLookUp { get; set; }
        public string AppUserPhoto { get; set; }
        public string thumbnailUrl { get; set; }
    }
}