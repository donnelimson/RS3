using Codebiz.Domain.Common.Model.EnumBaseModels;
using Codebiz.Domain.Common.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DataModel.Collection
{
    public class Surcharge : ModelBase
    {
        [Key]
        public int SurchargeId { get; set; }

        [ForeignKey("ConsumerClass")]
        public int? ConsumerClassId { get; set; }
        public virtual ConsumerClass ConsumerClass { get; set; }

        public int YearOfDelinquency { get; set; }
        public double RatePerMonth { get; set; }
        public bool IsDeleted { get; set; }

    }
}
