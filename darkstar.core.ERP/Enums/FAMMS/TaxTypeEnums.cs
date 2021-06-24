using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.ERP.Model.Enums.FAMMS
{
    public enum TaxTypeEnums
    {
        [Description("Regular Tax")]
        RegularTax = 1,

        [Description("Use Tax")]
        UseTax = 2,

        [Description("No Tax")]
        NoTax = 3,
    }
}
