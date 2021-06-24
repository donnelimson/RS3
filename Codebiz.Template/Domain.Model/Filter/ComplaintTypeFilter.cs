namespace Codebiz.Domain.Common.Model.Filter
{
    public class ComplaintTypeFilter : FilterBase
    {
        public string ComplaintType { get; set; }
        public string ComplaintSubType { get; set; }
        public bool ForJORequest { get; set; }
        public bool ForJOComplaint { get; set; }

    }
}
