using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Enums.CommercialServicesApplication
{
    public enum CoopEvaluationEnum
    {
        [Description("No unsettled/outstanding obligation at the time of filling application")]
        IsNoUnsettledOutstandingObigation = 1,


        [Description("Has never been apprehended of electric pilferage by the Cooperative")]
        HasNeverBeenApprehended = 2,

        [Description("With outstanding accounts of more than 60 days")]
        IsWithoutStandingAccounts = 3,
        [Description("Has been apprehended of electric pilferage by the Cooperative on")]
        HasBeenApprehended = 4,

        [Description("Cause of death is self-inflicted")]
        IsSelfInflicted = 5,

        [Description("Not removed for cause as director or employee from the Cooperative")]
        IsNotRemoved = 6,


    }
}
