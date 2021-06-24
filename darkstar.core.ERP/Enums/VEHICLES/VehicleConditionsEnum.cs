using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.ERP.Model.Enums.VEHICLES
{
    public enum VehicleConditionsEnum
    {
        [Description("Good")]
        Good = 1,

        [Description("Faulty")]
        Faulty = 2,
    }
}
