using System;

namespace Codebiz.Domain.Common.Model.Filter
{
    public class PaymentFilter : FilterBase
    {
        public string ORNo { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public DateTime? ORDateFrom { get; set; }
        public DateTime? ORDateTo { get; set; }
    }
}
