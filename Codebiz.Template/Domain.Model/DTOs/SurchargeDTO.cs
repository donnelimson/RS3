using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs
{
    public class SurchargeDTO
    {
        public int SurchargeId { get; set; }
        public string ConsumerClass { get; set; }
        public int YearOfDelinquency { get; set; }
        public double RatePerMonth { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
