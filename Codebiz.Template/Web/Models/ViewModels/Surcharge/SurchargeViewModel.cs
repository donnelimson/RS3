using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.ViewModels.Surcharge
{
    public class SurchargeViewModel
    {
        public int SurchargeId { get; set; }
        public int ConsumerClassId { get; set; }
        public int YearOfDelinquency { get; set; }
        public double RatePerMonth { get; set; }
    }
}