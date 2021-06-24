using System.Collections.Generic;

namespace Codebiz.Domain.ERP.Model.ViewModel.CSA.MonthlyRate
{
    public class MonthlyRateViewModel
    {
        public int MonthlyRateId { get; set; }
        public string BillingPeriod { get; set; }
        public string Description { get; set; }
        public int ConsumerClassId { get; set; }
        public string ConsumerClass { get; set; }
        public List<MonthlyRateUnbundledCategoryViewModel> UnbundledTransactionsByCategory { get; set; }
        public bool IsClone { get; set; }
    }

    public class MonthlyRateUnbundledCategoryViewModel
    {
        public int BillingUnbundledTransactionCategoryId { get; set; }
        public string Description { get; set; }
        public List<BillingUnbundledTransationList> UnbundledTransactionList { get; set; }
        public List<MonthlyRateUnbundledViewModel> UnbundledTransactions { get; set; }
    }

    public class MonthlyRateUnbundledViewModel
    {
        public int Id { get; set; }
        public int BillingUnbundledTransactionId { get; set; }
        public string RateOrAmount { get; set; }
        public string Rate { get; set; }
        public string Amount { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string DisplayedName { get; set; }
        public bool ComputedByKVA { get; set; }
    }

    public class BillingUnbundledTransationList
    {
        public int BillingUnbundledTransactionId { get; set; }
        public string Unbundled { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string DisplayedName { get; set; }
        public bool ComputedByKVA { get; set; }
    }
}
