using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model
{
    public enum Phases
    {
        [Description("MDTO")]
        MDTO = 1,

        [Description("House Wiring Inspection")]
        HWI = 2,

        [Description("For Payment")]
        ForPayment = 3,

        [Description("Manager Approval")]
        ManagerApproval = 3,

        [Description("Genaral Manager Approval")]
        GMApproval = 3,

        [Description("COM Section Head")]
        COMSectionHead = 3,
    }
}
