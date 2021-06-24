using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codebiz.Domain.Common.Model
{
    public class ApprovalTemplateTransaction
    {
        [Key]
        public int ApprovalTemplateTransactionId { get; set; }
        [ForeignKey("ApprovalTemplate")]
        public int ApprovalTemplateId { get; set; }
        public virtual ApprovalTemplate ApprovalTemplate { get; set; }
        [ForeignKey("TransactionType")]
        public int TransactionTypeId { get; set; }
        public virtual TransactionType TransactionType { get; set; }
    }
}
