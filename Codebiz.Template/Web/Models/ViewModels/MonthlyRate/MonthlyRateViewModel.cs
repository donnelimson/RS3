using Codebiz.Domain.Common.Model;
using System;
using System.Collections.Generic;

namespace Web.Models.ViewModels.MonthlyRate
{
    public class MonthlyRateViewModel
    {
        public int MonthlyRateId { get; set; }
        public int BillingUnbundledTransactionId { get; set; }
        public string BillingPeriod { get; set; }
        public string Description { get; set; }
        public int ConsumerTypeId { get; set; }
        public int GenerationVatTotal { get; set; }
        public int GramVatTotal { get; set; }
        public int CeraVatTotal { get; set; }
        public int TransmissionVatTotal { get; set; }
        public int SystemLossVatTotal { get; set; }
        public int DistributionVatTotal { get; set; }
        public int OthersVatTotal { get; set; }
        public DateTime? CreatedOn { get; set; }
        public List<BillingMonthlyRateUnbundledTransaction> MonthlyRateUnbundledTransactions { get; set; }

    }
}