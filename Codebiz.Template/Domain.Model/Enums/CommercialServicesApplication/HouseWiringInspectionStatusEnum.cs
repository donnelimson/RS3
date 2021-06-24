using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Enums.CommercialServicesApplication
{
    public enum HouseWiringInspectionStatusEnum
    {
        [Description("Approved")]
        Approved = 1,

        [Description("Rejected")]
        Rejected = 2,

        [Description("Pending")]
        Pending = 3,

        [Description("Disapproved")]
        Disapproved = 4
    }
}
