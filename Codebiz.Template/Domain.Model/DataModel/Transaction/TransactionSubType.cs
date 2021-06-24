using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codebiz.Domain.Common.Model.DataModel.Transaction
{
    public class TransactionSubType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TransactionSubTypeId { get; set; }

        public string Description { get; set; }

        [ForeignKey("TransactionType")]
        public int TransactionTypeId { get; set; }
        public virtual TransactionType TransactionType { get; set; }

        public bool IsUsedInSupportingDocuments { get; set; }
    }
}
