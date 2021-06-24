using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.Approval
{
    public class ApproverLabelDTO
    {
        public int ApproverLableId { get; set; }

        [DisplayName("NAME")]
        public string Name { get; set; }

        [DisplayName("ACTIVE")]
        public bool IsActive { get; set; }

        [DisplayName("ACTION BY")]
        public string ActionBy { get; set; }

        [DisplayName("ACTION ON")]
        public DateTime ActionOn { get; set; }
        public bool IsApproverLabelInUse { get; set; }

    }

    public class ApproverLabelsViewModel
    {
        public int ApproverLableId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
