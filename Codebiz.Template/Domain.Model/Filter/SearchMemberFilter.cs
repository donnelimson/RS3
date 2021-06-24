namespace Codebiz.Domain.Common.Model.Filter
{
    public class SearchMemberFilter : FilterBase
    {
        public string ConsumerNo { get; set; }
        public string Name { get; set; }
        public int? RequiredFilter { get; set; }
        public int? MemberId { get; set; }
        public string MembershipType { get; set; }
        public string SearchText { get; set; }
    }
}
