using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.ERP.Model.Enums.VEHICLES
{
    public enum VehicleStatuses
    {
        [Description("Pending")]
        Pending = 1,

        [Description("Approved")]
        Approved = 2,

        [Description("Rejected")]
        Rejected = 3,

        [Description("Disapproved")]
        Disapproved = 4,


        [Description("Operational")]
        Operational = 5,

        [Description("Non Operational")]
        NonOperational = 6,

        [Description("Under Repair")]
        UnderRepair = 7,

        [Description("Pull Out")]
        PullOut = 8,

        [Description("For Inspection")]
        ForInspection = 9,

        [Description("For Repair")]
        ForRepair = 10,

        [Description("Active")]
        Active = 11,

        [Description("For Maintenance")]
        ForMaintenance = 12,

        [Description("Under Maintenance")]
        UnderMaintenance = 13,

        [Description("Under Inspection")]
        UnderInspection = 14,
    }

    
}
