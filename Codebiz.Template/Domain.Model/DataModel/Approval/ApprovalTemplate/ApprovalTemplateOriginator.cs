using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model
{
    public class ApprovalTemplateOriginator
    {
        [Key]
        public int ApprovalTemplateOriginatorId { get; set; }
        [ForeignKey("AppUser")]
        public int AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
        [ForeignKey("ApprovalTemplate")]
        public int ApprovalTemplateId { get; set; }
        public virtual ApprovalTemplate ApprovalTemplate { get; set; }
        
    }
}
