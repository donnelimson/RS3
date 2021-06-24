using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.ViewModels.BillingUnbundledTransaction
{
    public class BillingUnbundledTransactionViewModel
    {
        public int BillingUnbundledTransactionId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string DisplayedName { get; set; }
        public int BillingUnbundledTransactionCategoryId { get; set; }
        public List<string> IncludedTo { get; set; }
        public string Classification { get; set; }
        public string ComputedBy { get; set; }
        public bool ZeroWhenThereIsDemand { get; set; }
        public bool ZeroWhenThereIsNoDemand { get; set; }
        public bool IsICERA { get; set; }
        public bool IsGRAM { get; set; }
        public bool IsLifelineSubsidy { get; set; }
        public bool IsSeniorCitizenDiscount { get; set; }

        public List<int> BillingUnbundledTransactionForVatIds { get; set; }
        public List<int> BillingUnbundledTransactionForDiscountIds { get; set; }

    }
}