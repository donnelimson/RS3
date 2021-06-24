using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Enums.CommercialServicesApplication
{
    public enum PersonTypes
    {
        [Description("Consumer")]
        Consumer = 1,

        [Description("Spouse")]
        Spouse = 2,

        [Description("Claimant")]
        Claimant = 3,

        [Description("App User")]
        AppUser = 4,

        [Description("ChangeOfNameSpouse")]
        ChangeOfNameSpouse = 5,

        [Description("Witness")]
        Witness = 6,
        [Description("Representative")]
        Representative = 7,
    }
}
