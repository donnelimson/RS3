using Codebiz.Domain.Common.Model.DataModel.Transaction;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codebiz.Domain.Common.Model.DataModel.CSA.SupportingDocument
{
    public class SupportingDocument : ModelBase
    {
        public SupportingDocument()
        {
            Details = new HashSet<SupportingDocumentDetail>();
        }

        [Key]
        public int SupportingDocumentId { get; set; }

        [ForeignKey("TransactionType")]
        public int TransactionTypeId { get; set; }
        public virtual TransactionType TransactionType { get; set; }

        [ForeignKey("TransactionSubType")]
        public int? TransactionSubTypeId { get; set; }
        public virtual TransactionSubType TransactionSubType { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<SupportingDocumentDetail> Details { get; set; }
    }
}
