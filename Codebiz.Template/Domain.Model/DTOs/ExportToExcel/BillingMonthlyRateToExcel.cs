using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.ExportToExcel
{
   public class BillingMonthlyRateToExcel
    {
        [DisplayName("CONSUMER CLASS")]
        public string ConsumerClass { get; set; }
        [DisplayName("BILLING PERIOD")]
        public string BillingPeriod { get; set; }
        [DisplayName("DESCRIPTION")]
        public string Description { get; set; }
        [DisplayName("CREATED BY")]
        public string CreatedBy { get; set; }
        [DisplayName("CREATED DATE")]
        public DateTime CreatedDate { get; set; }
    }
}
