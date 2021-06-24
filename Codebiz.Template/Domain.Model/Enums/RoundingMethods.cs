using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Enums
{
    public enum RoundingMethods
    {

        [Description("No Rounding")]
        NoRounding = 1,
        [Description("Round to Full Decimal Amount")]
        RoundToFullDecimalAmount = 2,
        [Description("Round to Full Amount")]
        RoundToFullAmount = 3,
        [Description("Round to Full Tens Amount")]
        RoundToFullTensAmount = 4
    }
}
