namespace Codebiz.Domain.Common.Model.Filter
{
    public class SalesEmployeeFilter : FilterBase
    {
        public string SalesEmployee { get; set; }
        public string Position { get; set; }
        public int? AreaOfficeId { get; set; }
        public int CommissionGroupId { get; set; }
        public double? Commission { get; set; }
    }
}
