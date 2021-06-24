using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.ViewModel.Currency
{
    public class CurrencyViewModel
    {
        public int CurrencyId { get; set; }
        public string Code { get; set; }
        public string Currency { get; set; }
        public string InternationalCode { get; set; }
        public string InternationalDescription { get; set; }
        public string HundredthName { get; set; }
        public string English { get; set; }
        public string EHN { get; set; }
        public int? ISOCurrencyCodeId { get; set; }
        public decimal? IncAmtDffAllwd { get; set; }
        public decimal? OutAmtDffAllwd { get; set; }
        public decimal? IncPrcntDffAllwd { get; set; }
        public decimal? OutPrcntDffAllwd { get; set; }
        public int? RoundingId { get; set; }
        public int? DecimalId { get; set; }

        public bool IsRounding { get; set; }
    }
}
