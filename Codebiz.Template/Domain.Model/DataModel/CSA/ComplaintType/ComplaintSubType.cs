using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codebiz.Domain.Common.Model
{
    public class ComplaintSubType
    {
        [Key]
        public int ComplaintSubTypeId { get; set; }

        [ForeignKey("ComplaintType")]
        public int ComplaintTypeId { get; set; }
        public virtual ComplaintType ComplaintType { get; set; }

        public string Complaint { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
