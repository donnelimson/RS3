using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Enums
{
    public enum ApprovalStageLabels
    {
        [Description("Checked By")]
        CheckedBy = 1,

        [Description("Noted By")]
        NotedBy = 2,

        [Description("Reviewed By")]
        ReviewedBy = 3,
        [Description("Approved By")]
        ApprovedBy = 4,
    }
}
