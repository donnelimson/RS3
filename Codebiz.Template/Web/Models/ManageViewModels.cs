using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Web.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public int PersonnelId { get; set; }

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

        [Display(Name = "Access Level")]
        public int? SelectedAccessLevel { get; set; }
        public List<KeyValuePair<int, string>> AccessLevelLookUp { get; set; }

        [Display(Name = "Region")]
        public int? SelectedRegion { get; set; }
        public List<KeyValuePair<int, string>> RegionLookUp { get; set; }

        [Display(Name = "Unit")]
        public int? SelectedUnit { get; set; }
        public List<KeyValuePair<int, string>> UnitLookUp { get; set; }

        [Display(Name = "Sub-Unit")]
        public int? SelectedSubUnit { get; set; }
        public List<KeyValuePair<int, string>> SubUnitLookUp { get; set; }

        [Required]
        [Display(Name = "Position")]
        public int? SelectedPositionId { get; set; }
        public virtual List<KeyValuePair<int, string>> PositionLookUp { get; set; }

        [Required]
        [Display(Name = "Office")]
        public int? SelectedOfficeId { get; set; }
        public List<KeyValuePair<int, string>> OfficeLookUp { get; set; }
        public string AppUserPhoto { get; set; }

        [Required]
        [Display(Name = "Department")]
        public int? SelectedDepartmentId { get; set; }
        public List<KeyValuePair<int, string>> DepartmentLookUp { get; set; }

        public bool ForFrofile { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}