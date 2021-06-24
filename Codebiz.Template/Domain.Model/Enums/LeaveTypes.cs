using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Enums
{
    public enum LeaveTypes
    {
        [Description("Vacation Leave")]
        VL = 1,

        [Description("Sick Leave")]
        SL = 2,

        [Description("Special Emergency Leave")]
        SEL = 3,

        [Description("Special Privilege Leave")]
        SPRL = 4,

        [Description("Maternity Leave")]
        MTL = 5,

        [Description("Mandatory Leave")]
        MDL = 6,

        [Description("Emergency Leave")]
        EML = 7,

        [Description("Study Leave")]
        STL = 8,

        [Description("Centenial Leave")]
        CNTL = 9,

        [Description("Academic Break")]
        AB = 10,

        [Description("Parental Leave")]
        PRTL = 11,

        [Description("Service Leave")]
        SRVL = 12,

        [Description("Indefinite Leave")]
        INDL = 13,

        [Description("Relocation Leave")]
        RL = 14,

        [Description("Paternity Leave")]
        PATL = 15,

        [Description("Magna Carta for Women")]
        MCW = 16,

        [Description("Solo Parent Leave")]
        SLPRL = 17,

        [Description("Ramadan Leave")]
        RML = 18,

        [Description("Calamity Leave")]
        CLML = 19,

        [Description("Compensatory Time-Off")]
        CTO = 20,

        [Description("Forced Leave")]
        FL = 21,

        [Description("Cumulative Leave")]
        CL = 22,

        [Description("Leave With Pay")]
        LWP = 23,

        [Description("Accrued Leave")]
        AL = 24
    }

    public enum WhereSpents
    {
        [Description("Abroad")]
        Abroad = 1,

        [Description("Local")]
        Local = 2,

        [Description("Hospital")]
        Hospital = 3,
    }
}
