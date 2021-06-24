using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codebiz.Domain.Common.Model.Financial
{
    public class Currency : ModelBase
    {
        [Key]
        public int CurrencyId { get; set; }

        public string Code { get; set; }
        [DisplayName("NAME")]

        public string CurrencyName { get; set; }

        public string InternationalCode { get; set; }

        public string InternationalDescription { get; set; }

        public string HundredthName { get; set; }

        public string English { get; set; }

        public int? ISOCurrencyCodeId { get; set; }

        public decimal? IncomingAmountDiffAllowed { get; set; }

        public decimal? OutAmountDiffAllowed { get; set; }

        public decimal? IncomingPercentDiffAllowed { get; set; }

        public decimal? OutPercentDiffAllowed { get; set; }

        public int? RoundingId { get; set; }

        public int? DecimalId { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsRounding { get; set; }
    }
}
