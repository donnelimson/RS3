using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Enums
{
    public enum UserStatuses
    {
        [Description("Active")]
        Active = 1,

        [Description("Locked")]
        Locked = 2,

        [Description("Dormant")]
        Dormant = 3,

    }
}
