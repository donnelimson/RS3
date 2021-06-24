using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.ERP.Model.Enums.VEHICLES
{
    public enum RestrictionCodeEnum
    {
        [Description("Motorbikes or motorized tricycles")]
        RestrictionOne = 1,

        [Description("Motor vehicle up to 4500 kg GVW")]
        RestrictionTwo = 2,

        [Description("Motor vehicle above 4500 kg GVW")]
        RestrictionThree = 3,

        [Description("Automatic transmission up to 4500 kg GVW")]
        RestrictionFour = 4,

        [Description("Automatic transmission above 4500 kg GVW")]
        RestrictionFive = 5,

        [Description("Articulated vehicle 1600 kg GVW")]
        RestrictionSix = 6,

        [Description("Articulated vehicle 1601 up to 4500 kg GVW")]
        RestrictionSeven = 7,

        [Description("Articulated vehicle 4501 kg and above GVW")]
        RestrictionEight = 8,
    }
}
