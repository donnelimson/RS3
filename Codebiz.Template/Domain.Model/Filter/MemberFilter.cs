using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Filter
{
    public class MemberFilter : FilterBase
    {
        public string ConsumerNo { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int? Status { get; set; }
        public int? ConsumerType { get; set; }
        public bool IsForApplicant { get; set; }
        public int? ApplicantType { get; set; }
        public int? MembershipTypeId { get; set; }
    }
}
