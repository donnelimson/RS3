using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Web.Models.ViewModels.FileUpload;

namespace Web.Models.ViewModels.AppUsers
{
    public class AppUsersCreateViewModel
    {
        public int AppUserId { get; set; }

        //[Required]
        [MinLength(5)]
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


        public string LastName { get; set; }


        public string FirstName { get; set; }


        public string MiddleName { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        public bool? IsLocalNetworkUser { get; set; }

        [Display(Name = "User Groups")]
        public List<int> SelectedUserGroups { get; set; }
        public List<KeyValuePair<int, string>> UserGroupsLookUp { get; set; }

        [Display(Name = "Position")]
        public int? SelectedPositionId { get; set; }
        public virtual List<KeyValuePair<int, string>> PositionLookUp { get; set; }

        [Required]
        [Display(Name = "Office")]
        public int? SelectedOfficeId { get; set; }
        public List<KeyValuePair<int, string>> OfficeLookUp { get; set; }

        [Display(Name = "Department")]
        public int? SelectedDepartmentId { get; set; }
        public List<KeyValuePair<int, string>> DepartmentLookUp { get; set; }

        [Display(Name = "Division")]
        public int? SelectedDivisionId { get; set; }
        public virtual List<KeyValuePair<int, string>> DivisionLookUp { get; set; }
        public bool ForFrofile { get; set; }
        public int RoleId { get; set; }
    }
}