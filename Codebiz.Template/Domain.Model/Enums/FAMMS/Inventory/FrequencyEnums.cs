using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Enums
{
    public enum FrequencyEnums
    {
        [Description("Daily")]
        Daily = 1,

        [Description("Weekly")]
        Weekly = 2,

        [Description("Every 4 Weeks")]
        EveryFourWeeks = 3,

        [Description("Monthly")]
        Monthly = 4,

        [Description("Quarterly")]
        Quarterly = 5,

        [Description("Semi Annually")]
        SemiAnnually = 6,

        [Description("One Time")]
        OneTime = 7,

        [Description("Annually")]
        Annually = 8,

        [Description("Every X Days")]
        EveryXDays = 9,
    }

    public enum SubFrequencyEnums
    {
        [Description("Monday")]
        Monday = 1,

        [Description("Tuesday")]
        Tuesday = 2,

        [Description("Wednesday")]
        Thursday = 3,

        [Description("Thursday")]
        Monthly = 4,

        [Description("Friday")]
        Quarterly = 5,

        [Description("Saturday")]
        SemiAnnually = 6,

        [Description("Sunday")]
        OneTime = 7,
    }
}
