using System.Collections.Generic;

namespace Infrastructure.Models.ViewModels
{
    public class AppUserAddOrUpdateViewModel
    {
        public int AppUserId { get; set; }
        public int? EmployeeId { get; set; }
        public string EmployeeNo { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int? RoleId { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public List<int> SelectedUserGroups { get; set; }
    }
}
