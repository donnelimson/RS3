using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DataModel
{
    public class Purpose:ModelBase
    {
        public Purpose()
        {
            this.PurposeDetails = new HashSet<PurposeDetail>();
        }
        [Key]
        public int Id { get; set; }
        [ForeignKey("TransactionType")]
        public int TransactionTypeId { get; set; }
        public virtual TransactionType TransactionType { get; set; }
        public virtual ICollection<PurposeDetail> PurposeDetails { get; set; }

    }
    public class PurposeDetail
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Purpose")]
        public int PurposeId { get; set; }
        public Purpose Purpose { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
      //  public virtual ICollection<ChangeOfMeter> ChangeOfMeters { get; set; }
    }
}
