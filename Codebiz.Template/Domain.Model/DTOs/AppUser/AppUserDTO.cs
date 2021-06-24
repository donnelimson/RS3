using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs
{
    public class AppUserDTO
    {
        public int AppUserId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public string Office { get; set; }
        public string Department { get; set; }
        public bool EmailConfirmation { get; set; }
        public string AppUserStatus { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string UserGroup { get; set; }
        public string AppUserPhotoThumnnailUrl { get; set; }

        public bool IsManager { get; set; }
    }
    public class AppUserSearchDTO : AppUserNameAndIdDTO
    {
        public int Id { get; set; }
        public string Office { get; set; }
        public string AreaOffice { get; set; }
        public string EmployeeNo { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public string Division { get; set; }
        public string Email { get; set; }
        public string BadgeNo { get; set; }
        public string AppUserPhotoThumbnailUrl { get; set; }
        public string EmployeePhotoThumbnailUrl { get; set; }
    }
    public class AppUserApprovalTemplateModalDetailsDTO
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public string FullName { get; set; }
        public string Office { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
    }
    public class AppUserMigrateData
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string OfficeCode { get; set; }
        public string Office { get; set; }
        public string DepartmentCode { get; set; }
        public string Department { get; set; }
        public string DivisionCode { get; set; }
        public string Division { get; set; }
        public string Position { get; set; }
        public string Head { get; set; }
        public string Manager { get; set; }

    }
    public class AppUserNameAndIdDTO
    {
        public int AppUserId { get; set; }
        public int? EmployeeId { get; set; }
        public string FullName { get; set; }
    }
    public class AllAppUserApprovalTemplateModalDetailsDTO : AppUserApprovalTemplateModalDetailsDTO
    {

    }
}

