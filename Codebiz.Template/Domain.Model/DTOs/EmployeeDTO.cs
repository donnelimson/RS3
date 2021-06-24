namespace Codebiz.Domain.Common.Model.DTOs
{
    public class EmployeeDto
    {
        public int AppUserId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeNo { get; set; }
        public string FullName { get; set; }
        public int? PositionId { get; set; }
        public string Office { get; set; }
        public string PositionName { get; set; }
        public string MemberPhotoThumbnailUrl { get; set; }
        public int? DivisionId { get; set; }
        public string DivisionName { get; set; }
        public int? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int? DivisionCategoryId { get; set; }
        public string DivisionCategoryName { get; set; }
        public bool IsMainOffice { get; set; }
    }

    public class EmployeeLookUpDTO
    {
        public int AppUserId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeNo { get; set; }
        public string FullName { get; set; }
        public string AreaOffice { get; set; }
        public string Position { get; set; }
    }
}
