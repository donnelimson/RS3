using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Enums.CommercialServicesApplication
{
    public enum Voltage
    {
        [Description("High Voltage")]
        High = 1,

        [Description("Low Voltage")]
        Low = 2,
    }
}
