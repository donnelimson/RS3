using System;

namespace Codebiz.Domain.Common.Model.Filter.Currency
{
    public class CurrencyFilter : FilterBase
    {
        public string Code { get; set; }
        public string Currency { get; set; }
        public string ActionBy { get; set; }
        public DateTime? ActionOn { get; set; }
    }
}
