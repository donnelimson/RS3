using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Enums.CommercialServicesApplication.Request
{
    public enum ChangeOfNameTypesEnum
    {
        [Description("Sale")]
        Sale = 1,
        [Description("Waived")]
        Waived = 2,
        [Description("Death")]
        Death = 3,
        [Description("ChangeStatus")]
        ChangeStatus = 4,
    }
}
