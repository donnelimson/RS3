using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Enums
{
    public enum ReportCategoryEnums
    {
        [Description("Customer Service")]
        CustomerService = 1,

        [Description("Billing")]
        Billing = 2,

        [Description("Engineering")]
        Engineering = 3,

        [Description("Vehicle")]
        Vehicle = 4,

        [Description("Material")]
        Material = 5
    }
}
