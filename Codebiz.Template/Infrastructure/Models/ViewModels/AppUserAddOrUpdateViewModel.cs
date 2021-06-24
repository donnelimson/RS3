using System.Collections.Generic;

namespace Infrastructure.Models.ViewModels
{
    public class AppUserAddOrUpdateViewModel
    {
        public int AppUserId { get; set; }
        public int? EmployeeId { get; set; }
        public string EmployeeNo { get; set; }
        public string Username { get; set; }
        public bool IsActive { get; set; }
        public List<int> SelectedUserGroups { get; set; }
    }
}
