using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Enums.BusinessPartners
{
    public enum CashDiscountBasedOnEnum
    {
        [Description("Posting Date")]
        PostingDate = 1,

        [Description("System Date")]
        SystemDate = 2,

        [Description("Document Date")]
        DocumentDate = 3,
    }
}
