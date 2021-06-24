using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Codebiz.Domain.Common.Model.DataModel
{
    public class DocumentHistory
    {
        [Key]
        public int Id { get; set; }
        public string Details { get; set; }
        public string Remarks { get; set; }
        public int? TransactionTypeId { get; set; } //Transaction Type
        public string ReferenceNo { get; set; } // Tracking No, Control No
        public string GroupReferenceNo { get; set; }
        public int GroupTransactionTypeId { get; set; }
        //Other References
        [ForeignKey("CreatedByAppUser")]
        public int CreatedByAppUserId { get; set; } = 1;
        [ScriptIgnore]
        public virtual AppUser CreatedByAppUser { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
