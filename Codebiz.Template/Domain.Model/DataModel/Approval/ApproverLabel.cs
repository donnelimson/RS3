using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model
{
    public class ApproverLabel : ModelBase
    {
        public int ApproverLabelId { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<ApprovalStage> ApprovalStage { get; set; }
    }
}
