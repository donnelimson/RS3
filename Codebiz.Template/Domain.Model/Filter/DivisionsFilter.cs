namespace Codebiz.Domain.Common.Model.Filter
{
    public class DivisionsFilter : FilterBase
    {
        public string DivisionCode { get; set; }
        public string DivisionName { get; set; }
        public int? OfficeId { get; set; }
        public int? DepartmentId { get; set; }
        public int? PositionId { get; set; }
        public int? CategoryId { get; set; }
    }
}
