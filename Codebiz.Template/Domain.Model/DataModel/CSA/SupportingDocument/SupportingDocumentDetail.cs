using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codebiz.Domain.Common.Model.DataModel.CSA.SupportingDocument
{
    public class SupportingDocumentDetail
    {
        [Key]
        public int SupportingDocumentDetailId { get; set; }

        [MaxLength(100)]
        public string DocumentName { get; set; }

        [ForeignKey("SupportingDocument")]
        public int SupportingDocumentId { get; set; }
        public virtual SupportingDocument SupportingDocument { get; set; }

        public int? Group { get; set; }

        public bool IsRequired { get; set; }
        public bool IsDeleted { get; set; }
    }
}
