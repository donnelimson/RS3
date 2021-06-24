using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model
{
    public enum AccountStatuses
    {
        [Description("Approved")]
        Approved = 1,

        [Description("Denied")]
        Denied = 2,

        [Description("Pending")]
        Pending = 3,
    }
}
