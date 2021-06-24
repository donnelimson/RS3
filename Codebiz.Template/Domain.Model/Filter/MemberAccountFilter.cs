using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Filter
{
   public class MemberAccountFilter: FilterBase
    {
        public string AccountNo { get; set; }
        public string ConsumerNo { get; set; }
        public int? ConsumerTypeId { get; set; }
        public string ConsumerType { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int? Status { get; set; }
        public int? CurrentPhase { get; set; }
        public int? MemberId { get; set; }
        public string Searcher { get; set; }
        public string AccountType { get; set; }
    }
}
