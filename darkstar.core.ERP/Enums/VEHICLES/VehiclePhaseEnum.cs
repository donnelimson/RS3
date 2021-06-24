using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.ERP.Model.Enums.VEHICLES
{
    public enum VehiclePhaseEnum
    {
        [Description("Vehicle Request")]
        VehicleRequest = 1,

        [Description("Recommend For Inspection")]
        RecommendForInspection = 2,

        [Description("Approval")]
        Approval = 3,

        [Description("Done")]
        Done = 4,

        [Description("Endorsement for motorpol section")]
        EndorsmentForMotorpolSection = 5,

        [Description("Recommend For Approval")]
        RecommendForApproval = 6,

        [Description("Vehicle Inspection")]
        VehicleInspection = 7,

        [Description("Vehicle Re-Inspection")]
        VehicleReInspection = 8,

        [Description("Departure")]
        Departure = 9,

        [Description("Motorpol Section")]
        MotorpolSection = 10,

        [Description("Repaired")]
        Repaired = 11,

        [Description("Arrival")]
        Arrival = 12
    }
}
