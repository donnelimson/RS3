using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Enums
{
    public enum ConsumerClasses
    {
        [Description("Residential")]
        Residential = 1,

        [Description("Low Voltage")]
        LowVoltage = 2,

        [Description("High Voltage")]
        HighVoltage = 3,
    }
}
