using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model
{
    public class LifelineSubsidyDTO
    {
        public int LifelineSubsidyId { get; set; }

        [DisplayName("CODE")]
        public string Code { get; set; }

        [DisplayName("MINIMUM")]
        public int Minimum { get; set; }

        [DisplayName("MAXIMUM")]
        public int Maximum { get; set; }

        [DisplayName("DISCOUNT")]
        public float Discount { get; set; }

        [DisplayName("ACTIVE")]
        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}