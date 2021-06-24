using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Enums.CommercialServicesApplication
{
    public enum MembershipTypeSubcategories
    {
        [Description("Single")]
        Single = 1,

        [Description("Joint")]
        Joint = 2,

        [Description("Corporation")]
        Corporation = 3,

        [Description("Association")]
        Association = 4,

        [Description("Agency")]
        Agency = 5,

        [Description("Sole Proprietor")]
        SoleProprietor = 6,

        [Description("Cooperative")]
        Cooperative = 7,

        [Description("Partnership")]
        Partnership = 8
    }
}
