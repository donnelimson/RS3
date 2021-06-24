using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models.ViewModels.BillingMonthlyRate
{
    public class GenerateMonthlyRateDescriptionViewModel
    {
        public int? MonthlyRateId { get; set; }
        public string BillingPeriod { get; set; }
        public string Description { get; set; }
    }
}