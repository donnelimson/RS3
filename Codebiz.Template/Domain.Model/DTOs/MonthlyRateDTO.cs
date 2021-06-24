using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs
{
   public class MonthlyRateDTO
    {
        public int MonthlyRateId { get; set; }
        public string BillingPeriod { get; set; }
        public string Description { get; set; }
        public string ConsumerClass { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class MonthlyRateDetailDTO
    {
        public string BillingPeriod { get; set; }
        public string Description { get; set; }
        public string ConsumerClass { get; set; }
        public int ConsumerClassId { get; set; }
        public List<MonthlyRateDetailCategoryDTO> Categories { get; set; }
    }

    public class MonthlyRateDetailCategoryDTO
    {
        public int BillingUnbundledTransactionCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<MonthlyRateDetailUnbundledDTO> UnbundledTransactions { get; set; }
    }

    public class MonthlyRateDetailUnbundledDTO
    {
        public int? BillingUnbundledTransactionId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string DisplayedName { get; set; }
        public float? Rate { get; set; }

        public float? Amount { get; set; }
        public float? ModifiedIndex { get; set; }
    }
    public class GetUnbundledNameByCodeDTO
    {
        public int BillingUnbundledTransactionId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string DisplayedName { get; set; }
    }
}
