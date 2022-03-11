using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Enums
{
    public enum UoMEnums
    {
        [Description("Kilo")]
        Kilo = 1,

        [Description("Box")]
        Box = 2,
        [Description("Piece")]
        Piece = 3,
    }
}
