using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model
{
    public enum AccessLevels
    {
        [Description("Sub-Unit")]
        SubUnit = 1,

        [Description("Unit")]
        Unit = 2,

        [Description("Regional")]
        Regional = 3,

        [Description("National")]
        National = 4
    }
}
