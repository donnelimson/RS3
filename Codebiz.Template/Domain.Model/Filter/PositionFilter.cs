namespace Codebiz.Domain.Common.Model.Filter
{
    public class PositionFilter : FilterBase
    {
        public string SearchTerm { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
    }
}
