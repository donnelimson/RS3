using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Codebiz.Domain.Common.Model
{
    public class TransactionTypeCategory
    {
        public TransactionTypeCategory()
        {
            this.TransactionTypes = new HashSet<TransactionType>();
        }

        [Key]
        public int TransactionTypeCategoryId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TransactionType> TransactionTypes { get; set; }
    }
}
