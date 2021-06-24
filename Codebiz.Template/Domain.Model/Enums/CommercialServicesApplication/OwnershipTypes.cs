using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Enums.CommercialServicesApplication
{
    public enum OwnershipTypes
    {
        [Description("Private")]
        Private = 1,

        [Description("Government/GOCC")]
        Government = 2
    }
}
