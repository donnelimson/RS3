using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.Currency
{
    public class CurrencyDTO
    {
        public int CurrencyId { get; set; }

        [DisplayName("CODE")]
        public string Code { get; set; }

        [DisplayName("CURRENCY")]
        public string Currency { get; set; }

        [DisplayName("ACTION BY")]
        public string ActionBy { get; set; }

        [DisplayName("ACTION ON")]
        public DateTime? ActionOn { get; set; }

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
