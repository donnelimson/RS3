using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.ExportToExcel
{
    public class SurchargeToExcel
    {
        [DisplayName("CONSUMER CLASS")]
        public string ConsumerClass { get; set; }

        [DisplayName("YEAR OF DELINQUENCY")]
        public int? YearOfDelinquency { get; set; }

        [DisplayName("RATE PER MONTH")]
        public double? RatePerMonth { get; set; }

        [DisplayName("ACTIVE")]
        public bool IsActive { get; set; }

        [DisplayName("CREATED BY")]
        public string CreatedBy { get; set; }

        [DisplayName("CREATED DATE")]
        public DateTime? CreatedDate { get; set; }
    }
}
