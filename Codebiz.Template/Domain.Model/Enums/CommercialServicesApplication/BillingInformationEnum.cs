using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Enums.CommercialServicesApplication
{
    public enum BillingInformationEnum
    {
        [Description("HardCopy")]
        HardCopy = 1,

        [Description("Email")]
        Email = 2,

        [Description("SMS")]
        SMS = 3
    }
}
