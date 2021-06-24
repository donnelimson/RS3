using System;

namespace Codebiz.Domain.Common.Model.DTOs.ComplaintType
{
    public class ComplaintTypeDTO
    {
        public int ComplaintTypeId { get; set; }
        public string ComplaintType { get; set; }
        public string ComplaintSubTypes { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
