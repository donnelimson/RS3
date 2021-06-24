using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.ERP.Model.Enums.BP
{
    public enum CustomerGroupEnum
    {
        [Description("Distibution Charge - Residential Sales")]
        ResidentialSales = 1,
        [Description("Distibution Charge - Low Voltage Sales")]
        LowVoltage = 2,
        [Description("Distibution Charge - High Voltage Sales")]
        HighVoltage = 3,
    }
}
