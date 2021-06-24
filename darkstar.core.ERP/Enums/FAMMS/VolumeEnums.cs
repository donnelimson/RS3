using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.ERP.Model.Enums.FAMMS
{
    public enum VolumeEnums
    {
        [Description("cc")]
        CubicCentimetre = 1,

        [Description("cdm")]
        CubicDecimeter = 2,

        [Description("cf")]
        CubicFoot = 3,
        [Description("ci")]
        CubicInch = 4,
        [Description("cm")]
        CentiMeters = 5,
        [Description("cmm")]
        CentiMilimeters = 6,
    }
}
