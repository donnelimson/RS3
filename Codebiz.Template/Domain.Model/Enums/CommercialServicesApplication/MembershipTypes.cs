using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Enums.CommercialServicesApplication
{
    public enum MembershipTypes
    {
        [Description("Natural")]
        Natural = 1,

        [Description("Juridical")]
        Juridical = 2,
    }
}
