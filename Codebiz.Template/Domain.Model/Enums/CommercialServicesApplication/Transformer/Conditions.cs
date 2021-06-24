using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Enums.CommercialServicesApplication.Transformer
{
    public enum Conditions
    {
        [Description("Damaged")]
        Damaged = 1,
        [Description("Good")]
        Good = 2,
        [Description("New")]
        New = 3,
        [Description("Stop")]
        Stop = 4,
    }
}
