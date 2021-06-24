using Codebiz.Domain.Common.Model.DataModel;
using Codebiz.Domain.Common.Model.DataModel.CSA.SupportingDocument;
using Codebiz.Domain.Common.Model.DataModel.Transaction;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codebiz.Domain.Common.Model
{
    public class TransactionType
    {
        public TransactionType()
        {
            TransactionSubTypes = new HashSet<TransactionSubType>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TransactionTypeId { get; set; }

        public string Description { get; set; }

        [ForeignKey("TransactionTypeCategory")]
        public int TransactionTypeCategoryId { get; set; }
        public virtual TransactionTypeCategory TransactionTypeCategory { get; set; }

        public bool IsRequest { get; set; }

        public bool IsApplication { get; set; }

        public bool HasApproval { get; set; }
        public bool IsOtherRequest { get; set; }

        public bool HasSeparateRequestInformation { get; set; }

        public bool IsUsedInSupportingDocuments { get; set; }

        public bool IsUsedInDocumentNumbering { get; set; }

        public bool ForJobOrder { get; set; }
        public bool HasPurpose { get; set; }
        public virtual ICollection<TransactionSubType> TransactionSubTypes { get; set; }
    }
}
