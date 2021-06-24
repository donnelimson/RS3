using System;

namespace Codebiz.Domain.Common.Model.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public int? EmployeeId { get; set; }
        public string EmployeeNo { get; set; }
        public string BadgeNo { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }

        public string Office { get; set; }
        public bool IsMainOffice { get; set; }

        public int? PositionId { get; set; }
        public string PositionName { get; set; }

        public int? DivisionId { get; set; }
        public string DivisionName { get; set; }

        public int? DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public int? DivisionCategoryId { get; set; }
        public string DivisionCategoryName { get; set; }

        public string MemberPhotoThumbnailUrl { get; set; }

        #region License
        public string LicenseNo { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Restriction { get; set; }
        #endregion
    }
}
